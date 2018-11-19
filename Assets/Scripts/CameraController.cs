using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	
	//GameObject cameraRig;

	public GameObject cameraObject;
	public GameObject cameraRig;

	// NIGHT NOTES:
	// Smooth rotation method:
	// If axis is true, set target to nearest increment greater/less than current (not current + increment, since that changes each frame).
	// If current is not target:
	//		If less than 1/10 difference in value, set to current to target.
	//		Else, lerp toward target.
	// 
	// Consider: Maybe use a step based system. scoot increments between a discrete set of positions (8 divisions of 360, say), and
	// 			when moving increments between steps, move it 1 distance per frame times update time. When greater/equal to target step,
	//			end lerp. Immediately, if lerp state isn't happening this frame, allow rotation to continue as long as it's state is met.
	// 
	// Consider: If world and movement is isometric, at very shallow camera angles, the sky and distant terrain will be visible.
	//			Perhaps, create a horizon skybox based on adjacent regions, and create local edges to the current zone.
	//			Setup transitions to fade current zone and load new one? If you move seamlessly between zones, load adjacents,
	//			and use 2-distance regions to create skybox, or render actual hex as foggy distant terrain that's true to it's geometry?
	//			
	//			Could blend paralaxes and Perspective 3D? Bend flat terrain like an exagerated horizon curve with shader voodoo?
	//			Would either look good? Think on it.
	//			
	//			Hex-based region macrotiles that define the general shape of each local zone, and each zone is made up of smaller hex tiles.
	//			Villages on Hex-Based Plots, all arranged into a larger Hex?
	//			
	//			Consider how to dump empty regions of the camera. What kind of background works well when near the edges of a zone? Base it
	//			on zone type/feel? Make it abstract and patterned? Tie it to theme of the voidy world-eating crisis?
	//			
	//			Could simply use camera angle limits to prevent viewing of very distant terrain; that's an advantage of primarily top-down.
	//			
	//			Discrete regions and zone-loading does allow for non-euclidian movement, and easier procedural dungeons and overworlds.
	//
	//			Characters warp to you on zone change? Otherwise, could get lost. Warping to you makes some traps or terrain barriers seem weird,
	//			and could break immersion or tension when a character is stuck somewhere? Could create a warp system that only brings cohorts with
	//			if within range. 
	//
	// Consider: Would multiplayer support befit this game? A single-player campaign mode featuring a Foundling Human, and a multiplayer survival
	//			sandbox with fully customizable characters?

	public bool firstPerson = false;
	public float elevation = 4.5f;			// Elevation of the camera. Static?

	public float rotation = 0f;			// Rotation applied to camera.
	float rotationIncrement = 15f;
	float camRotation = 0f;

	public float incline = 2f;			// Inclination angle of camera. Static?
	float inclineIncrement = 15f;
	float inclineMax = 90f;
	float camIncline = 30f;

	public float zoom = 2f;				// Camera Zoom as Negative Z Distance.
	float zoomIncrement = 3.125f;
	float zoomMax = 16f;
	float camZoom = 6.25f;

	float lerpRate = 0.125f;		// How fast the camera is lerped between values

	// Use this for initialization
	void Start ()
	{
		//cameraObject = Camera.main.gameObject;
		//cameraRig = this.gameObject;

		cameraRig.transform.localPosition = new Vector3 (0f, elevation, 0f);
		cameraRig.transform.localEulerAngles = new Vector3 (Mathf.Round(incline) * inclineIncrement, Mathf.Round(rotation) * rotationIncrement, 0f);
		cameraObject.transform.localPosition = new Vector3 (0f, 0f, -zoomIncrement * Mathf.Round(zoom));
	}

	void Update()
	{
		//transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, scrollRate);
		//transform.position = targetObject.transform.position;

		if (zoom < 1) // Enter First Person Mode
		{
			firstPerson = true;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		else if (firstPerson) // Release First Person Mode
		{
			firstPerson = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			incline = Mathf.Clamp(incline, 0, 6);
		}

		if (firstPerson)
		{
        	rotation = rotation + (Input.GetAxis("Mouse X"));
        	incline = Mathf.Clamp(incline - (Input.GetAxis("Mouse Y")), -6, 6);
		}
		else
		{
			if (Input.GetAxis("Mouse 2") != 0)
	        {
	        	rotation = rotation + (Input.GetAxis("Mouse X"));
	        	incline = Mathf.Clamp(incline - (Input.GetAxis("Mouse Y")), 0, 6);
	            //Debug.Log("Mouse Delta: " + mouseX + "/" + mouseY);

	            //cameraRig.transform.localEulerAngles = new Vector3 (Mathf.Round(incline) * inclineIncrement, Mathf.Round(rotation) * rotationIncrement, 0f);
	            //cameraObject.transform.localPosition = new Vector3 (0f, 0f, -zoomIncrement * Mathf.Round(zoom));
	        }
		}



   		if (Input.GetAxis("Mouse Wheel") != 0)
    	{
    		float mouseWheel = Input.GetAxis("Mouse Wheel");
    		zoom = Mathf.Clamp(zoom - mouseWheel, 0f, zoomMax);

    		// NIGHT NOTES 11/17/2018: Fix so zoom is increment * 2 to the rounded Zoom power.
    		// Lerp camera toward zoom results, and toward rotation/incline.
    		// If zoom < 1, do NOT snap to 0, but set First Person to False.
    		// Eventually make some kind of ejection system for making sure camera can dip below ground level
    		// ...but only if it's on a cliff, say, but will adjust elevation or eject camera if inside geometry.
    		// Go to First Person if camera inside geometry?
    		//
    		// After this, reinstate Raycast system, and retrieval of hit GameObject name.

    		//cameraObject.transform.localPosition = new Vector3 (0f, 0f, -zoomIncrement * Mathf.Round(zoom));
    	}

        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
        	//zoom = Mathf.Round(zoom);
			//cameraObject.transform.localPosition = new Vector3 (0f, 0f, -zoomIncrement * zoom);

			rotation = Mathf.Round(rotation);
			incline = Mathf.Round(incline);

			//cameraRig.transform.localEulerAngles = new Vector3 (incline * inclineIncrement, rotation * rotationIncrement, 0f);
        }
/*
		if (UserInput.NegativeDown("Camera X"))
		{
			rotation -= rotationIncrement;
		}
		if (UserInput.PositiveDown("Camera X"))
		{
			rotation += rotationIncrement;
		}

		if (UserInput.NegativeDown("Camera Y"))
		{
			incline = Mathf.Clamp(incline - inclineIncrement, inclineMin, inclineMax);
		}
		if (UserInput.PositiveDown("Camera Y"))
		{
			incline = Mathf.Clamp(incline + inclineIncrement, inclineMin, inclineMax);
		}

		if (UserInput.NegativeDown("Camera Z"))
		{
			zoom = Mathf.Clamp(zoom + zoomIncrement, zoomMin, zoomMax);
		}
		if (UserInput.PositiveDown("Camera Z"))
		{
			zoom = Mathf.Clamp(zoom - zoomIncrement, zoomMin, zoomMax);
		}
*/
	}

	void FixedUpdate()
	{
		//float camZoom = cameraObject.transform.localPosition.z;		// Current Zoom Level of Camera (negative Z position)
		//float camRotation = cameraRig.transform.localEulerAngles.y;	// Current Horizontal Rotation of Camera Rig
		//float camIncline = cameraRig.transform.localEulerAngles.x;	// Current Vertical Incline of Camera Rig

		//float camRotation = rotationAngle;	// Current Horizontal Rotation of Camera Rig

		camZoom = Mathf.Lerp(camZoom, Mathf.Round(zoom) * -zoomIncrement, lerpRate);
		//camRotation = Mathf.Lerp(camRotation, rotation * rotationIncrement, lerpRate);
		camRotation = Mathf.Lerp(camRotation, rotation * rotationIncrement, lerpRate);
		//camIncline = Mathf.Lerp(camIncline, incline * inclineIncrement, lerpRate);
		camIncline = Mathf.Lerp(camIncline, incline * inclineIncrement, lerpRate);

		cameraObject.transform.localPosition = new Vector3 (0f, 0f, camZoom);
		//cameraRig.transform.localEulerAngles = new Vector3 (camIncline, camRotation, 0f);
		cameraRig.transform.localEulerAngles = new Vector3 (camIncline, camRotation, 0f);

		//Quaternion camRotation = Quaternion.Lerp(cameraRig.transform.rotation, Quaternion.Euler(incline * inclineIncrement, rotation * rotationIncrement, 0), lerpRate);

		//cameraRig.transform.rotation = camRotation;

		//cameraObject.transform.localPosition = new Vector3 (0f, 0f, Mathf.Round(zoom) * -zoomIncrement);
		//cameraRig.transform.localEulerAngles = new Vector3 (incline * inclineIncrement, rotation * rotationIncrement, 0f);
	
		//Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed)
	/*
		float camZoom = -Camera.main.transform.localPosition.z;		// Current Zoom Level of Camera (negative Z position)
		float camRotation = cameraRig.transform.localEulerAngles.y;	// Current Horizontal Rotation of Camera Rig
		float camIncline = cameraRig.transform.localEulerAngles.x;	// Current Vertical Incline of Camera Rig

		camZoom = Mathf.Lerp(camZoom, zoom, lerpRate);
		camRotation = Mathf.LerpAngle(camRotation, rotation, lerpRate);
		camIncline = Mathf.LerpAngle(camIncline, incline, lerpRate);

		cameraRig.transform.localEulerAngles = new Vector3 (camIncline, camRotation, 0);
		cameraObject.transform.localPosition = new Vector3 (0, elevation, -camZoom);
	*/
	}
}
