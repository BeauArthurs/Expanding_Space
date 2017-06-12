﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIClass : MonoBehaviour
{
	public static Queue<string> messageQueue = new Queue<string> ();
	bool messageIsBeingDisplayed = false;
	string messageToBeDisplayed;
	private const string initialMessage = "This is a both a Game and a Simulation of our Solar System. \n" +
		"The planets the moon and the Space Crafts follow realistic orbits\n" +
		" and the relative sizes of the planets and moon are correct.";
	private const string secondMessage = "The goal of the Game is to launch Space Crafts and complete all the missions.";
	private const string thirdMessage = "Controls \n" +
		"Right click & move: rotate\n" +
		"Wheel: zoom in/out\n" +
		"Click on object to get info\n" +
		"Double click to focus.";
	private const string creditsMessage = " Programmers: \n Beau Arthurs,\n niek Schoone,\n Collin Loot";
	private const string controlsMessage1 = "Right click & move: rotate\n" +
		"Wheel: zoom in/out\n" +
		"Click on object to get info\n" +
		"Double click to focus\n";


	[System.Flags]
	enum Buttons
	{
		planetsButton =             0x001,  // 000000000001
		settingsButton =            0x200,
		creditsButton =             0x800
    }
	;
	Buttons buttonStatus;
	public GUISkin customSkin;
	private float hScrollbarDirection;
	private float hScrollbarSpeed;
	private float gravitySlider = Scales.massScale;
	public Texture2D plusIcon;
	public Texture2D minusIcon;
	public Texture2D playIcon;
	public Texture2D stopIcon;
	private float offSetX = 30;
	private float offSetY = 30;
	float offX = 10;
	float offY;
	float width;
	private float y1 = 5;
	private float y2 = 25;
	private CamController camCon;
	private List<Planet> planetList;
	private AuxOrbit auxOrbit;
	GUIStyle style = new GUIStyle ();
	private Scales.GravityLevel gravityLevel0 = Scales.GravityLevel.normal;

	void Start ()
	{
		camCon = Camera.main.GetComponent<CamController> ();

		planetList = Planet.planetList;

		auxOrbit = GameObject.Find ("AuxOrbit").GetComponent<AuxOrbit> ();

		messageQueue.Enqueue (initialMessage);
		messageQueue.Enqueue (secondMessage);
		messageQueue.Enqueue (thirdMessage);
	}

	void Update ()
	{
		//
		Scales.ClampTimeScale ();

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			Scales.IncreaseTimeScale ();
		}
		
		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			Scales.DecreaseTimeScale ();
		}

		if (Input.GetKeyDown ("space")) {
			Scales.Pause = !Scales.Pause;
		}
/******************************************************************************************************************/
		if (messageQueue.Count != 0 && messageIsBeingDisplayed == false) {
			messageToBeDisplayed = messageQueue.Dequeue ();
			StartCoroutine (CountdownForMessage (Scales.messageDuration));
		}
