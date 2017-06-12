using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour
{
	private static ObjectiveManager _instance;		//much better with singletons
	public static ObjectiveManager instance {
		get {
			if (_instance == null)		//This will only happen the first time this reference is used.
				_instance = GameObject.FindObjectOfType<ObjectiveManager> ();
			return _instance;
		}
	}

	void Awake ()
	{
		_instance = this;
	}

	private string objectivesCompletedMessage = "Great. You completed all objectives!!!";
	bool objectivesCompletedMessageDisplayed = false;
	private float deltaVelThres = 1.5f;
	private string approachingPlanet = "Your Space Craft is approaching its target but it is moving too fast (|δv|>1.5km/s) relative to the planet!";
	static public bool fiveFirstObjectivesCompleted = false;
	private Queue<string> mQueue;
	private const float shortMarsTripTime = 200; /*days*/

	private List<Planet> planetList;

	public delegate bool CheckDelegate (Objective o);

	private const int numberOfObjectives = 7;
	private Objective[] objsList = new Objective[numberOfObjectives];

	public Objective[] ObjsList {
		get { return objsList; }
	}

	public CheckDelegate[] ObjectiveFunction = new CheckDelegate[numberOfObjectives];
	private string[] objectiveGoal = new string[numberOfObjectives] { 
        "Launch a Space Craft",
        "Sent a Viking Space Craft to Mars",
        "Sent a Magellan Space Craft to Venus",
        "Sent a Viking to Mars in less than " + shortMarsTripTime.ToString () + " days",
        "Sent a Galileo Space Craft to Jupiter",
        "Sent a Space Craft beyond Neptune",
        "Put a Galileo Space Craft in orbit around Jupiter"
    };

	public class Objective
	{
		public string ObjectiveGoal { get; set; }

		public string objective { get; set; }

		public CheckDelegate Check;
		public bool isCompleted = false;
		public bool messageDisplayed = false;
		public bool messageIsBeingDisplayed = false;
	}

	void Start ()
	{
		mQueue = GUIClass.messageQueue;

		planetList = Planet.planetList;

		ObjectiveFunction [0] = Obj1Check;
		ObjectiveFunction [1] = Obj2Check;
		ObjectiveFunction [2] = Obj3Check;
		ObjectiveFunction [3] = Obj4Check;
		ObjectiveFunction [4] = Obj5Check;
		ObjectiveFunction [5] = Obj6Check;
		ObjectiveFunction [6] = Obj7Check;

		for (int i = 0; i < numberOfObjectives; i++) {
			objsList [i] = new Objective ();
			objsList [i].ObjectiveGoal = objectiveGoal [i];
			objsList [i].Check = ObjectiveFunction [i];
		}
	}

	private void CheckObjectives ()
	{
		if (fiveFirstObjectivesCompleted == false) {
			for (int i = 0; i < 5; i++) {
				fiveFirstObjectivesCompleted = true;
				if (objsList [i].isCompleted == false) {
					fiveFirstObjectivesCompleted = false;
					break;
				}
			}
		}
		//mQueue.Enqueue("You unlock the Generic Space Craft which has unlimited fuel!!!");

		bool allObjCompleted = true;
		foreach (Objective o in objsList) {
			if (o.isCompleted == false) {
				allObjCompleted = false;
				o.isCompleted = o.Check (o);
			}
		}

		if (allObjCompleted == true && objectivesCompletedMessageDisplayed == false) {
			mQueue.Enqueue (objectivesCompletedMessage);
			objectivesCompletedMessageDisplayed = true;
		}
	}

	void Update ()
	{
		CheckObjectives ();
	}

	private bool Obj1Check (Objective o)
	{
		
			return false;
	}

	private bool Obj2Check (Objective o)
	{
		
		return false;
	}

	private bool Obj3Check (Objective o)
	{
		
		return false;
	}

	private bool Obj4Check (Objective o)
	{
		
		return false;
	}

	private bool Obj5Check (Objective o)
	{
		
		return false;
	}

	private bool Obj6Check (Objective o)
	{
		
		return false;
	}

	private bool Obj7Check (Objective o)
	{
		
		return false;
	}
}
