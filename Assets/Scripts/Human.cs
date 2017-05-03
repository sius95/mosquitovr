using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour 
{
    public Spray spray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PickUpSpray()
    {
        spray.PickUpSpray();
    }

    public void Spraying()
    {
        spray.Spraying();
    }

    public void PutDownSpray()
    {
        spray.PutDownSpray();
    }
}
