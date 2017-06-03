using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    private GameController gameController;
    private AudioSource audioSource;
    public float speedIncrease;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObj = GameObject.FindWithTag("GameController");

        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plane")
        {
            audioSource.Play();

            //add score to controller
            gameController.AddScore(5);

            //hide the coin and increase the speed..than destroy the coin
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            GameObject.FindWithTag("Player").GetComponent<AutoFly_Advanced>().speed += speedIncrease;
            Destroy(gameObject, 1);
        }
    }

}
