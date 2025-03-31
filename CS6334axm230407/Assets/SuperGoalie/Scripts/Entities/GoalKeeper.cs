using Assets.SimpleSteering.Scripts.Movement;
using Assets.SuperGoalie.Scripts.FSMs;
using System;
using UnityEngine;

namespace Assets.SuperGoalie.Scripts.Entities
{
    [RequireComponent(typeof(GoalKeeperFSM))]
    [RequireComponent(typeof(RPGMovement))]
    public class GoalKeeper : MonoBehaviour
    {
        /// <summary>
        /// A reference to the dive speed of this instance
        /// </summary>
        [SerializeField]
        float _diveSpeed = 4f;

        /// <summary>
        /// A refernce to the goal keeping of this instance
        /// </summary>
        [SerializeField]
        float _goalKeeping = 0.85f;

        /// <summary>
        /// A reference to the height of this instance
        /// </summary>
        float _height = 1.9f;

        /// <summary>
        /// A refernce to the jump distance of this instance
        /// </summary>
        [SerializeField]
        float _jumpDistance = 1;

        /// <summary>
        /// A reference to the jump height of this instance
        /// </summary>
        [SerializeField]
        float _jumpHeight = 0.5f;

        /// <summary>
        /// A refernce to the goal keeping of this instance
        /// </summary>
        [SerializeField]
        float _reach = 0.5f;

        /// <summary>
        ///  reference to the tend goal distance of this instance
        /// </summary>
        [SerializeField]
        float _tendGoalDistance = 3f;

        /// <summary>
        ///  reference to the tend goal speed of this instance
        /// </summary>
        [SerializeField]
        float _tendGoalSpeed = 3f;

        /// <summary>
        /// A reference to this instance's animator
        /// </summary>
        [SerializeField]
        Animator _animator;

        /// <summary>
        /// A reference to the ball instance
        /// </summary>
        [SerializeField]
        Ball _ball;

        /// <summary>
        /// A reference to the goal instance
        /// </summary>
        [SerializeField]
        Goal _goal;

        /// <summary>
        /// A reference to the model root
        /// </summary>
        [SerializeField]
        Transform _modelRoot;

        public Action OnHasNoBall;

        public Action OnHasBall;

        public Action OnPunchBall;

        public delegate void BallLaunched(float flightPower, float velocity, Vector3 initial, Vector3 target);
        public BallLaunched OnBallLaunched;

        public bool HasBall { get; set; }

        public float BallFlightTime { get; set; }

        public Vector3 BallHitTarget { get; set; }

        public Vector3 BallInitialPosition { get; internal set; }

        public GoalKeeperFSM FSM { get; set; }

        public RPGMovement RPGMovement { get; set; }

        private void Awake()
        {
            FSM = GetComponent<GoalKeeperFSM>();
            RPGMovement = GetComponent<RPGMovement>();
        }

        public bool IsBallWithChasingDistance()
        {
            return DistanceOfBallToGoal() <= 20f;
        }

        public bool IsBallWithThreateningDistance()
        {
            return DistanceOfBallToGoal() <= 30f;
        }

        public bool IsShotOnTarget()
        {
            return _goal.IsPositionWithinGoalMouthFrustrum(BallHitTarget);
        }

        public float DistanceOfBallToGoal()
        {
            return Vector3.Distance(_ball.transform.position, _goal.transform.position);
        }

        public void Instance_OnBallLaunched(float flightTime, float velocity, Vector3 initial, Vector3 target)
        {
            BallLaunched temp = OnBallLaunched;
            if (temp != null)
                temp.Invoke(flightTime, velocity, initial, target);
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        public float DiveReach
        {
            get
            {
                return JumpDistance + Reach;
            }
        }

        public float DiveSpeed
        {
            get
            {
                return _diveSpeed;
            }

            set
            {
                _diveSpeed = value;
            }
        }

        public float GoalKeeping
        {
            get
            {
                return _goalKeeping;
            }

            set
            {
                _goalKeeping = value;
            }
        }

        public float JumpDistance
        {
            get
            {
                return _jumpDistance;
            }

            set
            {
                _jumpDistance = value;
            }
        }

        public float JumpReach
        { 
            get
            {
                return Height + JumpHeight;
            }
        }

        public float Reach
        {
            get
            {
                return _reach;
            }

            set
            {
                _reach = value;
            }
        }

        public float TendGoalDistance
        {
            get
            {
                return _tendGoalDistance;
            }

            set
            {
                _tendGoalDistance = value;
            }
        }

        public float TendGoalSpeed
        {
            get
            {
                return _tendGoalSpeed;
            }

            set
            {
                _tendGoalSpeed = value;
            }
        }

        public Animator Animator
        {
            get
            {
                return _animator;
            }

            set
            {
                _animator = value;
            }
        }

        public Ball Ball
        {
            get
            {
                return _ball;
            }

            set
            {
                _ball = value;
            }
        }

        public Goal Goal
        {
            get
            {
                return _goal;
            }

            set
            {
                _goal = value;
            }
        }

        public float Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        public float JumpHeight
        {
            get
            {
                return _jumpHeight;
            }

            set
            {
                _jumpHeight = value;
            }
        }

        public Transform ModelRoot
        {
            get
            {
                return _modelRoot;
            }

            set
            {
                _modelRoot = value;
            }
        }

        public float BallVelocity { get; internal set; }
    }
}
