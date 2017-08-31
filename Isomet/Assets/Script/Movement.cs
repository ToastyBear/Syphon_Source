using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    // Use this for initialization

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits
    public Slider healthBar;
    public GameObject playerModel;
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;

    public int bulletBloodLoss = 1;
    public int dashBloodLoss = 1;
    public int playerHealth = 100;


    private Quaternion targetRotation;
    public Gun gun;


    private CharacterController controller;
    private Camera cam;
    public Transform projectile;
    public float bulletSpeed;
    public Transform clone;


    public Vector3 moveDirection;
    //public float maxDashTime = 1.0f;
    public float dashSpeed = 20.0f;
    //public float dashStoppingSpeed = 0.1f;
    //private float currentDashTime;
    public float dashTimer = 0.2f;



    public Vector2 savedVelocity;
    void Start() {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        //currentDashTime = maxDashTime;
        //clone = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //control1();
        controlMouse();
        if (Input.GetMouseButtonDown(0))
        {
            playerHealth = playerHealth - bulletBloodLoss;
        }
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    gun.Shoot();

        //    //    clone = Instantiate(projectile, transform.position, transform.rotation);
        //    //    //clone.rigidbody.AddForce(clone.transform.forward * shootForce);
        //}
        //else if (Input.GetButton("Fire1")) gun.ShootContinuous();
        healthBar.value = playerHealth;
    }

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}
           
     

    

    void control1() {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }
       Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;
        controller.Move(motion * Time.deltaTime);
    }

    void controlMouse() {

        //Vector3 mousePos = Input.mousePosition;
        //mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        //targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x,0,transform.position.z));
        //transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        
            // Cast a ray from the camera to the mouse cursor
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If the ray strikes an object...
            if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                // ...and if that object is the ground...
                if (cameraRayHit.transform.tag == "Ground" || cameraRayHit.transform.tag == "Enemy" || cameraRayHit.transform.tag == "Wall")
                {
                    // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                    Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                    transform.LookAt(targetPosition);
                }
            }
        



        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        if (Input.GetKeyDown("r"))
        {
            //transform.position += new Vector3(dashSpeed * Time.deltaTime,0.1f,0f);
            //currentDashTime = 0.0f;
            Debug.Log(moveDirection);
            moveDirection = controller.transform.forward / 5 * dashSpeed;
            playerHealth -= dashBloodLoss;
            StartCoroutine("DashFade");
            controller.Move(moveDirection);
            //moveDirection = moveDirection * dashSpeed;
            Debug.Log(moveDirection);
        }
        //if (currentDashTime < maxDashTime)
        //{
        //    moveDirection = new Vector3(0, 0, dashSpeed);
        //    //moveDirection = controller.transform.forward;
        //    currentDashTime += dashStoppingSpeed;
        //    //controller.Move(moveDirection + motion * Time.deltaTime);
        //}
        //else
        //{
        //    //moveDirection = new Vector3.zero;
        //}
        //controller.Move(moveDirection + motion * Time.deltaTime);

        controller.Move(motion * Time.deltaTime);

    }
    IEnumerator DashFade()
    {
        playerModel.SetActive(false);
        yield return new WaitForSeconds(dashTimer);
        playerModel.SetActive(true);
    }

        void shoot() {
     

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            playerHealth -= 10;
        }
    }
}
