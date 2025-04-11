using Assets.SuperGoalie.Scripts.Entities;
using Assets.SuperGoalie.Scripts.FSMs;
using Assets.SuperGoalie.Scripts.Others.Utilities;
using Assets.SuperGoalie.Scripts.States.GoalKeeperStates.Dive.MainState;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.SuperGoalie.Scripts.States.GoalKeeperStates.InterceptShot.MainState
{
    public class InterceptShotMainState : BState
    {
        float _height;
        float _initDistOfBallToOrthogonalPoint;
        float _speed;
        float _timeToOrthogonalPoint;
        float _timeOfBallToPlayerOrthogonalPointOnBallPath;
        float _timeOfBallToPlayerOrthogonalPointOnBallPathCached;
        float _turn;
        float _weightMultiplier;
        Vector3 _ballInitPosition;
        Vector3 _playerInterceptPoint;
        Vector3 _playerSteeringTarget;
        Vector3 _ballPositionAtPlayerOrthogonalPoint;
        Vector3 _ballVelocity;
        Vector3 _ballVelocityXZ;
        Vector3 _normalizedBallInitialPosition;
        Vector3 _normalizedBallPosition;
        Vector3 _normalizedBallTarget;
        Vector3 _normalizedPlayerPosition;
        Vector3 _relativeBallPositionAtPlayerInterceptPoint;
        Vector3 _orthogonalPointToPlayerPositionOnBallPath;

        public bool BallTrapable { get; set; }

        public float RequiredJumpHeight { get; set; }

        public Vector3 LeftHandTargetPosition { get; set; }

        public Vector3 RightHandTargetPosition { get; set; }

        public override void Enter()
        {
            base.Enter();
           
            //normalize stuff
            _normalizedBallInitialPosition = new Vector3(Owner.BallInitialPosition.x, 0f, Owner.BallInitialPosition.z);
            _normalizedBallTarget = new Vector3(Owner.BallHitTarget.x, 0f, Owner.BallHitTarget.z);
            _normalizedPlayerPosition = new Vector3(Owner.Position.x, 0f, Owner.Position.z);

            //find the point on the ball path to target that is orthogonal to player position
            _playerInterceptPoint = _orthogonalPointToPlayerPositionOnBallPath = OrthogonalPoint.OrthPoint(_normalizedBallInitialPosition, _normalizedBallTarget, Owner.Position);

            //calculate some data depended on the orthogonal point
            _initDistOfBallToOrthogonalPoint = Vector3.Distance(_orthogonalPointToPlayerPositionOnBallPath, _normalizedBallInitialPosition);
            float distanceBetweenInitialBallPositionAndOrthogonalPoint = Vector3.Distance(_normalizedBallInitialPosition, _orthogonalPointToPlayerPositionOnBallPath);
            _timeOfBallToPlayerOrthogonalPointOnBallPathCached = _timeOfBallToPlayerOrthogonalPointOnBallPath = distanceBetweenInitialBallPositionAndOrthogonalPoint / Owner.BallVelocity;

            //calculate ball position at player orthogonal point
            _ballPositionAtPlayerOrthogonalPoint = Owner.Ball.FuturePosition(_timeOfBallToPlayerOrthogonalPointOnBallPath);

            /**Calculate player control variables**/

            Vector3 dirToOrthogonalPoint = _orthogonalPointToPlayerPositionOnBallPath - Owner.Position;                     //calculate the player intercept point
            dirToOrthogonalPoint.y = 0.0f;

            float maxMovementDistanceOfPlayer = 0f;
            float playerDistanceToOrthogonalPoint = dirToOrthogonalPoint.magnitude;

            //find the distance that the player can jump and still reach the ball
            if (playerDistanceToOrthogonalPoint >= Owner.Reach)
                maxMovementDistanceOfPlayer = Mathf.Clamp(playerDistanceToOrthogonalPoint - Owner.Reach, 0f, Owner.JumpDistance);        //find the maximum distance the player can dive
            else
                maxMovementDistanceOfPlayer = playerDistanceToOrthogonalPoint;

            //get the relative ball position at player 
            _relativeBallPositionAtPlayerInterceptPoint = Owner.transform.InverseTransformPoint(_ballPositionAtPlayerOrthogonalPoint);

            //if I'm already at target, I target somewhere in front of me
            if (Mathf.Abs(_relativeBallPositionAtPlayerInterceptPoint.x) < Owner.Reach)
            {
                //update the ball intercept point
                _playerInterceptPoint += Owner.transform.forward * Owner.Reach * 0.5f;
                _initDistOfBallToOrthogonalPoint = Vector3.Distance(_playerInterceptPoint, _normalizedBallInitialPosition);

                //we get the future ball target at steering target
                _timeOfBallToPlayerOrthogonalPointOnBallPathCached = _timeOfBallToPlayerOrthogonalPointOnBallPath = _initDistOfBallToOrthogonalPoint / Owner.BallVelocity;
                _ballPositionAtPlayerOrthogonalPoint = Owner.Ball.FuturePosition(_timeOfBallToPlayerOrthogonalPointOnBallPath);
                
                //find the future position of the ball relative to me
                _relativeBallPositionAtPlayerInterceptPoint = Owner.transform.InverseTransformPoint(_ballPositionAtPlayerOrthogonalPoint);
            }

            //calculate the required player speed to reach the steering target at the same time as the ball
            float playerRawDiveSpeed = maxMovementDistanceOfPlayer / _timeOfBallToPlayerOrthogonalPointOnBallPath;                      //find the raw dive speed
            float playerDiveSpeed = Mathf.Clamp(playerRawDiveSpeed, 0f, Owner.DiveSpeed);                                               //clamp the player dive speed

            //calculate the jump height required to reach the ball
            float playerRawJumpHeight = _relativeBallPositionAtPlayerInterceptPoint.y - Owner.Height;                                          //find the jump height to reach ball
            float playerJumpHeight = Mathf.Clamp(playerRawJumpHeight, 0f, Owner.JumpHeight);                                            //clamp the jump height
            RequiredJumpHeight = playerJumpHeight;

            //find the player intecpt point
            _playerSteeringTarget = _normalizedPlayerPosition + dirToOrthogonalPoint.normalized * maxMovementDistanceOfPlayer;

            //set the steering component
            Owner.RPGMovement.SetMoveTarget(_playerSteeringTarget);
            Owner.RPGMovement.Speed = playerDiveSpeed;
            Owner.RPGMovement.SetSteeringOn();

            //set the height animator value
            _height = Mathf.Clamp(_relativeBallPositionAtPlayerInterceptPoint.y / Owner.JumpReach, 0f, 1f);

            //set the turn animator value
            if (Mathf.Abs(_relativeBallPositionAtPlayerInterceptPoint.x) < Owner.Reach)
                _turn = 0f;
            else if (_relativeBallPositionAtPlayerInterceptPoint.x > 0)
                _turn = 1f;
            else if (_relativeBallPositionAtPlayerInterceptPoint.x < 0)
                _turn = -1f;

            //set the dive animation to play
            Owner.Animator.SetFloat("Height", _height);
            Owner.Animator.SetFloat("Turn", _turn);
            Owner.Animator.SetTrigger("Dive");

            //calculate the targets for the hands
            LeftHandTargetPosition = GetBoneIKTarget(HumanBodyBones.LeftHand);
            RightHandTargetPosition = GetBoneIKTarget(HumanBodyBones.RightHand);

        }

        public override void Execute()
        {
            base.Execute();

            //normalize stuff here
            _normalizedBallPosition = new Vector3(Owner.Ball.Position.x, Owner.Ball.Position.y + Owner.Ball.SphereCollider.radius, Owner.Ball.Position.z);
            _normalizedPlayerPosition = new Vector3(Owner.Position.x, 0f, Owner.Position.z);

            //stop moving if at target
            if (Vector3.Distance(_normalizedPlayerPosition, _playerSteeringTarget) <= 0.1f)
                Owner.RPGMovement.SetSteeringOff();

            //punch the ball
            if (_turn == 0f)
            {
                //check if the right hand can punch the ball
                BallTrapable = GetDistOfBoneToPosition(HumanBodyBones.RightHand, _normalizedBallPosition) <= GetDistanceTravelledInSingleFrame(Owner.Ball.Rigidbody.linearVelocity)
                    || GetDistOfBoneToPosition(HumanBodyBones.LeftHand, _normalizedBallPosition) <= GetDistanceTravelledInSingleFrame(Owner.Ball.Rigidbody.linearVelocity);

                //punch the ball
                if (BallTrapable)
                    Machine.ChangeState<PunchBallMainState>();
            }
            else if (_turn == 1)
            {
                //check if the right hand can punch the ball
                float distanceBetweenHandAndBall = GetDistOfBoneToPosition(HumanBodyBones.RightHand, _normalizedBallPosition);
                BallTrapable = distanceBetweenHandAndBall <= GetDistanceTravelledInSingleFrame(Owner.Ball.Rigidbody.linearVelocity);

                //punch the ball
                if (BallTrapable)
                    Machine.ChangeState<PunchBallMainState>();
            }
            else if (_turn == -1)
            {
                //check if the left hand can punch the ball
                float distanceBetweenHandAndBall = GetDistOfBoneToPosition(HumanBodyBones.LeftHand, _normalizedBallPosition);
                BallTrapable = distanceBetweenHandAndBall <= GetDistanceTravelledInSingleFrame(Owner.Ball.Rigidbody.linearVelocity);

                //punch the ball
                if (BallTrapable)
                    Machine.ChangeState<PunchBallMainState>();
            }

            //check if I have exhausted my dive time
            //exit if dive time exhausted
            _timeOfBallToPlayerOrthogonalPointOnBallPath -= Time.deltaTime;
            if (_timeOfBallToPlayerOrthogonalPointOnBallPath <= 0f)
                Machine.ChangeState<PunchBallMainState>();
        }

        public override void Exit()
        {
            base.Exit();

            //set the steering to off
            Owner.RPGMovement.SetSteeringOff();

            //set the animator to exit the dive state
            Owner.Animator.ResetTrigger("Dive");
        }

        public override void OnAnimatorIK(int layerIndex)
        {
            base.OnAnimatorIK(layerIndex);

            //calculate the weight multiplier depending on the remaining distance to target
            _normalizedBallPosition = new Vector3(Owner.Ball.Position.x, 0f, Owner.Ball.Position.z);
            _normalizedPlayerPosition = new Vector3(Owner.Position.x, 0f, Owner.Position.z);

            //find the distance of ball to orthogonal point
            float distanceOfBallToTarget = Vector3.Distance(_normalizedBallPosition, _orthogonalPointToPlayerPositionOnBallPath);

            //if ball comes within reach influence the weight multiplier
            if (distanceOfBallToTarget > 5 * Owner.Reach)
                _weightMultiplier = 0f;
            else
                _weightMultiplier = (5 * Owner.Reach - distanceOfBallToTarget) / 5 * Owner.Reach;

            float leftHandWeight = 0f;
            float rightHandWeight = 0f;
            float lookAtWeight = 0f;

            //choose which hands to effect
            if (_turn == 0f)
            {
                leftHandWeight = _weightMultiplier;
                rightHandWeight = _weightMultiplier;
                lookAtWeight = _weightMultiplier;
            }
            else if(_turn == -1)
            {
                leftHandWeight = _weightMultiplier;
                lookAtWeight = _weightMultiplier;
            }
            else if(_turn == 1)
            {
                rightHandWeight = _weightMultiplier;
                lookAtWeight = _weightMultiplier;
            }

            //set the animations weights
            Owner.Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
            Owner.Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
            Owner.Animator.SetLookAtWeight(lookAtWeight);

            //set the animations positions
            Owner.Animator.SetLookAtPosition(Owner.Ball.Position + new Vector3(0f, Owner.Ball.SphereCollider.radius, 0f));
            Owner.Animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTargetPosition);
            Owner.Animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandTargetPosition);
        }

        public override void OnAnimatorMove()
        {
            base.OnAnimatorMove();

            //calculate the ratio to ball point
            float ratio = (_timeOfBallToPlayerOrthogonalPointOnBallPathCached - _timeOfBallToPlayerOrthogonalPointOnBallPath) / _timeOfBallToPlayerOrthogonalPointOnBallPathCached;

            //manipulate the player height
            float positionY = Mathf.Lerp(0f, RequiredJumpHeight, ratio);

            //now move the player character depending on the height
            Vector3 localPosition = new Vector3(Owner.ModelRoot.transform.localPosition.x, positionY, Owner.ModelRoot.transform.localPosition.z);
            Owner.ModelRoot.transform.localPosition = localPosition;
        }

        public Transform GetBone(HumanBodyBones bone)
        {
            return Owner.Animator.GetBoneTransform(bone).transform;
        }

        public float GetDistOfBoneToPosition(HumanBodyBones bone, Vector3 position)
        {
            //find the distance between the bone and the target
            return Vector3.Distance(Owner.Animator.GetBoneTransform(bone).transform.position, position);
        }

        public float GetDistanceTravelledInSingleFrame(Vector3 velocity)
        {
            return velocity.magnitude * Time.deltaTime;
        }

        public Vector3 GetBoneIKTarget(HumanBodyBones bone)
        {
            //prepare data to calculate hit target
            Vector3 ballIKTarget = _ballPositionAtPlayerOrthogonalPoint + new Vector3(0f, Owner.Ball.SphereCollider.radius, 0f);
            //Vector3 bonePosition = GetBone(bone).position;
            //Vector3 directionOfIkTargetToBone = bonePosition - ballIKTarget;

            //calculate the ik target
            //ballIKTarget = bonePosition + directionOfIkTargetToBone.normalized * (directionOfIkTargetToBone.magnitude - Owner.Ball.SphereCollider.radius);
            //ballIKTarget = ballIKTarget + directionOfIkTargetToBone.normalized * Owner.Ball.SphereCollider.radius;

            //return the ik target
            return ballIKTarget;
        }

        GoalKeeper Owner
        {
            get
            {
                return ((GoalKeeperFSM)SuperMachine).Owner;
            }
        }

        public float Turn
        {
            get
            {
                return _turn;
            }

            set
            {
                _turn = value;
            }
        }
    }
}
