using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode forwardKey;

    public Player player;

    // Use this for initialization
    void Start () {
        //player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(leftKey))
        {
            player.move(1);
        }
        if (Input.GetKeyDown(rightKey))
        {
            player.move(2);
        }

        if (Input.GetKeyDown(forwardKey))
        {
            player.move(0);
        }
    }
}
