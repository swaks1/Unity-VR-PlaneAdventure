using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissleScript : MonoBehaviour
{

    public float speed;
    private GameObject target;
    //we must rotate children and move parent toward target
    private Transform childObject;

    // Use this for initialization
    void Start()
    {
        // this has to be empty because sometimes it gets called after you
        // change something externally in this script from AttackController.... LOST 1 HOUR HERE !!!!!

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        childObject.Rotate(0, 0, 100 * Time.deltaTime);
    }

    public void setTarget(GameObject obj)
    {
        this.target = obj;
        childObject = transform.GetChild(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain")
        {
            Destroy(gameObject);
        }
        if(other.tag == "Plane")
        {
            Destroy(gameObject);
            //destroy the Player's plane
            GameObject.FindWithTag("Plane").GetComponent<PlaneScript>().PlayerDied();
        }
        if(other.tag == "Missle")
        {
            Destroy(gameObject, 0.2f);
        }

    }



}
