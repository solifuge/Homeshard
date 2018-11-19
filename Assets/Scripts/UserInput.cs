using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserInput
{
	public static bool anyAxis;
	public static bool anyAxisDown;

	public static bool anyKey;
	public static bool anyKeyDown;

	public static float currentTime = 0f;

	public static string[] inputNames;

	public static Dictionary<string, float> lastAxis;
	public static Dictionary<string, float> lastAxisDownTime;
	public static Dictionary<string, float> lastPositiveDownTime;
	public static Dictionary<string, float> lastNegativeDownTime;
	public static Dictionary<string, float> heldAxisDownTime;
	public static Dictionary<string, float> heldPositiveDownTime;
	public static Dictionary<string, float> heldNegativeDownTime;

	// DEFAULT INPUT FUNCTIONS:
	public static float Axis(string input) { return (Input.GetAxis(input)); }
	public static float AxisRaw(string input) { return (Input.GetAxisRaw(input)); }
	public static bool AxisHeld(string input) { return (Input.GetButton(input)); }
	public static bool AxisDown(string input) { return (Input.GetButtonDown(input)); }
	public static bool AxisUp(string input) { return (Input.GetButtonUp(input)); }

	// BASIC CUSTOM INPUT FUNCTIONS:
	public static float NegativeAxis (string input) { return (Mathf.Max (0, -Input.GetAxis(input))); }
	public static float PositiveAxis (string input) { return (Mathf.Max (0, Input.GetAxis(input))); }

	public static bool NegativeHeld (string input) { return (Input.GetButton(input) && Input.GetAxis(input) < 0); }
	public static bool NegativeDown (string input) { return (Input.GetButtonDown(input) && Input.GetAxis(input) < 0); }
	public static bool NegativeUp (string input) { return (Input.GetButtonUp(input) && lastAxis[input] < 0); }

	public static bool PositiveHeld (string input) { return (Input.GetButton(input) && Input.GetAxis(input) > 0); }
	public static bool PositiveDown (string input) { return (Input.GetButtonDown(input) && Input.GetAxis(input) > 0); }
	public static bool PositiveUp (string input) { return (Input.GetButtonUp(input) && lastAxis[input] > 0); }

	public static bool AxisMin(string input) { return (Input.GetAxisRaw(input) == -1); }
	public static bool AxisNegative(string input) { return (Input.GetAxisRaw(input) < 0); }
	public static bool AxisZero(string input) { return (Input.GetAxisRaw(input) == 0); }
	public static bool AxisPositive(string input) { return (Input.GetAxisRaw(input) > 0); }
	public static bool AxisMax(string input) { return (Input.GetAxisRaw(input) == 1); }

	// LONG-TERM TRACKED INPUT FUNCTIONS
	public static float LastAxis(string input) { return (Input.GetAxis(input)); }

	public static bool AxisDoubleTap (string input, float doubleTapTime = 0.3333f) { return (AxisDown(input) && (currentTime - lastAxisDownTime[input]) <= doubleTapTime); }
	public static bool PositiveDoubleTap (string input, float doubleTapTime = 0.3333f) { return (PositiveDown(input) && (currentTime - lastPositiveDownTime[input]) <= doubleTapTime); }
	public static bool NegativeDoubleTap (string input, float doubleTapTime = 0.3333f) { return (NegativeDown(input) && (currentTime - lastNegativeDownTime[input]) <= doubleTapTime); }

	public static bool AxisHeldFor (string input, float holdTime = 0.3333f) { return (AxisHeld(input) && (currentTime - heldAxisDownTime[input]) >= holdTime); }
	public static bool PositiveHeldFor (string input, float holdTime = 0.3333f) { return (PositiveHeld(input) && (currentTime - heldPositiveDownTime[input]) >= holdTime); }
	public static bool NegativeHeldFor (string input, float holdTime = 0.3333f) { return (NegativeHeld(input) && (currentTime - heldNegativeDownTime[input]) >= holdTime); }

	/*
	public static bool AnyKey(string input) { return (Input.anyKey); }
	public static bool AnyKeyDown(string input) { return (Input.anyKeyDown); }

	public static bool AnyAxis(string input) { return anyAxis; }
	public static bool AnyAxisDown(string input) { return anyAxisDown; }

	public static Dictionary<string, bool> lastAxisUp;
	public static Dictionary<string, bool> lastAxisDown;
	public static Dictionary<string, bool> lastPositiveUp;
	public static Dictionary<string, bool> lastPositiveDown;
	public static Dictionary<string, bool> lastNegativeUp;
	public static Dictionary<string, bool> lastNegativeDown;
	*/


	// THINGS TO TRACK:
	// Is Button Down
	// When was button last Pressed?
	// If

	// THINGS TO RETRIEVE:
	//
	// How long has it been Held for (If Held has value, but axis released, becomes Zero)

	public static void Initialize()
	{
		anyAxis = false;
		anyAxisDown = false;

		anyKey = false;
		anyKeyDown = false;

		lastAxis = new Dictionary<string, float>();
		lastAxisDownTime = new Dictionary<string, float>();
		lastPositiveDownTime = new Dictionary<string, float>();
		lastNegativeDownTime = new Dictionary<string, float>();
		heldAxisDownTime = new Dictionary<string, float>();
		heldPositiveDownTime = new Dictionary<string, float>();
		heldNegativeDownTime = new Dictionary<string, float>();

		inputNames = new string[]
		{
			"Move X", "Move Y", "Aim X", "Aim Y", "Select X", "Select Y",
			"Confirm/Cancel", "Jump/Run", "Modifier", "Menu", "Use Item", "Item None/Drop",
			"Item 1/2", "Item 3/4", "Item 5/6", "Item 7/8", "Item 9/10", "Item 11/12",
			"Camera X", "Camera Y", "Camera Z"
		};

		foreach (string axisName in inputNames)
		{
			lastAxis.Add(axisName, 0f);
			lastAxisDownTime.Add(axisName, 0f);
			lastPositiveDownTime.Add(axisName, 0f);
			lastNegativeDownTime.Add(axisName, 0f);
			heldAxisDownTime.Add(axisName, 0f);
			heldPositiveDownTime.Add(axisName, 0f);
			heldNegativeDownTime.Add(axisName, 0f);
		}
	}

	public static void Update()
	{
		currentTime = Time.time;

		anyAxis = false;
		anyAxisDown = false;

		anyKey = Input.anyKey;
		anyKeyDown = Input.anyKeyDown;

		// Update Input Data for This Frame
		foreach (string axisName in inputNames)
		{
			if (AxisHeld(axisName))
			{
				anyAxis = true;

				if (!PositiveHeld(axisName)) { heldPositiveDownTime[axisName] = Mathf.Infinity; }
				if (!NegativeHeld(axisName)) { heldNegativeDownTime[axisName] = Mathf.Infinity; }

				if (AxisDown(axisName))
				{
					anyAxisDown = true;
					heldAxisDownTime[axisName] = currentTime;
				
					if (PositiveDown(axisName)) { heldPositiveDownTime[axisName] = currentTime; }
					if (NegativeDown(axisName)) { heldNegativeDownTime[axisName] = currentTime; }
				}
			}
			else
			{
				heldAxisDownTime[axisName] = Mathf.Infinity;
			}
		}
	}

	public static void LateUpdate()
	{
		// Update Input Data for Previous Frame at End of Update
		foreach (string axisName in inputNames)
		{
			lastAxis[axisName] = Axis(axisName);

			if (AxisDown(axisName))
			{
				lastAxisDownTime[axisName] = currentTime;

				if (PositiveDown(axisName)) { lastPositiveDownTime[axisName] = currentTime; }
				if (NegativeDown(axisName)) { lastNegativeDownTime[axisName] = currentTime; }
			}
		}
	}
}