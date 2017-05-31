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

    private GameController gameController;

  

    //variables use ot change rotation of plane
    public float checkingRotationInterfal = 0.1f;
    private float time = 0.0f;
    Vector3 currentRotation;
    Vector3 previosRotation;
    Vector3 difference;
    Vector3 startringRotation;
    bool left, right;


    // Use this for initialization
    void Start () {

        GameObject gameControllerObj = GameObject.FindWithTag("GameController");
        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
		// Initialize headCamera to the Camera in the child of Head
		headCamera = this.GetComponentInChildren<Camera> ();
        plane = GameObject.FindWithTag("Plane");

        isFlying = false;

        //set the current equal to previos rotation.. this is used for rotating plane left and right
        currentRotation = previosRotation = headCamera.transform.localEulerAngles;
        difference = Vector3.zero;
        startringRotation = plane.transform.localEulerAngles;
        left = right = false;

    }

	// Update is called once per frame
	void Update () {

        if (gameController.gameOver)
            return;

        // OVDE SE MENUVA INPUTOT za GVR VIEWR in FGVR INPUT MODULE....
       // viewer.Triggered = Input.GetKeyDown(KeyCode.Mouse0);
        viewer.Triggered = Input.GetKeyDown(KeyCode.D);
        // gvrInputModule.triggerDown = Input.GetKey(KeyCode.D);
        //gvrInputModule.triggering = Input.GetKey(KeyCode.D);

        // Check if user has triggered fly/stop
        // (Triggered is called when the user presses the button on their VR headset)
        if (viewer.Triggered) {
			isFlying = !isFlying;
            if (isFlying)
            {
                plane.GetComponent<AudioSource>().Play();
            }
            else
            {
                plane.GetComponent<AudioSource>().Stop();
            }
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

        //change the rotation of the plane dpeneding of HEAD rotation...
        //executes every 'checkingRotationInterfal' seconds...
        time += Time.deltaTime;
        if (time >= checkingRotationInterfal)
        {
            time = 0.0f;
            changeRotationOfPlane();
        }

        detectPressedKeyOrButton();

    }

    private void changeRotationOfPlane()
    {
        
        currentRotation = headCamera.transform.localEulerAngles;
        difference = currentRotation - previosRotation;      
        float differenceY = difference.y;
        //angley = (angley > 180) ? angley - 360 : angley;  if u want normalized with - sign
        //Debug.Log("difference :" + differenceY );

    
        if (differenceY > 3)
        {
            if (!right)
            {
                right = true;
                plane.transform.Rotate(0, 0, 10);
            }
            if (left)
            {
                left = false;
                plane.transform.Rotate(0, 0, 10);
            }
        }
        if (differenceY < -3)
        {
            if (!left)
            {
                left = true;
                plane.transform.Rotate(0, 0, -10);
            }
            if (right)
            {
                right = false;
                plane.transform.Rotate(0, 0, -10);
            }

        }

        if (difference == Vector3.zero)
        {
            if (left)
            {
                plane.transform.Rotate(0, 0, 10);
                left = false;
            }
            if (right)
            {
                plane.transform.Rotate(0, 0, -10);
                right = false;
            }
        }
        
        previosRotation = currentRotation;
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
