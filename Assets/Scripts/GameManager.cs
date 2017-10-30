using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Singleton
    public static GameManager instance = null;

    public Maze maze;

    public GameObject floor;
    GameObject floorClone;


    void Awake () {

        if(instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        floorClone = Instantiate(floor);

        maze = GetComponent<Maze>();

        InitGame();
	}

    private void InitGame()
    {
        maze.CreateMazeInnerLayoutForLevel();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
