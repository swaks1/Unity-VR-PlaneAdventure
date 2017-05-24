using UnityEngine;
using System.Collections;
using System;

public class AutoFly_Advanced : MonoBehaviour {

	[Tooltip("GvrViewer in the scene.")]
	public GvrViewer viewer;

	[Tooltip("Speed at which the player will move.")]
	public float speed;

	[Tooltip("Whether the camera is allowed to rotate around the y axis")]
	public bool allowsRotation;

	[Tooltip("Speed at which the player will rotate around the y axis")]
	public float rotationSpeed;

    //stavigo inputot od EVENT SYSTEMOT
    [Tooltip("Add the event system here.")]
    public GvrPointerInputModule gvrInputModule;


	// Whether or not player is flying
	public bool isFlying;

	// The Camera in the Head
	private Camera headCamera;

    private GameObject plane;


	// Use this for initialization
	void Start () {
		// Stationary start
		isFlying = false;

		// Initialize headCamera to the Camera in the child of Head
		headCamera = this.GetComponentInChildren<Camera> ();
        plane = GameObject.FindWithTag("Plane");

    }

	// Update is called once per frame
	void Update () {

        // OVDE SE MENUVA INPUTOT za GVR VIEWR in FGVR INPUT MODULE....

       // viewer.Triggered = Input.GetKeyDown(KeyCode.Mouse0);
        viewer.Triggered = Input.GetKeyDown(KeyCode.D);
        // gvrInputModule.triggerDown = Input.GetKey(KeyCode.D);
        //gvrInputModule.triggering = Input.GetKey(KeyCode.D);

        // Check if user has triggered fly/stop
        // (Triggered is called when the user presses the button on their VR headset)
        if (viewer.Triggered) {
			isFlying = !isFlying;
		}

		// Move Head if flying
		if (isFlying) {
			// Translate the Head by getting the forward vector from the headCamera
			// Time.deltaTime will smooth out the movement
			this.transform.position += headCamera.transform.forward * Time.deltaTime * speed;

            // Rotate Head around y axis
            if (allowsRotation) {
				float zRotation = headCamera.transform.rotation.eulerAngles.z;
				float normZ = zRotation;
				// Normalize the zRotation so that -180 < normZ < 180
				if (zRotation >= 180) {
					normZ = zRotation - 360;
				}
				Vector3 rotateVector = Vector3.up * rotationSpeed * Time.deltaTime * normZ;
				this.transform.Rotate (-rotateVector);
                

			}
		}

        detectPressedKeyOrButton();

    }

    //Test for Joystick
    //example USAGE
    //gvrInputModule.triggerDown = Input.GetKeyDown(KeyCode.Joystick1Button0);
    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                Debug.Log("KeyCode down: " + kcode);
        }
    }
}
