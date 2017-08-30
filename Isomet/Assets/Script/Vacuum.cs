using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour {

    public GameObject PullOBJ;
    public float ForceSpeed = 30f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {

        if (coll.gameObject.tag == ("Pullable"))
        {
            PullOBJ = coll.gameObject;

            PullOBJ.transform.position = Vector3.MoveTowards
                (PullOBJ.transform.position,
                 transform.position,
                 ForceSpeed * Time.deltaTime);
        }

    }
}
