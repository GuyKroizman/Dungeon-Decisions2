using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaster : MonoBehaviour {

    public Player m_player;
    public float m_usersTurnDuration;

    private int m_user1DirectionDecision = -1;
    private int m_user2DirectionDecision = -1;

    private float m_timer = 0;

    // the time the movment takes?
    private const float IN_MOVMENT_DURATION = 1.0f;
    private float m_inMovmentTimer;
    
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        m_inMovmentTimer -= Time.deltaTime;

        Debug.Log("Decision Master timer: " + m_timer + " user1: " + m_user1DirectionDecision + " user2: " + m_user2DirectionDecision);

        // if time is up.
        if(m_timer > m_usersTurnDuration)
        {
            // TODO: kill timer?

            // time is up and users did not decide anything or only one of the decided.
            MovePlayerToRandomDirection();

            // TODO: adjust to the m_usersTurnDuration + the time it took to make the movment. timer = -0.4?
            m_timer = 0;
            m_user1DirectionDecision = -1;
            m_user2DirectionDecision = -1;

            // TODO: create new timer once movment finished.

            m_inMovmentTimer = IN_MOVMENT_DURATION;

        }

        if (IsBothUsersHaveAlreadyDecided())
        {
            // TODO: kill timer
            if (m_user2DirectionDecision == m_user1DirectionDecision)
                m_player.Move(m_user1DirectionDecision);
            else
            {
                MovePlayerToRandomDirection();
            }

            // TODO: adjust to the m_usersTurnDuration + the time it took to make the movment. timer = -0.4?
            m_timer = 0;
            m_user1DirectionDecision = -1;
            m_user2DirectionDecision = -1;

            // TODO: create new timer once movment finished.

            m_inMovmentTimer = IN_MOVMENT_DURATION;
        }
    }

    private void MovePlayerToRandomDirection()
    {
        m_player.Move(UnityEngine.Random.Range(0, 3));
    }

    private bool IsBothUsersHaveAlreadyDecided()
    {
        return m_user1DirectionDecision != -1 && m_user2DirectionDecision != -1;
    }

    internal void Move(int userIndex, int direction)
    {
        if (direction == -1)
            return;


        // prevent from setting the move direction again from the same touch. 
        // TODO: if that works refactor and document
        if (m_inMovmentTimer > 0)
            return;

        if (userIndex == 1)
            m_user1DirectionDecision = direction;

        if (userIndex == 2)
            m_user2DirectionDecision = direction;
    }
}
