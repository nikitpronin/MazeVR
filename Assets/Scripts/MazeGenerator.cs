using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public int width = 15; 
    public int height = 15;

    //http://weblog.jamisbuck.org/2010/12/27/maze-generation-recursive-backtracking
    public Maze GenerateMaze()
    {
        var cells = new MazeGeneratorCell[width, height];

        for (var x = 0; x < cells.GetLength(0); x++)
        {
            for (var y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        for (var x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, height - 1].WallLeft = false;
        }

        for (var y = 0; y < cells.GetLength(1); y++)
        {
            cells[width - 1, y].WallBottom = false;
        }

        RemoveWallsWithBacktracker(cells);

        var maze = new Maze();
        
        maze.cells = cells;
        maze.finishPosition = PlaceMazeExit(cells);  
        
        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        var current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        var stack = new Stack<MazeGeneratorCell>();
        do
        {
            var unvisitedNeighbours = new List<MazeGeneratorCell>();

            var x = current.X;
            var y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                var chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }

    private Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        var furthest = maze[0, 0];

        for (var x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, height - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0) furthest.WallLeft = false;
        else if (furthest.Y == 0) furthest.WallBottom = false;
        else if (furthest.X == width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
        else if (furthest.Y == height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;

        return new Vector2Int(furthest.X, furthest.Y);
    }
}