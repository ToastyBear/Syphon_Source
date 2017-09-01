using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    //Adjustable Variables
    public float m_range;
    public float m_speed;
    public float m_attackSpeed;
    public float m_rotationSpeed;
    public float m_health;
    public float m_damage;
    public float m_dmgBuff;
    public bool m_isRanged;
    public bool m_isBuffer;

    //Object Variables
    public Transform m_target;
    public GameObject m_arrow;
    bool m_canAttack = true;
    NavMeshAgent m_navAgent;
    public Spawner m_spawnScript;

    void Start() {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_target = GameObject.Find("Player").transform;  //Find Betterway
        m_spawnScript = GameObject.Find("SpawnParent").GetComponent<Spawner>();
        m_navAgent.destination = m_target.position;
        m_navAgent.stoppingDistance = m_range;
        m_navAgent.speed = m_speed;
        InvokeRepeating("BehaviourController", 0.02f, 0.02f);
    }

    float FindDistance()
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (m_navAgent.enabled) m_navAgent.CalculatePath(m_target.position, path);
        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;
        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = m_target.position;
        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++) allWayPoints[i + 1] = path.corners[i];
        // Create a float to store the path length that is by default 0.
        float pathLength = 0;
        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++) pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        return pathLength;
    }

    void BehaviourController() {        
        //Attacking
        if (FindDistance() > m_range)
        {
            m_navAgent.destination = m_target.position;
            LookAtPlayer();
        }
        //Kiting
        else if (FindDistance() < m_range && m_isRanged)
        {
            if ((m_target.position - transform.localPosition).normalized.z > 0) transform.Translate((m_target.position - transform.localPosition).normalized * -0.03f);
            else if ((m_target.position - transform.localPosition).normalized.z < 0) transform.Translate((m_target.position - transform.localPosition).normalized * 0.03f);
            else if ((m_target.position - transform.localPosition).normalized.z == 0) transform.Translate(0, 0, 0);
            LookAtPlayer();
        }
        //Death
        if(m_health <= 0)
        {
            Die();
        }
    }

    void LookAtPlayer()
    {
        //Look at player when in distants
        Vector3 direction = (m_target.position - transform.localPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * m_rotationSpeed);
        //Check if we can attack
        if (m_canAttack)
        {
            //If character visible attack               ///----MAKE EFFICEITN---///
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, m_range))
                if (hit.collider.name == "Player") StartCoroutine(Attack());
        }
    } 

    IEnumerator Attack()
    {
        print("BeginAttack");
        m_canAttack = false;
        GameObject arrowSpawned;
        if (m_isRanged) arrowSpawned = Instantiate(m_arrow, transform.position, transform.localRotation);
        else
        {
            print("Swing");
            arrowSpawned = Instantiate(m_arrow, transform.position, transform.localRotation);
        }
        arrowSpawned.GetComponent<Arrow>().m_damage = m_damage;
        yield return new WaitForSeconds(m_attackSpeed);
        m_canAttack = true;
    }

    void Die()
    {
        m_spawnScript.RemoveAI(gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)") m_health -= 5.0f;
    }
   

    
}
