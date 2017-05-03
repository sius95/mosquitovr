using UnityEngine;
using System.Collections;

public class ShooZone : MonoBehaviour {

    public bool onShooZone;
    public Animator anim;
    public float shooTime;
    public float countingTime;
    public AlertManager alertManager;
    public int increaseAlertEnter;
    public int increaseAlertStay;
    public float alertTime;
    public float alertCountingTime;
    //public Collider Hand;
    //public Collider lArm;
    //public Collider uArm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //countingTime = shooTime;
            alertCountingTime = alertTime;
            onShooZone = true;
            alertManager.IncreaseAlert(increaseAlertEnter);
            //anim.SetBool("OnShooZone", true);
            //Hand.enabled = true;
            //Hand.isTrigger = true;
            //lArm.enabled = true;
            //lArm.isTrigger = true;
            //lArm.enabled = true;
            //lArm.isTrigger = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            countingTime += Time.deltaTime;
            if(countingTime >= shooTime)
            {
                alertCountingTime -= Time.deltaTime;
                if(alertCountingTime <= 1.0f)
                {
                    alertManager.IncreaseAlert(increaseAlertStay);
                    alertCountingTime = alertTime;
                }
                anim.SetBool("OnShooZone", true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            onShooZone = false;
            alertCountingTime = 0;
            countingTime = 0;
            //countingTime = shooTime;
            anim.SetBool("OnShooZone", false);
        }
    }
}
