using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour 
{
    public float startingTime;

    public float countingTime;

    //private PauseMenu pauseMenu;

    private TextMesh theText;

    //public GameObject GameOverScreen;

    //private PlayerController player;

    public HealthManager healthManager;

    public SpriteRenderer timeIcon;
    // Use this for initialization
    void Start()
    {
        countingTime = startingTime;

        theText = GetComponent<TextMesh>();

        //pauseMenu = FindObjectOfType<PauseMenu>();

        //player = FindObjectOfType<PlayerController>();

        //healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (pauseMenu.isPaused)
            //return;

        countingTime -= Time.deltaTime;

        if(countingTime < 30)
        {
            timeIcon.color = Color.red;
            theText.color = Color.red;
        }
        if (countingTime < 0)
        {
            healthManager.isDead = true;
            //GameOverScreen.SetActive(true);
            //player.gameObject.SetActive(true);
            countingTime = 0;
        }

        theText.text = "" + Mathf.Round(countingTime);

    }

    public void ResetTime()
    {
        countingTime = startingTime;
    }
}
