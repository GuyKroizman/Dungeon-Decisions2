using UnityEngine;

/// <summary>
/// Detects if the player/user keyboard press on one of the directional keys and then
/// if so then tell the decision master which direction button.
/// </summary>
public class PlayerInput : MonoBehaviour {

    // The keys assigned for this player movment
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode forwardKey;
    
    // The game is for two players/users. we have a set of directional buttons for each player
    // this flag determine whether the set of buttons is for player one or two.
    public bool m_isPlayerOne;

    // for convinience - change the bool from above to an int with value 1 for player one and 2 for player two.
    private int m_userIndex;

    public DecisionMaster m_decisionMaster;

    private void Start()
    {
        m_userIndex = m_isPlayerOne ? 1 : 2;
    }

    int getDirection()
    {
        if (Input.GetKeyDown(leftKey))
            return 1;

        if (Input.GetKeyDown(rightKey))
            return 2;

        if (Input.GetKeyDown(forwardKey))
            return 0;

        return -1;
    }

    void Update ()
    {
        int direction = getDirection();
        if (direction == -1)
            return;

        m_decisionMaster.Move(m_userIndex, direction);
    }
}
