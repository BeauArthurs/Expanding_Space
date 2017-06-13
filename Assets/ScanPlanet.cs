using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScanPlanet : MonoBehaviour
{
    [SerializeField] private PlanetsInformation planetInfoObject;
    private string infoText;
    [SerializeField] private Text infoTextPanel;

    public void Scan()
    {
        infoTextPanel.text = planetInfoObject.currentInfo;
    }
}
