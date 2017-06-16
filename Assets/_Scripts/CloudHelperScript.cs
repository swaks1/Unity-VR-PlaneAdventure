using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHelperScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Missle")
        {         
            Destroy(other.gameObject);
        }
        if (other.tag == "EnemyMissle")
        {
            Destroy(other.gameObject);
        }


    }
}
