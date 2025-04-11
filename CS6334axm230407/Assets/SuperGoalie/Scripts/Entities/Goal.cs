using System;
using Assets.SuperGoalie.Scripts.Triggers;
using UnityEngine;

namespace Assets.SuperGoalie.Scripts.Entities
{
    public class Goal : MonoBehaviour
    {
        [SerializeField]
        GoalMouth _goalMouth;

        [SerializeField]
        GoalTrigger _goalTrigger;

        internal bool IsPositionWithinGoalMouthFrustrum(Vector3 position)
        {
            //find the relative position to goal
            Vector3 relativePosition = transform.InverseTransformPoint(position);

            //find the relative position of each goal mouth
            Vector3 pointBottomLeftRelativePosition = transform.InverseTransformPoint(_goalMouth._pointBottomLeft.position);
            Vector3 pointBottomRightRelativePosition = transform.InverseTransformPoint(_goalMouth._pointBottomRight.position);
            Vector3 pointTopLeftRelativePosition = transform.InverseTransformPoint(_goalMouth._pointTopLeft.position);

            //check if the x- coordinate of the relative position lies within the goal mouth
            bool isPositionWithTheXCoordinates = relativePosition.x > pointBottomLeftRelativePosition.x && relativePosition.x < pointBottomRightRelativePosition.x;
            bool isPositionWithTheYCoordinates = relativePosition.y > pointBottomLeftRelativePosition.y && relativePosition.y < pointTopLeftRelativePosition.y;

            //the result is the combination of the two tests
            return isPositionWithTheXCoordinates && isPositionWithTheYCoordinates;
        }

        public GoalMouth GoalMouth
        {
            get
            {
                return _goalMouth;
            }

            set
            {
                _goalMouth = value;
            }
        }

        public GoalTrigger GoalTrigger
        {
            get
            {
                return _goalTrigger;
            }

            set
            {
                _goalTrigger = value;
            }
        }
       
    }

    [Serializable]
    public struct GoalMouth
    {
        public Transform _pointBottomLeft;
        public Transform _pointBottomRight;
        public Transform _pointTopLeft;
        public Transform _pointTopRight;
    }
}
