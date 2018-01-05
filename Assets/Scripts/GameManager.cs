using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Singleton
    public static GameManager instance = null;

    public GameObject m_floor;

    public Maze maze;

    void Awake () {

        if(instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {

        maze = Instantiate(maze);

        maze.Init();

        StartCoroutine(maze.CreateMazeInnerLayoutForLevel());
    }
    

}
