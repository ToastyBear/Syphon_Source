using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    float m_lifeTime = 5.0f;
    float m_time;
    float m_speed = 0.2f;
    public float m_damage;

	void FixedUpdate()
    {
        //Destroy arrow if it reaches life time
        m_time += Time.deltaTime;
        if (m_time > m_lifeTime) Destroy(gameObject);
        transform.Translate(0, 0, m_speed);
    }

    
}
