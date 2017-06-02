using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPlaneScript : MonoBehaviour {

    AttackControllerScript controller;
    public GameObject explosion;
    private AudioSource audioExplosion;


	// Use this for initialization
	void Start () {
        GameObject attackControllerObj = GameObject.FindWithTag("AttackController");
        if (attackControllerObj != null)
        {
            controller = attackControllerObj.GetComponent<AttackControllerScript>();
        }
        if (controller == null)
        {
            Debug.Log("Cannot find 'AttackController' script");
        }

        audioExplosion = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void PointerEnter()
    {
        controller.startedGaze(gameObject);
    }

    public void PointerExit()
    {
        controller.endedGaze();
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
            audioExplosion.Play();

            //destroy the missle and plane after 2 sec because the sound needs to finish
            Destroy(other.gameObject);
            Destroy(gameObject,2);
            Destroy(explsionAnimation,2);
        }

    }

}
