using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Maze : MonoBehaviour
{

    // bricks are initiated one next to another to create the maze wall.
    public GameObject m_brick;

    // floor is expected to be a square shape.
    private GameObject m_floor;

    // the dimension of the maze/floor
    float minX, maxX;
    float minZ, maxZ;

    /// <summary>
    /// Initiate a brick somewhere above the floor in x,z coords
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="z">z</param>
    void PutBrick(float x, float z)
    {
        if (x < minX)
            return;
        if (x > maxX)
            return;
        if (z < minZ)
            return;
        if (z > maxZ)
            return;

        float BrickInstantiationHeight = 5;

        x += m_brick.transform.localScale.x / 2;
        z += m_brick.transform.localScale.z / 2;
        Instantiate(m_brick, new Vector3(x, BrickInstantiationHeight, z), Quaternion.identity);
    }

    /// <summary>
    /// Create the walls (out of bricks) around the edges of the floor.
    /// </summary>
    void CreateMazeOuterWalls()
    {
        BuildNorthWall();
        BuildWestWallSkipFirstBlockWhichIsPartOfNorthWall();
        BuildSouthWallSkipFirstBlockWhichIsPartOfWestWall();
        BuildEastWallSkipFirstAndLastBricksWhichAlreadyBuilt();
    }

    internal void SetFloor(GameObject floorClone)
    {
        m_floor = floorClone;
    }

    private void BuildEastWallSkipFirstAndLastBricksWhichAlreadyBuilt()
    {
        for (float i = minZ + 1; i < maxZ - 1; i++)
        {
            PutBrick(maxX - 1, i);
        }
    }

    private void BuildSouthWallSkipFirstBlockWhichIsPartOfWestWall()
    {
        for (float i = minX + 1; i < maxX; i++)
        {
            PutBrick(i, maxZ - 1);
        }
    }

    private void BuildWestWallSkipFirstBlockWhichIsPartOfNorthWall()
    {
        for (float i = minZ + 1; i < maxZ; i++)
        {
            PutBrick(minX, i);
        }
    }

    private void BuildNorthWall()
    {
        for (float i = minX; i < maxX; i++)
        {
            PutBrick(i, minZ);
        }
    }


    public void Init()
    {

        m_floor = GameObject.Find("Floor");

        Vector3 floorSize = m_floor.transform.localScale;

        // the center of the floor is 0,0 (and not some corner as some might expect)
        minX = -1.0f * floorSize.x / 2.0f;
        maxX = floorSize.x / 2.0f;

        minZ = -1.0f * floorSize.z / 2.0f;
        maxZ = floorSize.z / 2.0f;

        CreateMazeOuterWalls();
    }

    // TODO: pass a variable holding the level
    public IEnumerator CreateMazeInnerLayoutForLevel()
    {

        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        int[,] layout = { };

        if (currentScene.name == "Level1")
            layout = GetLevel1Layout();

        if (currentScene.name == "Level2")
            layout = GetLevel2Layout();

        for (int x = (int)minX; x < maxX; x++)
        {
            if (x + Math.Abs(minX) > layout.GetLength(0))
                continue;

            for (int z = (int)minZ; z < maxZ; z++)
            {
                if (z + Math.Abs(minZ) > layout.GetLength(1))
                    continue;

                if (layout[x + (int)Math.Abs(minX), z + (int)Math.Abs(minZ)] == 1)
                {
                    PutBrick(x, z);
                    yield return new WaitForSeconds(0.3f);
                }

            }
        }
    }

    private static int[,] GetLevel1Layout()
    {
        return new int[,]
                {
                    {0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0 },
                    {0, 0, 0, 0, 0 }
                };
    }

    private static int[,] GetLevel2Layout()
    {
        return new int[,]
                {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0 },
            {0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0 },
            {0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0 },
            {0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0 },
            {0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0 },
            {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0 },
            {0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0 },
            {0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            {0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 0 },
            {0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0 },
            {0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                };
    }
}
