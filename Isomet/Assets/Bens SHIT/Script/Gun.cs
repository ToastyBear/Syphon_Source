using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {
    public enum GunType {Semi,Burst,Auto};
    public GunType gunType;
    public GameObject vaccuum;
    public Transform spawn;
    public Transform bulletSpawnPoint;
    public Rigidbody shell;
    private LineRenderer tracer;


    public float rpm;
    public AudioSource audio;

    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    // Use this for initialization
    void Start () {
        secondsBetweenShots = 60 / rpm;
        audio = GetComponent<AudioSource>();
        if (GetComponent<LineRenderer>()) {
            tracer = GetComponent<LineRenderer>();
        }

        vaccuum.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1)) {
            vaccuum.SetActive(true);
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            vaccuum.SetActive(false);
        }
        //if () {

        //}


    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 20;
            if (Physics.Raycast(ray, out hit))
            {
                shotDistance = hit.distance;
            }
            //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
            nextPossibleShootTime = Time.time + secondsBetweenShots;

            //audio.Play();

            if (tracer) {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
            //Rigidbody newShell = Instantiate(shell, bulletSpawnPoint.position, Quaternion.identity) as Rigidbody;
            //newShell.AddForce(bulletSpawnPoint.forward * Random.Range(150f, 200f) + spawn.forward * Random.Range(-10f, 10f));
        }
    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto) {
            Shoot();
        }
    }

    private bool CanShoot() {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) {
            canShoot = false;
        }
        return canShoot;
    }



    IEnumerator RenderTracer(Vector3 hitpoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitpoint);

        yield return null;
        tracer.enabled = false;
    }

}

