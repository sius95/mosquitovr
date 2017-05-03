using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

    public Fly fly;
    public Hitzone hitZone;
    public ShooZone shooZone;
    public HealthManager healthManager;

	// Use this for initialization
	void Start () 
    {
        fly = FindObjectOfType<Fly>();
        hitZone = FindObjectOfType<Hitzone>();
        shooZone = FindObjectOfType<ShooZone>();
        healthManager = FindObjectOfType<HealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other)
    {
        if(other.transform.tag == "Player")
        {
            if(hitZone.onHitzone)
                healthManager.isDead = true;
            if (shooZone.onShooZone && !fly.playerHurt)
                healthManager.DecreaseHealth(1);
        }
    }
}
