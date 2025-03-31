using Assets.SuperGoalie.Scripts.Entities;
using Assets.SuperGoalie.Scripts.FSMs;
using Assets.SuperGoalie.Scripts.States.GoalKeeperStates.Idle.MainState;
using Assets.SuperGoalie.Scripts.States.GoalKeeperStates.InterceptShot.MainState;
using RobustFSM.Base;
using System;
using UnityEngine;

namespace Assets.SuperGoalie.Scripts.States.GoalKeeperStates.Dive.MainState
{
    public class PunchBallMainState : BState
    {
        bool _ballTrapable;
        float _height;
        float _time;
        float _turn;
        float _weightMultiplier;
        Vector3 _leftHandTargetPosition;
        Vector3 _rightHandTargetPosition;

        public override void Enter()
        {
            base.Enter();

            _time = 0f;

            //get some important data
            _ballTrapable = Machine.GetState<InterceptShotMainState>().BallTrapable;
            _leftHandTargetPosition = Machine.GetState<InterceptShotMainState>().LeftHandTargetPosition;
            _rightHandTargetPosition = Machine.GetState<InterceptShotMainState>().RightHandTargetPosition;
            _turn = Machine.GetState<InterceptShotMainState>().Turn;

            //if the ball is trappable then hit it away
            if (_ballTrapable)
            {
                //calculate the punch direction
                Vector3 ballRelativePosition = Owner.transform.InverseTransformPoint(Owner.Ball.Position);
                Vector3 ballPunchDirection = Vector3.zero;
               
                //detemine the punch direction
                if (Mathf.Abs(ballRelativePosition.x) > 0.1f)
                {
                    //simply punch it to the side
                    ballPunchDirection = new Vector3(ballRelativePosition.x, 0f, 0f);
                }
                else
                {
                    //if it's less than my height then punch it infront of me else punch up
                    if (ballRelativePosition.y <= Owner.Height)
                        ballPunchDirection = new Vector3(0f, 0f, 1f);
                    else
                        ballPunchDirection = new Vector3(0f, 1f, -1f);
                }

                //punch the ball
                ballPunchDirection = Owner.transform.TransformDirection(ballPunchDirection);
                ballPunchDirection.Normalize();
                Owner.Ball.Rigidbody.linearVelocity = ballPunchDirection * 0.5f * Owner.Ball.Rigidbody.linearVelocity.magnitude;
            }

            //set the animator to exit the dive state
            Owner.Animator.SetTrigger("Exit");

            //raise the punch ball event
            Action temp = Owner.OnPunchBall;
            if (temp != null)
                temp.Invoke();
        }

        public override void Execute()
        {
            base.Execute();

            //go to idle state the moment the player gets into idle state
            if (Owner.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                Machine.ChangeState<IdleMainState>();
        }

        public override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);

            //declare the weights
            float leftHandWeight = 0f;
            float rightHandWeight = 0f;
            float lookAtWeight = 0f;

            //set the time
            if(_time < 1f)
                _time += 10 * Time.deltaTime;

            //set the weight multiplier
            _weightMultiplier = Mathf.Lerp(1f, 0f, _time);

            //choose which hands to effect
            if (_turn == 0f)
            {
                //set the weights
                leftHandWeight = _weightMultiplier;
                rightHandWeight = _weightMultiplier;
                lookAtWeight = _weightMultiplier;

                //set the animations weights
                Owner.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
                Owner.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);

                //set the animations positions
                Owner.Animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandTargetPosition);
                Owner.Animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandTargetPosition);
            }
            else if (_turn == -1)
            {
                //set the weights
                leftHandWeight = _weightMultiplier;
                lookAtWeight = _weightMultiplier;

                //set the animations weights
                Owner.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

                //set the animations positions
                Owner.Animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandTargetPosition);

            }
            else if (_turn == 1)
            {
                //set the weights
                rightHandWeight = _weightMultiplier;
                lookAtWeight = _weightMultiplier;

                //set the animations weights
                Owner.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);

                //set the animations positions
                Owner.Animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandTargetPosition);
            }

            //set the look target
            Owner.Animator.SetLookAtWeight(lookAtWeight);
            Owner.Animator.SetLookAtPosition(Owner.Ball.Position);
        }

        public override void OnAnimatorMove()
        {
            base.OnAnimatorMove();

            //manipulate the player height
            Owner.ModelRoot.transform.localPosition = Vector3.zero;
        }

        GoalKeeper Owner
        {
            get
            {
                return ((GoalKeeperFSM)SuperMachine).Owner;
            }
        }
    }
}
