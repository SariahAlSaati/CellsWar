using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopCaseText : MonoBehaviour {
    Text text;
    Case mycase;

    void Start () {
        text = GetComponent<Text> ();

        mycase = gameObject.transform.parent.parent.GetComponent<Case> ();
    }

    // Update is called once per frame
    void Update () {
        text.text = mycase.pop.ToString ();
    }
}