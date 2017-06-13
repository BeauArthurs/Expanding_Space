using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    private Planet currentPlanet;
    private CamController cam;
    private bool moving;
    private Vector3 offset;
    private float speed;
    private bool started;
	void Awake () {
        
        cam = Camera.main.gameObject.GetComponent<CamController>();
        offset = Vector3.up * 5;
        started = false;
        moving = false;
        speed = 5;
    }
    public void start() {
        currentPlanet = Planet.planetList[2];
        transform.position = currentPlanet.transform.position;
        
        started = true;
    }

	void Update () {
        if (started)
        {
            if (!moving)
            {
                transform.position = currentPlanet.gameObject.transform.position + offset;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPlanet.transform.position, speed);
                if (Vector3.Distance(transform.position, currentPlanet.transform.position) < .5)
                {
                    moving = false;
                    cam.LockObject(currentPlanet.transform);
                }
            }
        }
	}

    public void SetCurrentPlanet(int planet) {
        if (Planet.planetList[planet] != currentPlanet)
        {
            currentPlanet = Planet.planetList[planet];
            moving = true;
            cam.LockObject(this.transform);
        }
        
    }
}
