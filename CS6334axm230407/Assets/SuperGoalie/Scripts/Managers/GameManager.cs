using Assets.SuperGoalie.Scripts.Entities;
using Assets.SuperGoalie.Scripts.States.GoalKeeperStates.Idle.MainState;
using Patterns.Singleton;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SuperGoalie.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        float _ballDribbleForce = 5f;

        [SerializeField]
        float _ballKickForce = 15;

        [SerializeField]
        Ball _ball;

        [SerializeField]
        Goal _goal;

        [SerializeField]
        GoalKeeper _goalKeeper;

        [SerializeField]
        Text _scoreText;

        bool _run = true;
        int _score;
        Vector3 _ballInitPos;
        Quaternion _ballInitRot;

        protected Transform Cam;                  // A reference to the main camera in the scenes transform
        protected Vector3 CamForward;             // The current forward direction of the camera

        public delegate void BallLaunch(float power, Vector3 target);   //delegate to launch a ball
        public BallLaunch OnBallLaunch;                                 //on ball launch

        public override void Awake()
        {
            // register the game manager to some events
            _ball.OnBallLaunched += SoundManager.Instance.PlayBallKickedSound;
            _goalKeeper.OnPunchBall += SoundManager.Instance.PlayBallKickedSound;
            _goal.GoalTrigger.OnCollidedWithBall += SoundManager.Instance.PlayGoalScoredSound;

            //register entities to entitiy delegates
            _ball.OnBallLaunched += _goalKeeper.Instance_OnBallLaunched;

            //register entities to entitiy delegates
            _goal.GoalTrigger.OnCollidedWithBall += Instance_OnBallCollidedWithGoal;

            //register entities to local delegates
            OnBallLaunch += _ball.Instance_OnBallLaunch;

            //cache the initial data
            _ballInitPos = _ball.Position;
            _ballInitRot = _ball.Rotation;

            // get the transform of the main camera
            if (Camera.main != null)
                Cam = Camera.main.transform;
            else
                Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        private void Instance_OnBallCollidedWithGoal()
        {
            ++_score;
            _scoreText.text = string.Format("Score:{0}", _score);
        }

        private void Update()
        {
            if (!_run)
                return;

            #region TriggerShooting

            //get the mouse
            if (Input.GetMouseButtonDown(0))
            {
                //create a ray from mouse clicked position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //run a raycast into the scene
                if(Physics.Raycast(ray, out hit))
                {
                    //I can no longer run
                    _run = false;

                    //get the target
                    Vector3 target = hit.point;
                    
                    //launch the ball
                    BallLaunch tempBallLaunch = OnBallLaunch;
                    if (tempBallLaunch != null)
                        tempBallLaunch.Invoke(_ballKickForce, target);

                    //start the reset coroutine
                    StartCoroutine(Reset());
                }
            }
            else
            {
                //capture input
                float horizontalRot = Input.GetAxisRaw("Horizontal");
                float verticalRot = Input.GetAxisRaw("Vertical");
                verticalRot = 0f;

                //calculate the direction to rotate to
                Vector3 input = new Vector3(horizontalRot, 0f, verticalRot);

                //process if any key down
                if (Input.anyKeyDown)
                {
                    Vector3 Movement = new Vector3();

                    // calculate move direction to pass to character
                    if (Cam != null)
                    {
                        // calculate camera relative direction to move:
                        CamForward = Vector3.Scale(Cam.forward, new Vector3(1, 0, 1)).normalized;
                        Movement = input.z * CamForward + input.x * Cam.right;
                    }
                    else
                    {
                        // we use world-relative directions in the case of no main camera
                        Movement = input.z * Vector3.forward + input.x * Vector3.right;
                    }

                    //kick the ball
                    Movement.y = 0.03f;
                    _ball.Rigidbody.linearVelocity = Movement * _ballDribbleForce;
                }
            }

            #endregion
        }

        private IEnumerator Reset()
        {
            yield return new WaitForSeconds(5f);

            _ball.gameObject.SetActive(false);
            _ball.Stop();
            _ball.Position = _ballInitPos;
            _ball.Rotation = _ballInitRot;

            _goalKeeper.FSM.ChangeState<IdleMainState>();

            yield return new WaitForSeconds(1f);

            _run = true;
            _ball.gameObject.SetActive(true);
            _goal.GoalTrigger.gameObject.SetActive(true);
        }
    }
}
