using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodProjectile : MonoBehaviour
{

    public GameObject Blood_Emitter;

    public GameObject BloodBullet;

    public float Blood_Forward_Force;

    public float blood_death_timer = 5.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject Temp_Blood_Handler;
            Temp_Blood_Handler = Instantiate(BloodBullet, Blood_Emitter.transform.position, Blood_Emitter.transform.rotation) as GameObject;

            //Temp_Blood_Handler.transform.Rotate(Vector3.left * 90);

            Rigidbody Temp_RigidBody;
            Temp_RigidBody = Temp_Blood_Handler.GetComponent<Rigidbody>();

            Temp_RigidBody.AddForce(transform.forward * Blood_Forward_Force);

            Destroy(Temp_Blood_Handler, blood_death_timer);


        }
    }
}

