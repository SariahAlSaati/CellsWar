using UnityEngine;
using UnityEngine.Tilemaps;

public class Grille : MonoBehaviour {
    public Transform CaseDepart1;
    public Transform CaseDepart2;
    public static int Scale = 17;
    private Transform map;
    public static Transform caseDeReference;
    public static bool hasCaseDeReference = false;

    void Start () {
        map = GetComponent<Transform> ();
        if (map == null) Debug.Log ("pas de map");
        // Case case1 = CaseDepart1.GetComponent<Case>();
        //Duplicate();
        // Debug.Log(case1.pop);
        CaseDepart1.GetComponent<Case> ().pop = 12;
        CaseDepart2.GetComponent<Case> ().pop = 8;
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

    void Duplicate () {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");

            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        mycase.Duplicate ();
                        //Debug.Log("duplication");

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
        CaseDepart1.GetComponent<Case> ().pop = 12;
        CaseDepart2.GetComponent<Case> ().pop = 8;
    }

    public static bool SendCells (Transform caseFrom, Transform caseTo, int numberOfCells) {
        
        // Debug.Log ("cells sent");
        Case myCaseFrom = caseFrom.GetComponent<Case> ();
        Case myCaseTo = caseTo.GetComponent<Case> ();

        if (myCaseFrom.hasSentCells) {
            // Debug.Log ("has sent cells");
            return false;
        }
        // Debug.Log (myCaseFrom.pop);

        if (myCaseFrom.pop < numberOfCells) {
            // Debug.Log ("not enough population");
            return false;
        }

        if (myCaseTo.stateOfCase == Case._NEUTRAL) {

            // Debug.Log ("to case neutral");

            myCaseFrom.pop -= numberOfCells;
            myCaseTo.pop += numberOfCells;
            myCaseTo.hasReceivedCells = true;
            myCaseTo.cellsReceived += numberOfCells;
            myCaseTo.hasChangedTeam = true;
            myCaseTo.stateOfCase = myCaseFrom.stateOfCase;
            myCaseFrom.hasSentCells = true;
            // Debug.Log (myCaseFrom.pop);
            // Debug.Log (myCaseTo.pop);

            if (myCaseFrom.pop == 0) myCaseFrom.stateOfCase = Case._NEUTRAL;
            return true;
        }

        if (myCaseFrom.stateOfCase == myCaseTo.stateOfCase) {
            // Debug.Log ("to case of same team");
            myCaseFrom.pop -= numberOfCells;
            myCaseTo.pop += numberOfCells;

            myCaseTo.hasReceivedCells = true;
            myCaseTo.cellsReceived += numberOfCells;

            if (myCaseFrom.pop == 0) myCaseFrom.stateOfCase = Case._NEUTRAL;
            return true;
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

        return true;
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