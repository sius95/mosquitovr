using UnityEngine;
using System.Collections;

public class HideZone : MonoBehaviour 
{
    public AlertManager alertManager;
    public int decreaseAlert;
    public float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            alertManager.StartDecreaseAlert(decreaseAlert, time);
            //StartCoroutine(alertManager.DecreaseAlertPerSec(decreaseAlert,time));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            alertManager.StopDecreaseAlert(decreaseAlert, time);
            //StartCoroutine(alertManager.DecreaseAlertPerSec(decreaseAlert,time));
        }
    }
}
