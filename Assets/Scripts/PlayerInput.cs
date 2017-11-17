using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    // The keys assigned for this player movment
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode forwardKey;

    public Player m_player;

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
	
	// Update is called once per frame
	void Update () {
        int direction = getDirection();
        m_player.Move(direction);

    }
}
