using UnityEngine;
using System.Collections;

public class RhythmMiniGame : MonoBehaviour 
{
    public GameObject[] button;
    private Color initialColor;
    public bool miniGameStart;
    public AlertManager alertManager;
    public BloodManager bloodManager;
    public int increaseAlertSuccess;
    public int increaseAlertFailed;
    public int increaseBlood;

	// Use this for initialization
	void Start () 
    {
        initialColor = button[0].GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void StartMiniGame()
    {
        miniGameStart = true;
        StartCoroutine(MiniGameStart());
    }

    public void StopMiniGame()
    {
        miniGameStart = false;
        Debug.Log("Mini Game Stopped");
    }

    public IEnumerator MiniGameStart()
    {
        Debug.Log("Minigame start");
        int x = Random.Range(0, 9);
        float time = 1f;
        yield return new WaitForSeconds(time);
        for(int i=0;i<=9;i++)
        {
            if (miniGameStart)
            {
                int y = Random.Range(0, 4);
                Debug.Log(y);
                if (y == 4)
                {
                    y = Random.Range(0, 3);
                }
                button[y].SetActive(true);
                yield return new WaitForSeconds(time);
                button[y].SetActive(false);
                button[y].GetComponent<RhythmGameButton>().enabled = true;
                button[y].GetComponent<SpriteRenderer>().color = initialColor;
                yield return new WaitForSeconds(time);
            }
            else
                yield return 0;
        }
    }

    public void ButtonSuccess()
    {
        alertManager.IncreaseAlert(increaseAlertSuccess);
        bloodManager.IncreaseBlood(increaseBlood);
    }

    public void ButtonFailed()
    {
        alertManager.IncreaseAlert(increaseAlertFailed);
    }
}
