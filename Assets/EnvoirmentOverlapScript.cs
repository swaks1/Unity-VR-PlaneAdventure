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
           //shift the terrains in y axis so menu wont overlap
            var terrains = GameObject.FindGameObjectsWithTag("Terrain");
            foreach(var t in terrains)
            {
                t.transform.localPosition = new Vector3(t.transform.localPosition.x, t.transform.localPosition.y - 4f, t.transform.localPosition.z);
            }
            var borders = GameObject.FindGameObjectsWithTag("TerrainBorder");

            foreach (var t in borders)
            {
                t.transform.localPosition = new Vector3(t.transform.localPosition.x, t.transform.localPosition.y - 4f, t.transform.localPosition.z);
            }

        }

        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }

    }


}
