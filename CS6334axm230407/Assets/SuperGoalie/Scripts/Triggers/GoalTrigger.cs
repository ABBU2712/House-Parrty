using System;
using UnityEngine;

namespace Assets.SuperGoalie.Scripts.Triggers
{
    public class GoalTrigger : MonoBehaviour
    {
        public Action OnCollidedWithBall;

        private void OnTriggerEnter(Collider other)
        {
            //if tag is ball
            if(other.tag == "Ball")
            {
                //invoke that the wall has collided with the ball
                Action temp = OnCollidedWithBall;
                if (temp != null)
                    temp.Invoke();

                // disable
                gameObject.SetActive(false);
            }
        }
    }
}
