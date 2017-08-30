using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    CharacterController m_charController;
    public float m_speed;
    
    void Start () {
        m_charController = GetComponent<CharacterController>();
	}
	
	
	void Update () {
        //Gravity
        if (!m_charController.isGrounded) m_charController.SimpleMove(new Vector3(0, -9.8f * Time.deltaTime, 0));
        //Controls
        if (Input.GetKey(KeyCode.W)) m_charController.SimpleMove(new Vector3(0, 0, m_speed));
        if (Input.GetKey(KeyCode.S)) m_charController.SimpleMove(new Vector3(0, 0, -m_speed));
        if (Input.GetKey(KeyCode.A)) m_charController.SimpleMove(new Vector3(-m_speed, 0, 0));
        if (Input.GetKey(KeyCode.D)) m_charController.SimpleMove(new Vector3(m_speed, 0, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Arrow(Clone)")
        {
            print("Hit");
            Destroy(other.gameObject);
            //Take Damage
        }
    }
}
