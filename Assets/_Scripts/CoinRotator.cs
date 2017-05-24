using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour {

    private Transform child;
    public int speedRight = 100;
    public int speedUp = 100;

    // Use this for initialization
    void Start () {
        child = this.gameObject.transform.GetChild(0);
	}
	
    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        child.Rotate(Vector3.right * speedRight * Time.deltaTime);

        // ...also rotate around the World's Y axis
       child.Rotate(Vector3.up * speedUp * Time.deltaTime, Space.World);
        //child.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
