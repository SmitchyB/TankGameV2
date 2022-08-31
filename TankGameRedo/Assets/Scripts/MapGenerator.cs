using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public bool mapOfTheDay;
    public int mapSeed;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        if (mapOfTheDay)
        {
            Random.InitState(DateToInt(System.DateTime.Now));
        }
        else if (mapSeed != 0)
        {
            Random.InitState(mapSeed);
        }
        else
        {
            DateToIntExact(System.DateTime.Now);
        }
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int DateToInt(System.DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day;
    }

    public int DateToIntExact(System.DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    // Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        // Clear out the grid - "column" is our X, "row" is our Y
        grid = new Room[cols, rows];

        // For each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            // for each column in that row
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // Figure out the location. 
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + "," + currentRow;

                // Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                if (currentRow == 0)
                {
                    Destroy(tempRoom.doorNorth);
                }
                else if (currentRow == rows - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    Destroy(tempRoom.doorSouth);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }

                if (currentCol == 0)
                {
                    Destroy(tempRoom.doorEast);
                }
                else if (currentCol == cols - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    Destroy(tempRoom.doorWest);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    Destroy(tempRoom.doorEast);
                    Destroy(tempRoom.doorWest);
                }

                // Save it to the grid array
                grid[currentCol, currentRow] = tempRoom;
            }
        }
    }
}
