using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
	public GameObject cameraRig; // Used for calculating camera rotation, for the movement vector.
	public GameObject sprite;
	float camRotation;
	float rotation;

	public float actionRange = 15f; //range at which player can interact with objects.

	// public static Ray MouseRay ()
	// {
	//	return (Camera.main.ScreenPointToRay(Input.mousePosition));
	// }

	Vector3 inputVector;

    // Use for initialization
    void Start()
    {
    	camRotation = cameraRig.transform.localEulerAngles.y;
		rotation = transform.localEulerAngles.y;

        inputVector = Vector3.zero;
    }

    // Use for Input
    void Update()
    {
		camRotation = cameraRig.transform.localEulerAngles.y;

		inputVector.x = Input.GetAxis("Move X");
		inputVector.z = Input.GetAxis("Move Y");
		inputVector = Quaternion.Euler(0, camRotation, 0) * inputVector;

		if (Input.GetButtonDown("Action 1"))
		{
			Debug.Log("Action Button");
			RaycastHit actionHit;

			Ray actionRay = Calc.MouseRay();
        	Debug.DrawRay(actionRay.origin, actionRay.direction * 500f, Color.grey, 2f);

			if (Physics.Raycast(actionRay, out actionHit, 500f))
			{
				Debug.DrawLine(actionRay.origin, actionHit.point, Color.yellow, 2f);
				Debug.Log("Mouse Click Target: " + actionHit.transform.name);

				float hitDistance = (actionHit.point - transform.position).magnitude;
				if (hitDistance <= actionRange)
				{
					Debug.Log("Target Within Action Range: " + hitDistance + "/" + actionRange);
					Debug.DrawLine(transform.position, actionHit.point, Color.green, 2f);
				}
				else
				{
					Debug.Log("Target Beyond Action Range: " + hitDistance + "/" + actionRange);
					Debug.DrawLine(transform.position, actionHit.point, Color.red, 2f);
				}
			}
		}
	}

	// Use for Physics
    void FixedUpdate()
	{
		//Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f)
		transform.position = (transform.position + inputVector);
		//sprite.transform.rotation = Quaternion.LookRotation(inputVector);

	}
}
