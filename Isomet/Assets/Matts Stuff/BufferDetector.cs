using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferDetector : MonoBehaviour {

    public List<GameObject> m_buffedAI = new List<GameObject>();

    void Start()
    {
        m_buffedAI.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            m_buffedAI.Add(other.gameObject);
            other.GetComponent<AI>().m_damage = transform.parent.GetComponent<AI>().m_dmgBuff * other.GetComponent<AI>().m_damage;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            m_buffedAI.Remove(other.gameObject);
            other.GetComponent<AI>().m_damage *= transform.parent.GetComponent<AI>().m_dmgBuff / other.GetComponent<AI>().m_damage;
        }
    }
}
