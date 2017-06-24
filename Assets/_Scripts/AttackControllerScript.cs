using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackControllerScript : MonoBehaviour {
    //how long to wait than shoot
    public float gazeTime = 2f;
    private float timer = 0;
    private bool gazedAt;
    private GameObject target;
    private Slider slider;
    private GameObject plane;
    public GameObject missle;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
         //get the slider to show progress of attack
        slider = GameObject.FindWithTag("Slider").GetComponent<Slider>();
        audio = GetComponent<AudioSource>();
        plane = GameObject.FindWithTag("Plane");
        slider.gameObject.SetActive(false);
        slider.value = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (gazedAt == true)
        {
            timer += Time.deltaTime;
            if (timer <= gazeTime)
            {
                slider.value = timer / gazeTime;
            }
            else
            {
                //Launch missle.... get the script and set the target to folow...also end gaze
                GameObject newMissle = Instantiate(missle, plane.transform.position, plane.transform.rotation);
                var script = newMissle.GetComponent<MissleScript>();
                script.setTarget(target);
                audio.Play();
                endedGaze();
            }
        }
    }
    public void startedGaze(GameObject obj)
    {
        gazedAt = true;
        target = obj;
        timer = 0;
        slider.gameObject.SetActive(true);
        slider.value = 0;     
    }
    public void endedGaze()
    {
        gazedAt = false;
        target = null;
        timer = 0;
        slider.gameObject.SetActive(false);
        slider.value = 0;
    }
}
