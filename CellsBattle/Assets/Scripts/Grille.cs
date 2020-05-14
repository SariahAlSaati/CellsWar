using UnityEngine;

public class Grille : MonoBehaviour {
    public Transform CaseDepart1;
    public Transform CaseDepart2;
    public static int Scale = 17;
<<<<<<< Updated upstream
    
    
    void Start() {
        Case case1 = CaseDepart1.GetComponent<Case>();
        Debug.Log(case1.pop);
    }
   
=======
    private Transform map;
    public static Transform caseDeReference;
    public static bool hasCaseDeReference = false;

    void Start () {
        map = GetComponent<Transform> ();
        if (map == null) Debug.Log ("pas de map");
        // Case case1 = CaseDepart1.GetComponent<Case>();
       
        // Debug.Log(case1.pop);
        CaseDepart1.GetComponent<Case> ().pop = 12;
        CaseDepart2.GetComponent<Case> ().pop = 8;
        // Duplicate(1);
         }
>>>>>>> Stashed changes

    bool isNeighbour(Transform case1, Transform case2){
        Vector3 position1 = case1.position;
        Vector3 position2 = case2.position;

        // Debug.Log(System.Math.Abs(position1.x - position2.x)*Scale);

        if (System.Math.Abs(position1.x - position2.x) >1.01*Scale) return false;

        // Debug.Log(System.Math.Abs(position1.y - position2.y)*Scale);

<<<<<<< Updated upstream
        if (System.Math.Abs(position1.y - position2.y) >0.76*Scale) return false;
=======
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
                        if (mycase.stateOfCase==state){
                            mycase.Duplicate();
                            Debug.Log(mycase.pop);
                        }
                        // Debug.Log(mycase.pop);

                        // Debug.Log("pas de state");

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
                        Debug.Log("duplication");

                    }
                }
            }
        }
        CaseDepart1.GetComponent<Case> ().pop = 12;
        CaseDepart2.GetComponent<Case> ().pop = 8;
    }

<<<<<<< Updated upstream
    public static bool SendCells (Transform caseFrom, Transform caseTo, int numberOfCells) {
>>>>>>> Stashed changes
=======
    public static void SendCells (int numberOfCells) {
        
        // Debug.Log ("cells sent");
        Case myCaseFrom = caseDeReference.GetComponent<Case> ();
        Case myCaseTo = caseDeReference2.GetComponent<Case> ();

        // if (myCaseFrom.hasSentCells) {
        //     Debug.Log ("has sent cells");
        //     return;
        // }

        if (caseDeReference.position == caseDeReference2.position) {
            Debug.Log("These are the same cell ! ");
            return;
        }

        // Debug.Log (myCaseFrom.pop);

        if (myCaseFrom.pop == numberOfCells) {
            Debug.Log("You cannot leave the case !");
            return;
        }

        if ((myCaseFrom.pop - myCaseFrom.cellsReceived) < (numberOfCells)) {
            Debug.Log ("Not enough population, you cannot send one cell two times in the same turn !");
            return;
        }

        if (myCaseTo.stateOfCase == Case._NEUTRAL) {

            // Debug.Log ("to case neutral");

            if (numberOfCells == 0) return;
        
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
            return;
        }

        if (myCaseFrom.stateOfCase == myCaseTo.stateOfCase) {
            // Debug.Log ("to case of same team");
            myCaseFrom.pop -= numberOfCells;
            // myCaseTo.pop += numberOfCells;
            int mem = myCaseTo.pop;

            myCaseTo.pop = System.Math.Min(myCaseTo.pop + numberOfCells, 20);

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

>>>>>>> Stashed changes
        
        return true;
    }


}