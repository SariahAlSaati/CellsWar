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

    //Variaables Ahmed
    private int popnew;
    private int popold;

    private const int popmax = 20;
    public int pop;
    private int condition;

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

    void Start () {
        popnew = 1;
        anim = GetComponentInChildren<Animator> ();
        anim.SetInteger ("AnimCaseState", stateOfCase);
        position = GetComponent<Transform> ();
    }

    void Update () {
        pop = popnew + popold;
        anim.SetInteger ("AnimCaseState", stateOfCase);
    }

    //gère l'envoi des cellules avec varible globale de MapManager casedereference

    void OnMouseOver () {
        if (Input.GetMouseButtonDown (0) && !GUIHandler.suspendActions) {
            if (stateOfCase == _PLAYER1) stateOfCase = _PLAYER2;
            else stateOfCase = _PLAYER1;

            anim.SetInteger ("AnimCaseState", stateOfCase);
            // popnew++;
        }
    }
    public void Duplicate () {
        popnew = 2 * popnew;
    }

    public void ResetCase() {
        popnew = 0;
        popold = 1;
        stateOfCase = _NEUTRAL;
    }

}