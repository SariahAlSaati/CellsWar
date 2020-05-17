using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Grille : MonoBehaviour {
    public Transform CaseDepart1;
    public Transform CaseDepart2;
    public static int Scale = 17;
    private Transform map;
    public static Transform caseDeReference;
    public static bool hasCaseDeReference = false;

    public static Transform caseDeReference2;
    public static bool hasCaseDeReference2 = false;

    void Start () {
        map = GetComponent<Transform> ();
        if (map == null) Debug.Log ("pas de map");
        

        CaseDepart1.Find("Canvas").gameObject.SetActive (true);
        CaseDepart2.Find("Canvas").gameObject.SetActive (true);
    }

    public static bool isNeighbour (Transform case1, Transform case2) {
        Vector3 position1 = case1.position;
        Vector3 position2 = case2.position;

        // Debug.Log(System.Math.Abs(position1.x - position2.x)*Scale);

        if (System.Math.Abs (position1.x - position2.x) > 1.01 * Scale) return false;

        // Debug.Log(System.Math.Abs(position1.y - position2.y)*Scale);

        if (System.Math.Abs (position1.y - position2.y) > 0.76 * Scale) return false;

        return true;
    }

    public void Duplicate (int state) {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");

            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        if (mycase.stateOfCase == state) {
                            mycase.Duplicate ();
                            // Debug.Log(mycase.pop);
                        }

                    }
                }
            }
        }
    }

    public void ResetMap () {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");
            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        mycase.ResetCase ();
                        //Debug.Log("duplication");

                    }
                }
            }
        }
        CaseDepart1.Find("Canvas").gameObject.SetActive (true);
        CaseDepart2.Find("Canvas").gameObject.SetActive (true);
    }

    public static void SendCells (int numberOfCells) {

        Case myCaseFrom = caseDeReference.GetComponent<Case> ();
        Case myCaseTo = caseDeReference2.GetComponent<Case> ();

        if (caseDeReference.position == caseDeReference2.position) {
            GUIHandler.ErrorMessage = "Même case de départ et d'arrivée.";
            GUIHandler.IsError = true;
            return;
        }

        // Debug.Log (myCaseFrom.pop);

        if (myCaseFrom.pop == numberOfCells) {
            GUIHandler.ErrorMessage = "Vous ne pouvez pas vider la case.";
            GUIHandler.IsError = true;
            return;
        }

        if ((myCaseFrom.pop - myCaseFrom.cellsReceived) < (numberOfCells)) {
            GUIHandler.ErrorMessage = "Pas assez de cellules actives.";
            GUIHandler.IsError = true;
            return;
        }

        if (myCaseTo.stateOfCase == Case._NEUTRAL) {

            // Debug.Log ("to case neutral");

            if (numberOfCells == 0) return;

            myCaseTo.transform.Find("Canvas").gameObject.SetActive (true);

            myCaseFrom.pop -= numberOfCells;
            myCaseTo.pop += numberOfCells;
            myCaseTo.hasReceivedCells = true;
            myCaseTo.cellsReceived += numberOfCells;
            myCaseTo.hasChangedTeam = true;
            myCaseTo.stateOfCase = myCaseFrom.stateOfCase;
            myCaseFrom.hasSentCells = true;
            // Debug.Log (myCaseFrom.pop);
            // Debug.Log (myCaseTo.pop);

            if (myCaseFrom.pop == 0) {
                myCaseFrom.stateOfCase = Case._NEUTRAL;
                myCaseFrom.transform.Find("Canvas").gameObject.SetActive (false);
            }
            return;
        }

        if (myCaseFrom.stateOfCase == myCaseTo.stateOfCase) {
            // Debug.Log ("to case of same team");
            myCaseFrom.pop -= numberOfCells;
            // myCaseTo.pop += numberOfCells;
            int mem = myCaseTo.pop;

            myCaseTo.pop = System.Math.Min (myCaseTo.pop + numberOfCells, 20);

            myCaseTo.hasReceivedCells = true;
            myCaseTo.cellsReceived += myCaseTo.pop - mem;

            if (myCaseFrom.pop == 0) myCaseFrom.stateOfCase = Case._NEUTRAL;
            return;
        }
        //cases are from different teams
        // Debug.Log ("to case of adverse team");

        myCaseFrom.pop -= numberOfCells;
        myCaseFrom.hasSentCells = true;

        if (numberOfCells > myCaseTo.pop) { //case go to attacker
            myCaseTo.hasSentCells = false;
            myCaseTo.hasChangedTeam = true;
            myCaseTo.hasReceivedCells = true;
            myCaseTo.pop = numberOfCells - myCaseTo.pop;;
            myCaseTo.cellsReceived = myCaseTo.pop;
            myCaseTo.stateOfCase = myCaseFrom.stateOfCase;
        } else if (numberOfCells == myCaseTo.pop) { //case back to neutral
            myCaseTo.ResetCase ();
        } else { //Case remain to the same player
            myCaseTo.pop -= numberOfCells;
            myCaseTo.cellsReceived = System.Math.Max (myCaseTo.cellsReceived - numberOfCells, 0);
        }

        if (myCaseFrom.pop == 0) myCaseFrom.stateOfCase = Case._NEUTRAL;

    }

    public void ResetGridTurn () {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");
            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        mycase.ResetStateEndTurn ();
                        //Debug.Log("reset case turn");

                    }
                }
            }
        }
    }

    public void ResetGridMidTurn () {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");
            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        mycase.ResetStateMidTurn ();
                        //Debug.Log("reset case turn");

                    }
                }
            }
        }
    }
}