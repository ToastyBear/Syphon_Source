using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform[] m_spawnPoints;
    public GameObject[] m_aiObjects;
    public float m_spawnRate;
    public List<GameObject> m_aiList = new List<GameObject>();
    List<GameObject> m_toBeSpawned = new List<GameObject>();
    public int m_maxAITotal;
    int m_numberSpawnedAi;
    public int m_round = 0;
    int m_totalRoundAI;
    int m_activeAI;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
        RoundStarter();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(m_toBeSpawned.Count==0 && m_activeAI == 0)   //Need to deduce ailist once ai killed for next round to trigeer
        {
            m_round++;
            RoundStarter();
        }
	}

    void RoundStarter()
    {
        m_aiList.Clear();
        for (int x = 0; x < ((m_round + 1) * 2); x++)
        {
            m_toBeSpawned.Add(m_aiObjects[0]); //Add melles to be spawned list
            if(x < (m_round - 2)) m_toBeSpawned.Add(m_aiObjects[1]); //Add ranged to be spawned list
            //ADD HEALERS
        }
        m_totalRoundAI = m_toBeSpawned.Count;
        m_activeAI = m_totalRoundAI;
        m_numberSpawnedAi = 0;
        print("dsa");

    }

    IEnumerator Spawn ()
    {
        if(m_aiList.Count < m_maxAITotal && m_numberSpawnedAi < m_totalRoundAI)
        {
            Transform chosenSpawn = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)];
            GameObject spawnedAI = m_toBeSpawned[Random.Range(0, m_toBeSpawned.Count)];
            Instantiate(spawnedAI, chosenSpawn.position, chosenSpawn.rotation);
            m_aiList.Add(spawnedAI);
            m_toBeSpawned.Remove(spawnedAI);
            m_numberSpawnedAi++;
        }        
        yield return new WaitForSeconds(m_spawnRate);
        StartCoroutine(Spawn());
    }

    public void RemoveAI(GameObject p_ai)
    {
        m_aiList.Remove(p_ai);
        m_activeAI--;
    }
}
