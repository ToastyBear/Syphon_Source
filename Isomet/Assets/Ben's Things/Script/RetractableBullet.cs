using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RetractableBullet : MonoBehaviour {


    public float blood_death_timer = 2.0f;

    // Use this for initialization
    void Start () {

    

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Player")
        {
            
            Destroy(gameObject, blood_death_timer);
            gameObject.SetActive(false);

        }

        }
    }
