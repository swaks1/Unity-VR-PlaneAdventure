using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        AutoFly_Advanced scriptFly = player.GetComponent(typeof(AutoFly_Advanced)) as AutoFly_Advanced;
        scriptFly.isFlying = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        AutoFly_Advanced scriptFly = player.GetComponent(typeof(AutoFly_Advanced)) as AutoFly_Advanced;
        scriptFly.isFlying = false;
    }
}
