using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform[] spawnPoints;
    public GameObject aiObject;
    public float spawnRate;


	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    IEnumerator Spawn ()
    {
        Transform chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(aiObject, chosenSpawn.position, chosenSpawn.rotation);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(Spawn());
    }
}
