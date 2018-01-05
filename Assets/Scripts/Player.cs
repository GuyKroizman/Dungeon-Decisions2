using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representing the character in the maze.
/// Receive call to Move the player character.
/// </summary>
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

    /// <summary>
    /// Used to detect when a the player stopped moving
    /// </summary>
    class EndMotionDetector
    {        
        Vector3 m_position;
        Quaternion m_rotation;

        /// <summary>
        /// Detect if there is no change between the provided position and rotation to the previous position and rotation.
        /// Because if there is not change that means the player is not moving.
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="newRotation"></param>
        /// <returns></returns>
        internal bool IsMotionEnded(Vector3 newPosition, Quaternion newRotation)
        {
            if (newPosition == m_position || newRotation == m_rotation)
                return true;

            m_position = newPosition;
            m_rotation = newRotation;

            return false;
        }
    }

    // This variable has a life cycle in which it is initiated once the player start a Move (Be it rotate or a forward)
    // and it is deleted once the move ended and then the variable is set to null.
    EndMotionDetector m_endMotionDetector;

    Direction m_direction;

    Rigidbody m_rigidbody;

    MoveForward m_moveForward;

    public GameObject m_progressbar;

    private float m_turningSpeed = 175f;

    void Start()
    {        
        m_rigidbody = GetComponent<Rigidbody>();

        m_endMotionDetector = new EndMotionDetector();

        m_moveForward = new MoveForward(m_rigidbody.transform);

        m_direction = new Direction();        
    }

    // Update is called once per frame
    void Update()
    {
        RotateToDestination(m_direction.getDirection(), m_turningSpeed);

        m_moveForward.Update();

        // detect player end movement.
        if(m_endMotionDetector != null &&
            m_endMotionDetector.IsMotionEnded(m_rigidbody.transform.position, m_rigidbody.transform.rotation))
        {

            // the variable is not null once the player started moving and once the movement ended the variable must 
            // be set to null again
            m_endMotionDetector = null;
            Instantiate(m_progressbar, new Vector3(Screen.width/2, Screen.height / 4, 0), Quaternion.identity);
        }
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
    /// Gets called from the decision master with the command to 
    /// Move the player either forward or turn the player 90 degree left or right; Then
    /// set the internal state of the class to move appropriately which will be carried out
    /// by the Update function.
    /// </summary>
    /// <param name="direction">
    /// 0 is for direction forward
    /// 1 is for direction turn left
    /// 2 is for direction turn right
    /// </param>
    public void Move(int direction)
    {
        if (direction == 0)
        {
            if (!IsPossibleToMoveForward())
            {
                m_moveForward.Start();
                m_endMotionDetector = new EndMotionDetector();
            }                
        }

        if (direction == 1)
        {
            m_direction.TurnLeft();
            m_endMotionDetector = new EndMotionDetector();
        }

        if (direction == 2)
        {
            m_direction.TurnRight();
            m_endMotionDetector = new EndMotionDetector();
        }        
    }

    /// <summary>
    /// cast a ray forward to detect if the player can move forward.
    /// 
    /// </summary>
    /// <returns>bool</returns>
    private bool IsPossibleToMoveForward()
    {
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(transform.position, Camera.main.transform.forward, out hitInfo, 0.5f);

        if (!isHit)
            return false;

        // if we hit something but it does not have a rigid body then there is no problem going there.
        return hitInfo.rigidbody != null;
        
    }


}
