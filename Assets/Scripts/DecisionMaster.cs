using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collects players vote for direction and move the player accordingly. 
/// That is if the player voted for the same direction move to that direction. Otherwise
/// random direction.
/// Turn ended and players did not chose a direction -> move player in random direction
/// 
/// </summary>
public class DecisionMaster : MonoBehaviour {

    public Player m_player;

    // The time for each turn.
    // TODO: Note There is also the time it takes for the player to do its movement (turn or forward). But we don't take 
    // that into account in the logic below. need to be fixed.
    public float m_usersTurnDurationSeconds;

    private bool m_levelEnded = false;
    internal void EndLevelStop()
    {
        m_levelEnded = true;
    }

    private AudioSource m_audioSource;

    BallotBox m_ballotBox;

    private float m_timer = 0;
       
    /// <summary>
    /// Collects users direction votes and provide the results
    /// </summary>
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
            {
                Debug.Log("Reject user direction vote because player is still moving.");
                return;
            }

            if (userIndex == 1)
                m_user1DirectionDecision = direction;

            if (userIndex == 2)
                m_user2DirectionDecision = direction;
        }
    }

    public void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();    
    }

    private void Start()
    {
        m_ballotBox = new BallotBox();
    }

    void Update () {
        if (m_levelEnded)
            return;

        m_timer += Time.deltaTime;
        m_ballotBox.UpdateTime();

        // if time is up.
        if(m_timer > m_usersTurnDurationSeconds)
        {
            // time is up and users did not decide anything or only one of the users decided.
            MovePlayer(GetRandomDirection());
            Debug.Log("Time is up.");
        }

        if (m_ballotBox.IsBothUsersHaveAlreadyDecided())
        {

            if (m_ballotBox.IsTwoUsersVotedToMoveInTheSameDirection())
            {
                MovePlayer(m_ballotBox.GetFinalDirection());
                m_audioSource.Play();
                Debug.Log("Synergy!");
            }
            else
            {
                MovePlayer(GetRandomDirection());
                Debug.Log("Indecision is your enemy.");
            }
        }
    }

    private void MovePlayer(int direction)
    {
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
