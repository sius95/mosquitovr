using UnityEngine;
using System.Collections;

public class PoisonZone : MonoBehaviour 
{
    public HealthManager healthManager;
    public float decreaseHealth;
    public float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            healthManager.StartDecreaseHealth(decreaseHealth, time);
            //StartCoroutine(alertManager.DecreaseAlertPerSec(decreaseAlert,time));
        }
    }*/

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!healthManager.inPoisonZone)
                healthManager.StartDecreaseHealth(decreaseHealth, time);
            //StartCoroutine(alertManager.DecreaseAlertPerSec(decreaseAlert,time));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            healthManager.StopDecreaseHealth(decreaseHealth, time);
            //StartCoroutine(alertManager.DecreaseAlertPerSec(decreaseAlert,time));
        }
    }
}
