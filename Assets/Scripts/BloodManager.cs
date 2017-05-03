using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloodManager : MonoBehaviour 
{
    private int initalBlood;
    private int blood;
    private TextMesh bloodText;

	// Use this for initialization
	void Start () 
    {
        initalBlood = 0;
        blood = initalBlood;
        bloodText = GetComponent<TextMesh>();
        bloodText.text = blood.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
        bloodText.text = blood.ToString();
	}

    public void IncreaseBlood(int pointToIncrease)
    {
        blood += pointToIncrease;
    }
}
