using UnityEngine;

public class Maze : MonoBehaviour {

    public GameObject brick;

    // floor is expected to be a square shape.
    public GameObject floor;

    float minX;
    float maxX;
    float minZ;
    float maxZ;


    void putBrick(float x, float z)
    {
        x += brick.transform.localScale.x / 2;
        z += brick.transform.localScale.z / 2;
        Instantiate(brick, new Vector3(x, 1, z), Quaternion.identity);
    }

    void createMazeOuterWalls()
    {
        buildNorthWall();
        buildWestWallSkipFirstBlockWhichIsPartOfNorthWall();
        buildSouthWallSkipFirstBlockWhichIsPartOfWestWall();
        buildEastWallSkipFirstAndLastBricksWhichAlreadyBuilt();
    }

    private void buildEastWallSkipFirstAndLastBricksWhichAlreadyBuilt()
    {
        for (float i = minZ + 1; i < maxZ - 1; i++)
        {
            putBrick(maxX - 1, i);
        }
    }

    private void buildSouthWallSkipFirstBlockWhichIsPartOfWestWall()
    {
        for (float i = minX + 1; i < maxX; i++)
        {
            putBrick(i, maxZ - 1);
        }
    }

    private void buildWestWallSkipFirstBlockWhichIsPartOfNorthWall()
    {
        for (float i = minZ + 1; i < maxZ; i++)
        {
            putBrick(minX, i);
        }
    }

    private void buildNorthWall()
    {
        for (float i = minX; i < maxX; i++)
        {
            putBrick(i, minZ);
        }
    }

    void Start () {
        
        Vector3 floorSize = floor.transform.localScale;

        // the center of the floor is 0,0 (and not some corner as some might expect)
        minX = -1.0f * floorSize.x / 2.0f;
        maxX = floorSize.x / 2.0f;

        minZ = -1.0f * floorSize.z / 2.0f;
        maxZ = floorSize.z / 2.0f;

        createMazeOuterWalls();
    }
	
	
	void Update () {
	
	}
}
