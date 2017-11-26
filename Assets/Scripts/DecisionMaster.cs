using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaster : MonoBehaviour {

    public Player m_player;
    public float m_usersTurnDuration;

    BallotBox m_ballotBox;

    private float m_timer = 0;
       

    class BallotBox
    {
        private int m_user1DirectionDecision;
        private int m_user2DirectionDecision;

        // the time the movement takes?
        private const float IN_MOVMENT_DURATION = 1.0f;

        private float m_inMovmentTimer;

        public BallotBox()
        {
            Reset();
        }

        public void Reset()
        {
            m_user1DirectionDecision = -1;
            m_user2DirectionDecision = -1;

            m_inMovmentTimer = IN_MOVMENT_DURATION;
        }

        public void UpdateTime()
        {
            m_inMovmentTimer -= Time.deltaTime;
        }

        public bool IsTwoUsersVotedToMoveInTheSameDirection()
        {
            return m_user2DirectionDecision == m_user1DirectionDecision;
        }

        public int GetFinalDirection()
        {
            if( IsTwoUsersVotedToMoveInTheSameDirection())
                return m_user1DirectionDecision;

            return -1;
        }

        public bool IsBothUsersHaveAlreadyDecided()
        {
            return m_user1DirectionDecision != -1 && m_user2DirectionDecision != -1;
        }

        internal void SetUserDirectionVote(int userIndex, int direction)
        {
            // prevent from setting the move direction again from the same touch.             
            if (m_inMovmentTimer > 0)
                return;

            if (userIndex == 1)
                m_user1DirectionDecision = direction;

            if (userIndex == 2)
                m_user2DirectionDecision = direction;
        }
    }

    private void Start()
    {
        m_ballotBox = new BallotBox();
    }

    // Update is called once per frame
    void Update () {
        m_timer += Time.deltaTime;
        m_ballotBox.UpdateTime();

        // if time is up.
        if(m_timer > m_usersTurnDuration)
        {
            // time is up and users did not decide anything or only one of the users decided.
            MovePlayer(GetRandomDirection());

            // TODO: adjust to the m_usersTurnDuration + the time it took to make the movement. timer = -0.4?

        }

        if (m_ballotBox.IsBothUsersHaveAlreadyDecided())
        {
            
            if (m_ballotBox.IsTwoUsersVotedToMoveInTheSameDirection())
                MovePlayer(m_ballotBox.GetFinalDirection());
            else
            {
                MovePlayer(GetRandomDirection());
            }

            // TODO: adjust to the m_usersTurnDuration + the time it took to make the movement. timer = -0.4?


        }
    }

    private void MovePlayer(int direction)
    {
        // TODO: kill ui timer
        m_player.Move(direction);

        m_timer = 0;
        m_ballotBox.Reset();
    }

    /// <summary>
    /// Return a random direction number. That is either a 0,1 or 2
    /// </summary>
    /// <returns>number 0,1 or 2</returns>
    private int GetRandomDirection()
    {
        return UnityEngine.Random.Range(0, 3);
    }

    internal void Move(int userIndex, int direction)
    {        
        m_ballotBox.SetUserDirectionVote(userIndex, direction);
    }
}
