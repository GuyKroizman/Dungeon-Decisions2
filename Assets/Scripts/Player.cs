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

    Direction m_direction;

    Rigidbody m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_direction = new Direction();
    }

    // Update is called once per frame
    void Update()
    {
        float turningSpeed = 175.0f;
        RotateToDestination(m_direction.getDirection(), turningSpeed);
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
            m_rigidbody.AddForce(transform.forward * 10);
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


}
