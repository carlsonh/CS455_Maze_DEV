using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Gridder : MonoBehaviour
{
    public int mazeWidth;
    public int mazeHeight;
    GridLevel maze;
    public Location startCell = new Location(0,0);


    MeshFilter meshFilter;


    // Start is called before the first frame update
    void Start()
    {
        maze = new GridLevel(mazeWidth,mazeHeight);
        //maze.startAt(new Location(0,0));
        generateMaze(maze, startCell);

    }

    ///From bslease
    ///     https://github.com/bslease/Procedural_Content/blob/93621be58df2e6f48a66ef0ff5b565b1191fb91f/Procedural%20Content%20Project/Assets/MazeMaker.cs#L139
    void generateMaze(Level level, Location start)
    {
        // a stack of locations we can branch from
        Stack<Location> locations = new Stack<Location>();
        locations.Push(start);
        level.startAt(start);

        while (locations.Count > 0)
        {
            Location current = locations.Peek();

            // try to connect to a neighboring location
            Location next = level.makeConnection(current);
            if (next != null)
            {
                // if successful, it will be our next iteration
                locations.Push(next);
            }
            else
            {
                locations.Pop();
            }
        }
    }


    void GenerateHoudiniData()
    {
        
    }

    ///From bslease
    ///     https://github.com/bslease/Procedural_Content/blob/93621be58df2e6f48a66ef0ff5b565b1191fb91f/Procedural%20Content%20Project/Assets/MazeMaker.cs
    void Update()
    {
         for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Connections currentCell = maze.cells[x, y];
                if (maze.cells[x, y].bIsInMaze)
                { 
                    Vector3 cellPos = new Vector3(x, 0, y);
                    float lineLength = 1f;
                    if (currentCell.directions[0])
                    {
                        // positive x
                        Vector3 neighborPos = new Vector3(x + lineLength, 0, y);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                    if (currentCell.directions[1])
                    {
                        // positive y
                        Vector3 neighborPos = new Vector3(x, 0, y + lineLength);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                    if (currentCell.directions[2])
                    {
                        // negative y
                        Vector3 neighborPos = new Vector3(x, 0, y - lineLength);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                    if (currentCell.directions[3])
                    {
                        // negative x
                        Vector3 neighborPos = new Vector3(x - lineLength, 0, y);
                        Debug.DrawLine(cellPos, neighborPos, Color.cyan);
                    }
                }
            }
        }
    }
    
}

