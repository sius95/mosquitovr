using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour 
{
    private int initialHealth;
    private float health;
    public GameObject[] healthIcon;

    public Fly fly;
    public float HurtTime;
    public float CountingTime;

    public bool isDead;
    public GameObject gameOverScreen;

    public bool decrease;
    public bool inPoisonZone;

    public float decreaseHealth;
    public float timeRange;
	// Use this for initialization
	void Start () 
    {
        fly = FindObjectOfType<Fly>();
        initialHealth = 3;
        health = initialHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(inPoisonZone && !decrease)
        {
            decrease = true;
            StartCoroutine(DecreaseHealthPerSec(decreaseHealth, timeRange));
        }
        if (!inPoisonZone && decrease)
        {
            decrease = false;
            StopCoroutine(DecreaseHealthPerSec(decreaseHealth, timeRange));
        }

        if(CountingTime >=1.0f)
        {
            CountingTime -= Time.deltaTime;
            fly.playerHurt = true;
        }
        else if(CountingTime <=1.0f)
        {
            fly.playerHurt = false;
        }

        if (health == 2.5f)
        {
            healthIcon[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_half");
        }
        else if (health == 2.0f)
        {
            healthIcon[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_empty");
            //healthIcon[2].SetActive(false);
        }
        else if (health == 1.5f)
        {
            healthIcon[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_empty");
            healthIcon[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_half");
            //healthIcon[2].SetActive(false);
        }
        else if (health == 1.0f)
        {
            healthIcon[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_empty");
            healthIcon[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_empty");
            //healthIcon[2].SetActive(false);
            //healthIcon[1].SetActive(false);
        }
        else if (health == 0.5f)
        {
            healthIcon[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_empty");
            healthIcon[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_empty");
            healthIcon[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("hudHeart_half");
            //healthIcon[2].SetActive(false);
            //healthIcon[1].SetActive(false);
        }
        else if (health <= 0.0f || isDead)
        {
            healthIcon[2].SetActive(false);
            healthIcon[1].SetActive(false);
            healthIcon[0].SetActive(false);
            gameOverScreen.SetActive(true);
        }

	}

    public float GetHealth()
    {
        return health;
    }

    public void IncreaseHealth(float healthToIncrease)
    {
        health += healthToIncrease;
    }

    public void DecreaseHealth(float healthToDecrease)
    {
        health -= healthToDecrease;
        CountingTime = HurtTime;
    }

    public void StartDecreaseHealth(float pointToDecrease, float time)
    {
        //decrease = true;
        inPoisonZone = true;
        decreaseHealth = pointToDecrease;
        timeRange = time;
        //StartCoroutine(DecreaseHealthPerSec(pointToDecrease, time));
    }

    public void StopDecreaseHealth(float pointToDecrease, float time)
    {
        //decrease = false;
        inPoisonZone = false;
        decreaseHealth = pointToDecrease;
        timeRange = time;
        //StopCoroutine(DecreaseHealthPerSec(pointToDecrease, time));
    }

    public IEnumerator DecreaseHealthPerSec(float pointToDecrease, float time)
    {
        while (decrease)
        {
            health -= pointToDecrease;
            yield return new WaitForSeconds(time);
        }
    }
}
