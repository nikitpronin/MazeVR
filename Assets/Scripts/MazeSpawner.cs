using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeSpawner : MonoBehaviour
{
    public static MazeSpawner instance;
    public Cell cellPrefab;
    public Vector3 cellSize = new Vector3(1,1,0);
    public HintRenderer hintRenderer;

    public NavMeshSurface navMeshSurface;
    public Maze maze;
    
    public Transform player;
    public Transform mazeCells;
    private void Start()
    {
        instance = this;
        player.localPosition = new Vector3(5f, 1.4f, 5f);
        player.localRotation = Quaternion.identity;
        var generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (var x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (var y = 0; y < maze.cells.GetLength(1); y++)
            {
                var c = Instantiate(cellPrefab, new Vector3(x * cellSize.x, y * cellSize.y, y * cellSize.z), Quaternion.identity, mazeCells);
                
                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }

        hintRenderer.DrawPath();

        //Update NavMesh
        navMeshSurface.BuildNavMesh();
    }
}