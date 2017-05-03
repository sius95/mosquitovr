using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Fly : MonoBehaviour 
{
    //public bool isDead;
    //public HealthManager healthManager;
    //public GameObject gameOverScreen;

    public float speed;
    public float moveVelocity;
    public float moveSpeed;
    public bool moveForward;
    private CharacterController controller;
    private GvrViewer gvrViewer;
    public GameObject gvrHead;
    private Transform vrHead;
    public Vector3 forward;
    public Vector3 cameraDirection;
    private Vector3 moveDirection = new Vector3(0,0,1);
    public bool onHitZone;
    public bool playerHurt;

	// Use this for initialization
	void Start () 
    {
        //healthManager = FindObjectOfType<HealthManager>();
	    controller = GetComponent<CharacterController>();
        gvrViewer = GetComponent<GvrViewer>();
        vrHead = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    /*if(Input.GetAxis("Vertical") > 0.1)
        {
            moveForward = !moveForward;
        }*/

        if (Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") <0.1)
        {
            cameraDirection = gvrHead.transform.localPosition;
            if(cameraDirection.y > 0.01)
            {
                moveDirection.y = 1;
            }
            else if(cameraDirection.y < -0.01)
            {
                moveDirection.y = -1;
            }
            else
            {
                moveDirection.y = 0;
            }
            moveVelocity = Input.GetAxis("Vertical");
            moveSpeed = Input.GetAxis("Vertical") * speed;
            forward = vrHead.TransformDirection(moveDirection);
            controller.Move(forward*moveSpeed);
        }

        //if (isDead || healthManager.GetHealth() == 0 )
            //gameOverScreen.SetActive(true);
	}

/*    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Hitzone")
        {
            onHitZone = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Hitzone")
        {
            onHitZone = false;
        }
    }*/
}
