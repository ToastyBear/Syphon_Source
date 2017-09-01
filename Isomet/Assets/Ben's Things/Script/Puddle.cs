using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

    public GameObject Bullet;
    public Transform PuddleLoc;

    public float Puddle_Death_Timer = 2.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "vacuum")
        {
            
            Instantiate(Bullet, PuddleLoc.position, PuddleLoc.rotation);
            Destroy(gameObject, Puddle_Death_Timer);
            //Debug.Log("Suck me off at the next station");

            gameObject.SetActive(false);
            
        }


    }
}
