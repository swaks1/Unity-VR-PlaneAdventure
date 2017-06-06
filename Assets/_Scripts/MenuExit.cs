using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExit : MonoBehaviour
{

    public float gazeTime;
    private GameObject StartProgress;
    private GameController gameController;
    private float timer = 0;
    private bool gazedAt;


    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        StartProgress = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt == true)
        {
            timer += Time.deltaTime;
            if (timer <= gazeTime)
            {
                Vector3 newScale = new Vector3(timer / gazeTime, StartProgress.transform.localScale.y, StartProgress.transform.localScale.z);
                Vector3 newPosition = new Vector3(-0.5f + (timer / gazeTime) / 2, StartProgress.transform.localPosition.y, StartProgress.transform.localPosition.z);

                StartProgress.transform.localScale = newScale;
                StartProgress.transform.localPosition = newPosition;
            }
            else
            {
                //StartTheGame
                endedGaze();
                gameController.ExitGame();
            }
        }

    }

    public void startedGaze()
    {
        gazedAt = true;
        StartProgress.gameObject.SetActive(true);
        timer = 0;

        Debug.Log("started gaze ");
    }

    public void endedGaze()
    {
        gazedAt = false;
        StartProgress.gameObject.SetActive(false);
        timer = 0;

        Debug.Log("ended gaze ");
    }
}
