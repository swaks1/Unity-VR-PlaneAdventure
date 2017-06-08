using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStart : MonoBehaviour
{

    public float gazeTime;
    private GameObject StartProgress;
    private GameController gameController;
    private float timer = 0;
    private bool gazedAt;
    private AudioSource buttonLoad;


    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        StartProgress = gameObject.transform.GetChild(0).gameObject;
        buttonLoad = GetComponent<AudioSource>();
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
                Vector3 newPosition = new Vector3(-0.5f + (timer / gazeTime)/2, StartProgress.transform.localPosition.y, StartProgress.transform.localPosition.z);

                StartProgress.transform.localScale = newScale;
                StartProgress.transform.localPosition = newPosition;
            }
            else
            {
                //StartTheGame
                endedGaze();
                gameController.startGame();
            }
        }

    }

    public void startedGaze()
    {
        gazedAt = true;
        StartProgress.gameObject.SetActive(true);
        timer = 0;
        buttonLoad.Play();

        Debug.Log("started gaze ");
    }

    public void endedGaze()
    {
        gazedAt = false;
        StartProgress.gameObject.SetActive(false);
        timer = 0;
        buttonLoad.Stop();

        Debug.Log("ended gaze ");
    }
}
