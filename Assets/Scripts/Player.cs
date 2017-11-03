using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

  
    
    class TurnInfo
    {
        private float rotation = 0.0f;
        private Quaternion m_RotateDestinationQuaternion = Quaternion.identity;

        public void TurnRight()
        {
            rotation += 90.0f;
            m_RotateDestinationQuaternion = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        public void TurnLeft()
        {
            rotation -= 90.0f;
            m_RotateDestinationQuaternion = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        internal Quaternion getTurnDestination()
        {
            return m_RotateDestinationQuaternion;
        }

        internal float getTurningSpeed()
        {
            return 175;
        }
    }

    TurnInfo m_TurnInfo;

    Rigidbody m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        m_TurnInfo = new TurnInfo();
    }

    // Update is called once per frame
    void Update()
    {
        RotateToDestination(m_TurnInfo.getTurnDestination(), m_TurnInfo.getTurningSpeed());
    }

    private void RotateToDestination(Quaternion rotateDestinationQuaternion, float turnSpeed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDestinationQuaternion, turnSpeed * Time.deltaTime);
    }

    public void move(int asdf)
    {
        if (asdf == 0)
        {
            m_rigidbody.AddForce(transform.forward * 10);
        }


        if (asdf == 1)
        {
            m_TurnInfo.TurnLeft();                      
        }

        if (asdf == 2)
        {
            m_TurnInfo.TurnRight();            
        }

        
    }


}
