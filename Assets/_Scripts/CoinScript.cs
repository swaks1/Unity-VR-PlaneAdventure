using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    private GameController gameController;

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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plane")
        {
            //add score to controller
            gameController.AddScore(1);
            Destroy(this.gameObject);
        }
    }

}
