using UnityEngine;
using System.Collections;

public class RhythmGameButton : MonoBehaviour 
{
    public int whatButton;
    private SpriteRenderer sprite;
    public RhythmMiniGame rhythmMiniGame;

    // Use this for initialization
	void Start () 
    {
        sprite = GetComponent<SpriteRenderer>();
        rhythmMiniGame = GetComponentInParent<RhythmMiniGame>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetAxis("Vertical") > 0.1)
        {
            if(whatButton == 1)
            {
                sprite.color = Color.green;
                rhythmMiniGame.ButtonSuccess();
            }
            else
            {
                sprite.color = Color.red;
                rhythmMiniGame.ButtonFailed();
            }
            this.enabled = false;
        }

        if(Input.GetAxis("Vertical") < -0.1)
        {
            if (whatButton == -1)
            {
                sprite.color = Color.green;
                rhythmMiniGame.ButtonSuccess();
            }
            else
            {
                sprite.color = Color.red;
                rhythmMiniGame.ButtonFailed();
            }
            this.enabled = false;
        }

        if (Input.GetAxis("Horizontal") > 0.1)
        {
            if (whatButton == 2)
            {
                sprite.color = Color.green;
                rhythmMiniGame.ButtonSuccess();
            }
            else
            {
                sprite.color = Color.red;
                rhythmMiniGame.ButtonFailed();
            }
            this.enabled = false;
        }

        if (Input.GetAxis("Horizontal") < -0.1)
        {
            if (whatButton == -2)
            {
                sprite.color = Color.green;
                rhythmMiniGame.ButtonSuccess();
            }
            else
            {
                sprite.color = Color.red;
                rhythmMiniGame.ButtonFailed();
            }
            this.enabled = false;
        }
	}
}
