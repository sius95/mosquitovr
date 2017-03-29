using UnityEngine;
using System.Collections;

public class Hitzone : MonoBehaviour {

    public bool onHitzone;
    public Animator anim;
    public Collider Hand;
    public Fly fly;

	// Use this for initialization
	void Start () 
    {
        fly = FindObjectOfType<Fly>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(fly.isDead)
        {
            anim.SetBool("OnHitzone", false);
        }
	}

    void OnTriggerEnter(Collider other)
    {
       if(other.transform.tag == "Player")
       {
            onHitzone = true;
            anim.SetBool("OnHitzone", true);
            Hand.enabled = true;
            Hand.isTrigger = true;
       }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            onHitzone = false;
            anim.SetBool("OnHitzone", false);
        }
    }
}
