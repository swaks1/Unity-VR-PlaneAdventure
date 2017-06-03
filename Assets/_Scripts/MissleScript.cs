using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour {

    public float speed;
    private GameObject target;
    //we must rotate children and move parent toward target
    private Transform childObject;

	// Use this for initialization
	void Start () {
        // this has to be empty because sometimes it gets called after you
        // change something externally in this script from AttackController.... LOST 1 HOUR HERE !!!!!
     }
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {
            //face towards target , move, and rotate
            transform.LookAt(target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            // this can be deactivated...cant be noticed in game very good...
            childObject.Rotate(0,0,100 * Time.deltaTime);
        }
        else
        {
            target = GameObject.FindWithTag("Terrain");
        }
    }

    public void setTarget(GameObject obj)
    {
        this.target = obj;
        childObject = transform.GetChild(0);
        speed += GameObject.FindWithTag("Player").GetComponent<AutoFly_Advanced>().speed; 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
            Destroy(gameObject);
        }

    }



}
