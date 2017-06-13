using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    private Planet currentPlanet;
    private Planet destinationPlanet;
    private bool moving;
    private Vector3 offset;
    private float speed;
    private bool started;

    [SerializeField] private GameObject scanButton;
    [SerializeField] private CameraOrbit cam;

	void Awake () {
        
        offset = Vector3.up * 2;
        moving = false;
        speed = 5;
    }

    public void Initiate() {
        currentPlanet = Planet.planetList[2];
        transform.position = currentPlanet.transform.position;
        cam.target = currentPlanet.transform;
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
                if (destinationPlanet != null) {
                    MoveToPlanet(destinationPlanet);
                }
                if (Vector3.Distance(transform.position, destinationPlanet.transform.position) < .5)
                {
                    SetCurrentPlanet(destinationPlanet);
                }
            }
        }
	}

    private void MoveToPlanet(Planet dest) {
        float dist = Vector3.Distance(transform.position, dest.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, dest.transform.position, speed + (dist/1000));
    }

    public void SetCurrentPlanet(Planet p) {
        if (p != currentPlanet)
        {
            currentPlanet = p;
            cam.target = currentPlanet.transform;
            offset = Vector3.up * (currentPlanet.transform.localScale.x + 1);
            moving = false;
            scanButton.SetActive(true);
        }
    }

    public void SetDestinationPlanet(int planetIndex)
    {
        if (Planet.planetList[planetIndex] != currentPlanet)
        {
            currentPlanet = null;
            destinationPlanet = Planet.planetList[planetIndex];
            cam.target = this.transform;
            moving = true;
            scanButton.SetActive(false);
        }
    }
}
