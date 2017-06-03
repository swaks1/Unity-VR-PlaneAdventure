using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour {

    public GameObject player;
    public GameObject explosion;
    private GameController gameController;

    // Use this for initialization
    void Start ()
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
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        PlayerDied();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
            PlayerDied();
        }

    }

    public void PlayerDied()
    {
        AutoFly_Advanced scriptFly = player.GetComponent(typeof(AutoFly_Advanced)) as AutoFly_Advanced;
        scriptFly.stopFlying();

        gameController.GameOver();
        GameObject.FindWithTag("AttackController").GetComponent<AttackControllerScript>().endedGaze();

        Instantiate(explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
