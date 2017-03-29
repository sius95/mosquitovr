using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

    public Fly fly;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other)
    {
        if(other.transform.tag == "Player")
        {
            fly.isDead = true;
        }
    }
}
