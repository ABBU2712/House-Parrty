using System;
using UnityEngine;

namespace Assets.SuperGoalie.Scripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Ball : MonoBehaviour
    {
        [Tooltip("The gravity acting on the ball")]
        public float gravity = 9f;

        public delegate void BallLaunched(float flightTime, float velocity, Vector3 initial, Vector3 target);
        public BallLaunched OnBallLaunched;

        public Rigidbody Rigidbody { get; set; }

        public SphereCollider SphereCollider { get; set; }

        private void Awake()
        {
            //get the components
            Rigidbody = GetComponent<Rigidbody>();
            SphereCollider = GetComponent<SphereCollider>();

            // set the gravity of the ball
            Physics.gravity = new Vector3(0f, -gravity, -0f);
        }

        public void Stop()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.linearVelocity = Vector3.zero;
        }

        public Vector3 FuturePosition(float time)
        {
            //get the velocities
            Vector3 velocity = Rigidbody.linearVelocity;
            Vector3 velocityXZ = velocity;
            velocityXZ.y = 0f;

            //find the future position on the different axis
            float futurePositionY = Position.y + (velocity.y * time + 0.5f * -gravity * Mathf.Pow(time, 2));
            Vector3 futurePositionXZ = Vector3.zero;

            //get the ball future position
            futurePositionXZ = Position + velocityXZ.normalized * velocityXZ.magnitude * time;

            //bundle the future positions to together
            Vector3 futurePosition = futurePositionXZ;
            futurePosition.y = futurePositionY;

            //return the future position
            return futurePosition;
        }

        public void Launch(float power, Vector3 final)
        {
            //set the initial position
            Vector3 initial = Position;

            //find the direction vectors
            Vector3 toTarget = final - initial;
            Vector3 toTargetXZ = toTarget;
            toTargetXZ.y = 0;

            //find the time to target
            float time = toTargetXZ.magnitude / power;

            // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
            // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
            // so xz = v0xz * t => v0xz = xz / t
            // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
            toTargetXZ = toTargetXZ.normalized * toTargetXZ.magnitude / time;

            //set the y-velocity
            Vector3 velocity = toTargetXZ;
            velocity.y = toTarget.y / time + (0.5f * gravity * time);

            //return the velocity
            Rigidbody.linearVelocity = velocity;

            //invoke the ball launched event
            BallLaunched temp = OnBallLaunched;
            if (temp != null)
                temp.Invoke(time, power, initial, final);
        }

        public void Instance_OnBallLaunch(float power, Vector3 target)
        {
            //launch the ball
            Launch(power, target);
        }

        public Quaternion Rotation
        {
            get
            {
                return transform.rotation;
            }

            set
            {
                transform.rotation = value;
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }

            set
            {
                transform.position = value;
            }
        }
    }
}
