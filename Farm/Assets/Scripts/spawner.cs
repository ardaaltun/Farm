using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawnPointParent;
    public Transform[] spawnPoints;
    public int counter = 0;
    public int killed = 0;
    public static int finalScore = 0;
    bool spawned = false;
    void Start()
    {
        spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        finalScore = killed;
        if (!spawned && counter <= 30)
        {
            Invoke("Spawn", 1f);
            spawned = true;
        }

        if (!spawned && counter > 30)
        {
            Invoke("Spawn", 10f);
            spawned = true;
        }
    }
    void Spawn()
    {
        //print(counter);
        counter++;
        
        spawned = false;
        System.Random rand = new System.Random();
        //print(rand.Next(spawnPoints.Length));
        GameObject intedEnemy = Instantiate(enemy);
        //intedEnemy.transform.position = spawnPoints[rand.Next(spawnPoints.Length)].position;
        intedEnemy.GetComponent<NavMeshAgent>().Warp(spawnPoints[rand.Next(spawnPoints.Length)].position);

        //intedEnemy.transform.position = spawnPoints[0].position;
    }
}
