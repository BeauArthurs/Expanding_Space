using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float scrollRate;

    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(target);

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.fieldOfView > 1)
            {
                cam.fieldOfView--;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.fieldOfView < 179)
            {
                cam.fieldOfView++;
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = target.position + Vector3.one * 10;
    }

    public void SetTarget(Transform t) {
        target = t;
    }
}
