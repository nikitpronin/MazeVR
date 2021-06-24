using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vampire : MonoBehaviour
{
    private NavMeshAgent _enemyNavMesh;

    private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _enemyNavMesh = GetComponent<NavMeshAgent>();
        if(!_player){
            Debug.Log("Make sure your player is tagged!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _enemyNavMesh.destination = _player.position;
    }
}
