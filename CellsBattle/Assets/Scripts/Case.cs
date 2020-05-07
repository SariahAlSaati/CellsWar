using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour {

    // Contient IsNeighbour(Case deuxiemecase)
    // Population Pi, population qu'il est possible d'envoyer
    // Population Preceived: population reçue au cour du tour
    // booléen HasReceived (inutile selon Ahmed)
    // booléen HasSent (inutiles selon Ahmed)
    // booléen  HasChangedTeam

    // //Variaables Ahmed
    // public int popnew;
    // public int popold;

    private const int popmax = 20;
    public int pop;
    //private int condition;

    private Transform position;

    private Animator anim;

    public const int _NEUTRAL = 0;
    public const int _PLAYER1 = 1;
    public const int _PLAYER2 = 2;

    int _stateOfCase = _NEUTRAL;

    public int stateOfCase {
        get {
            return _stateOfCase;
        }
        set {
            _stateOfCase = value;
        }
    }

    public bool hasReceivedCells = false;
    public bool hasChangedTeam = false;
    public int cellsReceived = 0;
    public bool hasSentCells = false;

    void Start () {
        // popnew = 0;
        // popold = 0;
        anim = GetComponentInChildren<Animator> ();
        anim.SetInteger ("AnimCaseState", stateOfCase);
        position = GetComponent<Transform> ();
    }

    void Update () {
        // pop = popnew + popold;
        anim.SetInteger ("AnimCaseState", stateOfCase);
    }

    //gère l'envoi des cellules avec les variables statiques Grille.caseDeReference et GUIHandler.suspendActions

    void OnMouseOver () {
        if (Input.GetMouseButtonDown (0) && !GUIHandler.suspendActions && GUIHandler.stateOfGame != GUIHandler._INGUI) {

            if (!Grille.hasCaseDeReference) {
                if (GUIHandler.stateOfGame == stateOfCase) {
                    Grille.hasCaseDeReference2 = false;
                    Grille.hasCaseDeReference = true;
                    Grille.caseDeReference = position;

                    
                }
            } else { //hasCaseDeReference
                if (Grille.isNeighbour (position, Grille.caseDeReference)) {
                    Grille.caseDeReference2 = position;
                    Grille.hasCaseDeReference2 = true;

                    GUIHandler.doSend = true;
                    

                }

                Grille.hasCaseDeReference = false;
            }
        }
    }

    // private IEnumerator WaitForInput(Transform caseFrom, Transform caseTo) {
    //     while (!GUIHandler.hasReceivedInt) {
    //         yield return new WaitForSeconds(3.0f);
    //         Debug.Log("wait");
    //     }
    //     bool sent = Grille.SendCells (caseFrom, caseTo, GUIHandler.cellsToSend);
    //     Debug.Log(sent);
    // }

    public void Duplicate () {
        pop = System.Math.Min (2 * (pop - cellsReceived) + cellsReceived, popmax);
    }

    public void ResetCase () {
        ResetStateEndTurn ();
        // popnew = 0;
        // popold = 0;
        pop = 0;
        stateOfCase = _NEUTRAL;

    }

    public void ResetStateEndTurn () {
        // popold = popold + popnew;
        // popnew = 0;
        hasReceivedCells = false;
        hasChangedTeam = false;
        cellsReceived = 0;
        hasSentCells = false;

    }
    public void ResetStateMidTurn () {
        hasSentCells = false;
    }
}