/******************************************************************************************************************/
		gravityLevel0 = Scales.GravityLevel.normal;

		switch (gravityLevel0) {
		case Scales.GravityLevel.normal:
			Scales.ResetMaximumTimeScale ();
			break;
		case Scales.GravityLevel.medium:
			Scales.MaxTimeScale = 1;
			break;
		case Scales.GravityLevel.high:
			Scales.MaxTimeScale = 0.5f;
			break;
		}
	}

	void OnGUI ()
	{
		GUI.skin = customSkin;

		offX = 10;
		width = 0;

		if (GUI.Button (new Rect (offX, y1, playIcon.width, y2), playIcon)) {
			Scales.Pause = false;
		}
		offX += playIcon.width;

		if (GUI.Button (new Rect (offX, y1, stopIcon.width, y2), stopIcon)) {
			Scales.Pause = true;
		}
		offX += stopIcon.width + 10f;

		if (GUI.Button (new Rect (offX, y1, plusIcon.width, y2), plusIcon)) {
			Scales.IncreaseTimeScale ();
		}
		offX += plusIcon.width;

		if (GUI.Button (new Rect (offX, y1, minusIcon.width, y2), minusIcon)) {
			Scales.DecreaseTimeScale ();
		}
		offX += minusIcon.width;

		if (Scales.Pause == false)
			GUI.Label (new Rect (offX, y1, Screen.width, Screen.height), "x" + Scales.CurrentTimeScale.ToString ());
		else
			GUI.Label (new Rect (offX, y1, Screen.width, Screen.height), "paused");
		offX += width + 50f;

		width = 100f;

		width = 170f;
		
		if (GUI.Button (new Rect (Screen.width - width - offSetX, y1, width, y2), "Settings") && messageIsBeingDisplayed == false) {
			buttonStatus ^= Buttons.settingsButton;
			buttonStatus &= Buttons.settingsButton;
		}
		if (buttonStatus == Buttons.settingsButton) {

			if (GUI.Button (new Rect (Screen.width - width - offSetX, y1 + 1 * offSetY, width, y2), "Credits")) {
				messageQueue.Enqueue (creditsMessage);
				buttonStatus = 0;
			}
			if (GUI.Button (new Rect (Screen.width - width - offSetX, y1 + 2 * offSetY, width, y2), "Controls")) {
				messageQueue.Enqueue (controlsMessage1);
				buttonStatus = 0;
			}
		}

		


		if (messageIsBeingDisplayed == true)
			GUI.Box (new Rect (Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 175), messageToBeDisplayed);
	}

	IEnumerator CountdownForMessage (float time0)
	{
		for (float timer = time0; timer >= 0; timer -= Time.deltaTime) {
			Scales.Pause = true;
			Scales.ResetTimeScale ();
			messageIsBeingDisplayed = true;
			yield return null;
		}
		messageIsBeingDisplayed = false;
	}

	void ShowObjectives ()
	{
		int i = 1;
		foreach (ObjectiveManager.Objective o in ObjectiveManager.instance.ObjsList) {
			if (o.isCompleted == true)
				style.normal.textColor = Color.green;
			else
				style.normal.textColor = Color.red;

			GUI.Label (new Rect (offX, y1 + offSetY * i++, 240, y2), o.ObjectiveGoal, style);
		}
	}
	void ShowPlanets ()
	{
		for (int i = 0; i < planetList.Count; i++)
			if (GUI.Button (new Rect (offX, y1 + offSetY * (i + 1), width, y2), planetList [i].gameObject.name))
				camCon.LockObject (planetList [i].gameObject.transform);
	}

	void ShowLaunchPanel (float minDirection, float maxDirection, float minSpeed, float maxSpeed, string label)
	{
		offY += offSetY;
		hScrollbarDirection = GUI.HorizontalScrollbar (new Rect (offX, y1 + offSetY, 170, 30),
                                                hScrollbarDirection, 0f, minDirection, maxDirection);

		string labelDirection = Mathf.Round (hScrollbarDirection).ToString () + "°";
		GUI.Label (new Rect (offX + width, y1 + offSetY - 3, 140, 30), labelDirection);

		offY += offSetY;
		hScrollbarSpeed = GUI.HorizontalScrollbar (new Rect (offX, y1 + 2 * offSetY, 170, 30),
                                                hScrollbarSpeed, 0.0f, minSpeed, maxSpeed);

		string labelSpeed = (Mathf.Round (hScrollbarSpeed * Scales.velmu2kms * 100f) / 100f).ToString () + "km/s";
		GUI.Label (new Rect (offX + width, y1 + 2 * offSetY - 3, 140, 30), labelSpeed);

		offY += offSetY;
		if (GUI.Button (new Rect (offX, y1 + 3 * offSetY, 170, 30), "Launch " + label)) {
			buttonStatus = 0;

			hScrollbarDirection = 0;
			hScrollbarSpeed = 0;

			auxOrbit.isActive = false;
			auxOrbit.POPlanet = null;
		}

		auxOrbit.Speed = hScrollbarSpeed;
		auxOrbit.Direction = hScrollbarDirection;
	}

	void ShowCourseCorrectionPanel (float minDirection, float maxDirection, float minSpeed, float maxSpeed)
	{
		offY += offSetY;
		hScrollbarDirection = GUI.HorizontalScrollbar (new Rect (offX, y1 + offSetY, 170, 30),
                                                hScrollbarDirection, 0f, minDirection, maxDirection);

		string labelDirection = Mathf.Round (hScrollbarDirection).ToString () + "°";
		GUI.Label (new Rect (offX + width, y1 + offSetY - 3, 140, 30), labelDirection);

		offY += offSetY;
		

		

		offY += offSetY;
		if (GUI.Button (new Rect (offX, y1 + 3 * offSetY, 170, 30), "Thrust")) {

			hScrollbarDirection = 0;
			hScrollbarSpeed = 0;
		}
	}

	private float AngleBetween (Vector3 a, Vector3 b, Vector3 n)
	{
		return Vector3.Angle (a, b) * Mathf.Sign (Vector3.Dot (n, Vector3.Cross (a, b)));
	}
}