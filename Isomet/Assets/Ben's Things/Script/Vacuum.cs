using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour {

    public GameObject PullOBJ;
    public List<GameObject> items;
    public float ForceSpeed = 30f;

   // bool isActive = false;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

       // if (isActive == true){ 

            //PullOBJ.transform.position = Vector3.MoveTowards
            //(PullOBJ.transform.position,
            // transform.position,
            // ForceSpeed * Time.deltaTime);
        // }
    }

    void OnTriggerEnter(Collider coll)
    {

        if (coll.gameObject.tag == ("Pullable"))
        {
            //if (!items.Contains(coll.gameObject))
            //{
            //    items.Add(coll.gameObject);
            //}


            PullOBJ = coll.gameObject;
           // InvokeRepeating("PullMeDaddy", 0f, 1f);
            PullOBJ.transform.position = Vector3.MoveTowards
                (PullOBJ.transform.position,
                 transform.position,
                 ForceSpeed * Time.deltaTime);
            // Debug.Log("the vac sees that bullet");
        }
    }

    void PullMeDaddy(){

             PullOBJ.transform.position = Vector3.MoveTowards
            (PullOBJ.transform.position,
             transform.position,
             ForceSpeed * Time.deltaTime);

    }
}
