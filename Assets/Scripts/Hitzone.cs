using UnityEngine;
using System.Collections;

public class Hitzone : MonoBehaviour {

    public bool onHitzone;
    public Animator anim;
    //public Collider Hand;
    public Fly fly;
    public HealthManager healthManager;
    public TextMesh notification;
    public RhythmMiniGame rhythmMiniGame;
    public AlertManager alertManager;
	// Use this for initialization
	void Start () 
    {
        fly = FindObjectOfType<Fly>();
        healthManager = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(healthManager.isDead)
        {
            rhythmMiniGame.StopMiniGame();
            rhythmMiniGame.gameObject.SetActive(false);
            anim.SetBool("OnHitzone", false);
        }
        if(onHitzone && alertManager.GetAlert() >= 100 && !healthManager.isDead)
        {
            //fly.enabled = true;
            anim.SetBool("OnHitzone", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (onHitzone)
            {
                Debug.Log("Fire1 Button Pressed");
                if (!rhythmMiniGame.miniGameStart)
                {
                    notification.text = "";
                    rhythmMiniGame.gameObject.SetActive(true);
                    fly.enabled = false;
                    rhythmMiniGame.StartMiniGame();
                }
                else if (rhythmMiniGame.miniGameStart)
                {
                    //rhythmMiniGame.miniGameStart = false;
                    fly.enabled = true;
                    notification.text = "Press Fire Button to Start Sucking Blood";
                    rhythmMiniGame.StopMiniGame();
                    rhythmMiniGame.gameObject.SetActive(false);
                }
            }
        }

	}

    void OnTriggerEnter(Collider other)
    {
       if(other.transform.tag == "Player")
       {
           onHitzone = true;
           notification.text = "Press Fire Button to Start Sucking Blood";
           //anim.SetBool("OnHitzone", true);
           //Hand.enabled = true;
           //Hand.isTrigger = true;
       }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump Button Pressed");
                if (!rhythmMiniGame.miniGameStart)
                {
                    //rhythmMiniGame.gameObject.SetActive(true);
                    fly.enabled = false;
                    //rhythmMiniGame.StartMiniGame();
                }
                else if (rhythmMiniGame.miniGameStart)
                {
                    //rhythmMiniGame.miniGameStart = false;
                    rhythmMiniGame.StopMiniGame();
                }
            }
            if(Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire1 Button Pressed");
                if (rhythmMiniGame.miniGameStart)
                {
                    //rhythmMiniGame.miniGameStart = false;
                    fly.enabled = true;
                    //rhythmMiniGame.StopMiniGame();
                }
            }
        }
    }*/

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            notification.text = "";
            onHitzone = false;
            anim.SetBool("OnHitzone", false);
        }
    }
}
