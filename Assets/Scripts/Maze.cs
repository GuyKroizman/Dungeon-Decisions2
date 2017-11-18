using System;
using UnityEngine;

public class Maze : MonoBehaviour {

    // bricks are initiated one next to another to create the maze wall.
    public GameObject brick;

    // floor is expected to be a square shape.
    private GameObject floor;

    // the dimension of the the maze/floor
    float minX, maxX;
    float minZ, maxZ;

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

        x += brick.transform.localScale.x / 2;
        z += brick.transform.localScale.z / 2;
        Instantiate(brick, new Vector3(x, 1, z), Quaternion.identity);
    }

    void CreateMazeOuterWalls()
    {
        BuildNorthWall();
        BuildWestWallSkipFirstBlockWhichIsPartOfNorthWall();
        BuildSouthWallSkipFirstBlockWhichIsPartOfWestWall();
        BuildEastWallSkipFirstAndLastBricksWhichAlreadyBuilt();
    }

    internal void setFloor(GameObject floorClone)
    {
        floor = floorClone;
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

    public void Init () {
        
        Vector3 floorSize = floor.transform.localScale;

        // the center of the floor is 0,0 (and not some corner as some might expect)
        minX = -1.0f * floorSize.x / 2.0f;
        maxX = floorSize.x / 2.0f;

        minZ = -1.0f * floorSize.z / 2.0f;
        maxZ = floorSize.z / 2.0f;

        CreateMazeOuterWalls();
    }

    // TODO: pass a variable holding the level
    public void CreateMazeInnerLayoutForLevel()
    {
        
        int [,] layout = new int[,]
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

        for(int x = (int)minX; x < maxX; x++)
        {
            if (x + Math.Abs(minX)> layout.GetLength(0))
                continue;

            for (int z = (int)minZ; z<maxZ; z++)
            {
                if (z + Math.Abs(minZ) > layout.GetLength(1))
                    continue;

                if(layout[x + (int)Math.Abs(minX), z+(int)Math.Abs(minZ)] == 1)
                    PutBrick(x, z);
            }
        }
    }

}
