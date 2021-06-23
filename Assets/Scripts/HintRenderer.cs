using System.Collections.Generic;
using UnityEngine;

public class HintRenderer : MonoBehaviour
{
    public MazeSpawner mazeSpawner;

    private LineRenderer _componentLineRenderer;

    private void Start()
    {
        
        _componentLineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawPath()
    {
        var maze = mazeSpawner.maze;
        var x = maze.finishPosition.x;
        var y = maze.finishPosition.y;
        var positions = new List<Vector3>();

        while ((x != 0 || y != 0) && positions.Count < 10000)
        {
            positions.Add(new Vector3(x * mazeSpawner.cellSize.x, y * mazeSpawner.cellSize.y, y * mazeSpawner.cellSize.z));

            var currentCell = maze.cells[x, y];

            if (x > 0 &&
                !currentCell.WallLeft &&
                maze.cells[x - 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x--;
            }
            else if (y > 0 &&
                !currentCell.WallBottom &&
                maze.cells[x, y - 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y--;
            }
            else if (x < maze.cells.GetLength(0) - 1 &&
                !maze.cells[x + 1, y].WallLeft &&
                maze.cells[x + 1, y].DistanceFromStart < currentCell.DistanceFromStart)
            {
                x++;
            }
            else if (y < maze.cells.GetLength(1) - 1 &&
                !maze.cells[x, y + 1].WallBottom &&
                maze.cells[x, y + 1].DistanceFromStart < currentCell.DistanceFromStart)
            {
                y++;
            }
        }

        positions.Add(Vector3.zero);
        _componentLineRenderer.positionCount = positions.Count;
        _componentLineRenderer.SetPositions(positions.ToArray());
    }
}