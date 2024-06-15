using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] wayPoints;
    public Transform FinalPoint;
    private Transform target;
    public GameObject Monster;
    public float moveSpeed = 3f; 

    void Start()
    {
     
        target = wayPoints[Random.Range(0, wayPoints.Length)];
       
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, FinalPoint.position, moveSpeed * Time.deltaTime);

    }

    public void MonsterSpawn()
    {
        Monster = Instantiate(Monster, target.position, Quaternion.identity);
    }
}
