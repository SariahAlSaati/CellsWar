using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Case : MonoBehaviour {

    private Transform position;
    private Animator anim;

    public const int _NEUTRAL = 0;
    public const int _PLAYER1 = 1;
    public const int _PLAYER2 = 2;
    private const int popmax = 20;
    private const int foodmax = 35;

    public int popToCreate;
    public int pop;

    public bool hasReceivedCells = false;
    public bool hasChangedTeam = false;
    public int cellsReceived = 0;
    public bool hasSentCells = false;

    int _stateOfCase = _NEUTRAL;
    public int stateOfCase {
        get {
            return _stateOfCase;
        }
        set {
            _stateOfCase = value;
        }
    }

    void Start () {
        anim = GetComponentInChildren<Animator> ();
        anim.SetInteger ("AnimCaseState", stateOfCase);
        position = GetComponent<Transform> ();
        popToCreate = foodmax;
    }

    void Update () {
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
                    anim.SetBool ("selected", true);
                    TextDynamic.TextState = 2;

                }
            } else { //hasCaseDeReference
                if (Grille.isNeighbour (position, Grille.caseDeReference)) {
                    Grille.caseDeReference2 = position;
                    Grille.hasCaseDeReference2 = true;

                    GUIHandler.doSend = true;
                    anim.SetBool ("selected", true);
                } else { Grille.caseDeReference.GetComponentInChildren<Animator> ().SetBool ("selected", false); }
                TextDynamic.TextState = 1;
                Grille.hasCaseDeReference = false;

            }
        }
    }

    public void Duplicate () {
        int newpop = System.Math.Min (2 * (pop - cellsReceived) + cellsReceived, popmax);
        if (newpop - pop <= popToCreate) {
            popToCreate -= newpop - pop;
            pop = newpop;
        } else {
            pop += popToCreate;
            popToCreate = 0;
        }
    }

    public void ResetCase () {
        ResetStateEndTurn ();
        pop = 0;
        popToCreate = foodmax;
        stateOfCase = _NEUTRAL;
        position.Find ("Canvas").gameObject.SetActive (false);

    }

    public void ResetCaseToNeutral () {
        ResetStateEndTurn ();
        pop = 0;
        stateOfCase = _NEUTRAL;
        position.Find ("Canvas").gameObject.SetActive (false);

    }

    public void ResetStateEndTurn () {
        hasReceivedCells = false;
        hasChangedTeam = false;
        cellsReceived = 0;
        hasSentCells = false;

    }
    public void ResetStateMidTurn () {
        hasSentCells = false;
    }
}