using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopToSendText : MonoBehaviour {
    Text text;
    Case mycase;
    string pop;
    string cellsThatCanBeSend;

    void Start () {
        text = GetComponent<Text> ();

        mycase = gameObject.transform.parent.parent.GetComponent<Case> ();
    }

    // Update is called once per frame
    void Update () {
        if (mycase.cellsReceived == 0 && mycase.pop != 0) {
            cellsThatCanBeSend = (mycase.pop - 1).ToString ();
        } else {
            cellsThatCanBeSend = (mycase.pop - mycase.cellsReceived).ToString ();
        }
        text.text = cellsThatCanBeSend + "/" + mycase.popToCreate.ToString ();
    }
}