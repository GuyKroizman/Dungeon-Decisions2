using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /// <summary>
    /// Hold the state of the rotation.
    /// Does the math using regular degrees and provide the quaternion.
    /// </summary>    
    class Direction
    {
        private float directionDegrees = 0.0f;
        private Quaternion directionQuaternion = Quaternion.identity;

        internal void TurnRight()
        {
            directionDegrees += 90.0f;
            directionQuaternion = Quaternion.Euler(0.0f, directionDegrees, 0.0f);
        }

        internal void TurnLeft()
        {
            directionDegrees -= 90.0f;
            directionQuaternion = Quaternion.Euler(0.0f, directionDegrees, 0.0f);
        }

        internal Quaternion getDirection()
        {
            return directionQuaternion;
        }
    }

    /// <summary>
    /// Move one step forward.
    /// Once start() is called the initial position is stored and the object change state so that
    /// each update will move the player one step
    /// </summary>
    class MoveForward
    {
        bool m_moveNow = false;
        private Vector3 m_initialPosition;
        private Transform m_transform;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform">The player's position</param>
        public MoveForward(Transform transform)
        {
            m_transform = transform;
        }

        /// <summary>
        /// Change the object state so that Update() moves it forward until a distance of one step is traveled
        /// </summary>
        public void Start()
        {
            m_moveNow = true;
            m_initialPosition = m_transform.position;
        }

        /// <summary>
        /// if in move state - Move forward until a distance of one step is traveled.
        /// </summary>
        public void Update()
        {

            if (IsTraveledCompleteStepSinceStart())
                m_moveNow = false;

            if (m_moveNow)
            {
                MoveStep();                
            }
                
        }

        // move one step forward
        private void MoveStep()
        {
            m_transform.Translate(Vector3.forward * Time.deltaTime);
        }

        // is distance from initial position is one step
        private bool IsTraveledCompleteStepSinceStart()
        {
            float distanceFromInitialPosition = Vector3.Distance(m_initialPosition, m_transform.position);
            return distanceFromInitialPosition > 1;            
        }

        
    }

    Direction m_direction;

    Rigidbody m_rigidbody;

    MoveForward m_move_forward;


    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_direction = new Direction();

        m_move_forward = new MoveForward(m_rigidbody.transform);
    }

    // Update is called once per frame
    void Update()
    {
        float turningSpeed = 175.0f;
        RotateToDestination(m_direction.getDirection(), turningSpeed);

        m_move_forward.Update();
    }

    /// <summary>
    /// Rotate the current player in the direction provided, using the speed provided
    /// </summary>
    /// <param name="rotateDestinationQuaternion">The direction to turn to</param>
    /// <param name="turnSpeed">The turning speed</param>
    private void RotateToDestination(Quaternion rotateDestinationQuaternion, float turnSpeed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDestinationQuaternion, turnSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">
    /// 0 is for direction forward
    /// 1 is for directino turn left
    /// 2 is for direction turn right
    /// </param>
    public void Move(int direction)
    {
        if (direction == 0)
        {
            if(!IsThereAWallAhead())
                m_move_forward.Start();
        }

        if (direction == 1)
        {
            m_direction.TurnLeft();                      
        }

        if (direction == 2)
        {
            m_direction.TurnRight();            
        }        
    }

    private bool IsThereAWallAhead()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 0.5f);
    }


}
