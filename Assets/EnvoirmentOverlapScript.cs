using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvoirmentOverlapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
          other.transform.localPosition = new Vector3(other.transform.localPosition.x, other.transform.localPosition.y - 4f , other.transform.localPosition.z);
            //Debug.Log(transform.parent.gameObject.tag);
        }

        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }

    }


}
