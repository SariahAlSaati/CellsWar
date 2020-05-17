using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInformation : MonoBehaviour {
    public Transform RulesCanvas;

    void OnMouseOver () {
        RulesCanvas.gameObject.SetActive (true);
    }

    void OnMouseExit () {
        RulesCanvas.gameObject.SetActive (false);
    }
}