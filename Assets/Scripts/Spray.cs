using UnityEngine;
using System.Collections;

public class Spray : MonoBehaviour {

    public GameObject parentBone;

    public GameObject spraySmoke;
    public GameObject smoke;

    public GameObject[] poisonZone;
    public Vector3 firstPosition;
    public Quaternion firstRotation;
    public Transform firstParent;

    public AlertManager alertManager;
    public int decreaseAlert;

    public float poisonTime;

    public GameObject poisonIcon;
	// Use this for initialization
	void Start () 
    {
        firstParent = transform.parent;
        firstPosition = transform.position;
        firstRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PickUpSpray()
    {
        transform.parent = parentBone.transform;
        transform.position = parentBone.transform.position;
        transform.rotation = parentBone.transform.rotation;
    }

    public void Spraying()
    {
        smoke.SetActive(true);
        smoke.GetComponent<ParticleSystem>().Play();
        //Instantiate(smoke, spraySmoke.transform.position, spraySmoke.transform.rotation);
        StartCoroutine(PoisonStart(poisonTime));
    }

    public void PutDownSpray()
    {
        transform.parent = firstParent;
        transform.position = firstPosition;
        transform.rotation = firstRotation;
        alertManager.DecreaseAlert(decreaseAlert);
    }

    public IEnumerator PoisonStart(float time)
    {
        poisonIcon.SetActive(true);
        for(int i=0;i<poisonZone.Length;i++)
        {
            poisonZone[i].SetActive(true);
        }
        yield return new WaitForSeconds(time);
        for (int i = 0; i < poisonZone.Length; i++)
        {
            poisonZone[i].SetActive(false);
        }
        poisonIcon.SetActive(false);
    }
}
