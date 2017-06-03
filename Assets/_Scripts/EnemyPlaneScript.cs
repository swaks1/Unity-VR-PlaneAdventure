using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPlaneScript : MonoBehaviour {

    public GameObject explosion;
    //public float decreasedSpeed;
    public float speed;

    private GameObject target;
    private AttackControllerScript attackController;
    private GameController gameController;
    private AudioSource audioExplosion;
    


	// Use this for initialization
	void Start () {

        attackController = GameObject.FindWithTag("AttackController").GetComponent<AttackControllerScript>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        //set the speed to current of plane minus some constant
        //speed = GameObject.FindWithTag("Player").GetComponent<AutoFly_Advanced>().speed - decreasedSpeed;

        audioExplosion = GetComponent<AudioSource>();

        //get the target
        target = GameObject.FindWithTag("Plane");
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null && !gameController.gameOver)
        {
            //face towards target , move, and rotate
            //transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
            transform.LookAt(target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }


    }

    public void PointerEnter()
    {
        if (gameController.gameOver == false)
        {
            attackController.startedGaze(gameObject);
        }
            
    }

    public void PointerExit()
    {
        if(gameController.gameOver == false)
        {
            attackController.endedGaze();
        }
       
    }
    //RIGID BODY IS NEEDED IN ONE OF THE COLLIDERS GAME OBJECT !!!!!!!!!!!! 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missle")
        {
            //explosion and hide the plane...dont destroy it because music will finish
            GameObject explsionAnimation = Instantiate(explosion, transform.position, transform.rotation);

            //hiding of game object is done using renderer.enabled = false.... but htis prefab doesnt have renderer
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GetComponent<EventTrigger>().enabled = false;
            GetComponent<Collider>().enabled = false;
            audioExplosion.Play();

            gameController.AddScore(10);

            //destroy the missle and plane after 2 sec because the sound needs to finish
            Destroy(other.gameObject);
            Destroy(gameObject,2);
            Destroy(explsionAnimation,2);
        }

        if (other.tag == "Plane")
        {
            //explosion and hide the plane...dont destroy it because music will finish
            GameObject explsionAnimation = Instantiate(explosion, transform.position, transform.rotation);

            //hiding of game object is done using renderer.enabled = false.... but htis prefab doesnt have renderer
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GetComponent<EventTrigger>().enabled = false;
            audioExplosion.Play();

            //destroy the Player's plane
            GameObject.FindWithTag("Plane").GetComponent<PlaneScript>().PlayerDied();

            //destroy this game object and the explosion particles
            Destroy(gameObject, 2);
            Destroy(explsionAnimation, 2);
        }

    }

}
