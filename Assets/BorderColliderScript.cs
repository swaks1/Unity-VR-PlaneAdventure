using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderColliderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            //destroy the Player's plane
            GameObject.FindWithTag("Plane").GetComponent<PlaneScript>().PlayerDied();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Plane")
        {
            //destroy the Player's plane
            GameObject.FindWithTag("Plane").GetComponent<PlaneScript>().PlayerDied();
            Destroy(this.gameObject);
        }


    }
}
