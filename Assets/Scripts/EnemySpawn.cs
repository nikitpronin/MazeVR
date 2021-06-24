using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    public MazeSpawner mazeSpawner;
    private int _xPos;
    private int _zPos;

    public Transform enemies;

    private int _enemyCount = 0;
    
    void Start()
    {
        StopCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (_enemyCount < 15)
        {
            _xPos = Random.Range(1, mazeSpawner.maze.cells.GetLength(0))+5;
            _zPos = Random.Range(1, mazeSpawner.maze.cells.GetLength(1))-5;

            Instantiate(enemy, new Vector3(_xPos, 0, _zPos), Quaternion.identity, enemies);
        }

        yield return new WaitForSeconds(3);
        _enemyCount++;
    }
}
