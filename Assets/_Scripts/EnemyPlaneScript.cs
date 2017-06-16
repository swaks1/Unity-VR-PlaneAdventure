using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPlaneScript : MonoBehaviour {

    public GameObject explosion;
    //public float decreasedSpeed;
    public float speed;
    public GameObject missle;

    private GameObject target;
    private AttackControllerScript attackController;
    private GameController gameController;
    private AudioSource audioExplosion;

    //interval to shoot rockets
    public float shootTime;
    private float shootTimer;

    public float explosionTime; // the time needed to be touching so player will die
    private float timer;
    private bool touching;



	// Use this for initialization
	void Start () {
        timer = 0;
        shootTimer = 0;
        touching = false;

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

            shootTimer += Time.deltaTime;
            if (shootTimer > shootTime)
            {
                shootTimer = 0;
                GameObject newMissle = Instantiate(missle, transform.position, transform.rotation);
                var script = newMissle.GetComponent<EnemyMissleScript>();
                script.setTarget(target);
            }
        }

        if (touching)
        {
            timer += Time.deltaTime; //destroy the player after explosion time  of  tocushing
            if (timer >= explosionTime)
            {
                DestroyPlayer();
            }
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
            //this has to stay here
            touching = false;
            shootTimer = -10;
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

            //add score and decrease enemy count
            gameController.AddScore(10);
            gameController.currentEnemyCount--;
            gameController.updateEnemyCount();

            //destroy the missle and plane after 2 sec because the sound needs to finish
            Destroy(other.gameObject);
            Destroy(gameObject,1.2f);
            Destroy(explsionAnimation,2);
        }

        if (other.tag == "Plane")
        {
            // set touching and in update after some time destroy the player
            touching = true;       
        }

    }

    private void OnTriggerExit(Collider other)
    {
        touching = false;
        //timer = 0;
    }

    private void DestroyPlayer()
    {
        touching = false;
        //explosion and hide the plane...dont destroy it because music will finish
        GameObject explsionAnimation = Instantiate(explosion, transform.position, transform.rotation);

        //hiding of game object is done using renderer.enabled = false.... but htis prefab doesnt have renderer
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GetComponent<EventTrigger>().enabled = false;
        //audioExplosion.Play();

        //destroy the Player's plane
        GameObject.FindWithTag("Plane").GetComponent<PlaneScript>().PlayerDied();

        //destroy this game object and the explosion particles
        Destroy(gameObject, 2);
        Destroy(explsionAnimation, 2);
    }

}
