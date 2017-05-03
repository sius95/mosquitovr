using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour 
{
    private int initialAlert;
    public int alert;
    private TextMesh alertText;
    public Animator anim;
    public bool decrease;
    public SpriteRenderer alertIcon;

	// Use this for initialization
	void Start () 
    {
        initialAlert = 0;
        alert = initialAlert;
        alertText = GetComponent<TextMesh>();
        alertText.text = alert.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
        alertText.text = alert.ToString();

        if(alert >=100)
        {
            alertIcon.sprite = Resources.Load<Sprite>("alert red black");
            alertText.color = Color.red;
            anim.SetBool("AlertFull", true);
            alert = 100;
        }
        else if(alert < 0)
        {
            alert = 0;
        }
        else
        {
            if(alert > 50 && alert <100)
            {
                alertIcon.sprite = Resources.Load<Sprite>("alert yellow black");
                alertText.color = Color.yellow;
            }
            else if( alert < 50)
                alertIcon.sprite = Resources.Load<Sprite>("alert fix");
            anim.SetBool("AlertFull", false);
        }
	}

    public int GetAlert()
    {
        return alert;
    }

    public void IncreaseAlert(int pointToIncrease)
    {
        alert += pointToIncrease;
    }

    public void DecreaseAlert(int pointToDecrease)
    {
        alert -= pointToDecrease;
    }

    public void StartDecreaseAlert(int pointToDecrease,float time)
    {
        decrease = true;
        StartCoroutine(DecreaseAlertPerSec(pointToDecrease,time));
    }

    public void StopDecreaseAlert(int pointToDecrease, float time)
    {
        decrease = false;
        StopCoroutine(DecreaseAlertPerSec(pointToDecrease, time));
    }

    public IEnumerator DecreaseAlertPerSec(int pointToDecrease,float time)
    {
        while (decrease)
        {
            if(alert > 0)
                alert -= pointToDecrease;
            yield return new WaitForSeconds(time);
        }
    }
}
