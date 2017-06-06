using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRestart : MonoBehaviour
{

    public float gazeTime;
    private GameObject RestartProgress;
    private GameController gameController;
    private float timer = 0;
    private bool gazedAt;


    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        RestartProgress = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt == true)
        {
            timer += Time.deltaTime;
            if (timer <= gazeTime)
            {
                Vector3 newScale = new Vector3(timer / gazeTime, RestartProgress.transform.localScale.y, RestartProgress.transform.localScale.z);
                Vector3 newPosition = new Vector3(-0.5f + (timer / gazeTime) / 2, RestartProgress.transform.localPosition.y, RestartProgress.transform.localPosition.z);

                RestartProgress.transform.localScale = newScale;
                RestartProgress.transform.localPosition = newPosition;
            }
            else
            {
                //RestartTheGame
                endedGaze();               
                gameController.restartGame();
            }
        }

    }

    public void startedGaze()
    {
        gazedAt = true;
        RestartProgress.gameObject.SetActive(true);
        timer = 0;

        Debug.Log("started gaze ");
    }

    public void endedGaze()
    {
        gazedAt = false;
        RestartProgress.gameObject.SetActive(false);
        timer = 0;

        Debug.Log("ended gaze ");
    }
}
