using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {
    public Transform button_PLAY;
    public Transform button_RESIGN;
    public Transform button_ENDTURN;
    public Transform InputField;
    public Transform TextPlayer;
    public Transform TextMenu;
    public Transform Carte;
    public Transform WinPlyr1;
    public Transform WinPlyr2;

    private Case casePlyr1;
    private Case casePlyr2;

    public const int _INGUI = 0;
    public const int _NEUTRAL = 0;
    public const int _PLAYER1 = 1;
    public const int _PLAYER2 = 2;

    public static bool suspendActions = false;

    private static int _stateOfGame = _INGUI;

    public static int stateOfGame {
        get {
            return _stateOfGame;
        }
        set {
            _stateOfGame = value;
        }
    }

    // Start is called before the first frame update
    void Start () {
        Button playbtn = button_PLAY.GetComponent<Button> ();
        playbtn.onClick.AddListener (TaskOnClickPlayButton);

        Button resignbtn = button_RESIGN.GetComponent<Button> ();
        resignbtn.onClick.AddListener (TaskOnClickResignButton);

        Button endTurnbtn = button_ENDTURN.GetComponent<Button> ();
        endTurnbtn.onClick.AddListener (TaskOnClickEndTurnButton);

        button_RESIGN.gameObject.SetActive (false);

        button_ENDTURN.gameObject.SetActive (false);
        InputField.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive (true);
        WinPlyr1.gameObject.SetActive (false);
        WinPlyr2.gameObject.SetActive (false);

        Grille grid = Carte.GetComponent<Grille> ();
        casePlyr1 = grid.CaseDepart1.GetComponent<Case> ();
        casePlyr2 = grid.CaseDepart2.GetComponent<Case> ();

        casePlyr1.stateOfCase = _PLAYER1;
        casePlyr2.stateOfCase = _PLAYER2;

    }

    // Update is called once per frame
    void Update () {
        if (casePlyr1.stateOfCase == _PLAYER2) {
            casePlyr1.stateOfCase = _NEUTRAL;
            StartCoroutine (Plyr2WinAnim ());
        } else if (casePlyr2.stateOfCase == _PLAYER1) {
            casePlyr2.stateOfCase = _NEUTRAL;
            StartCoroutine (Plyr1WinAnim ());
        }

    }

    void TaskOnClickPlayButton () {
        button_PLAY.gameObject.SetActive (false);
        button_RESIGN.gameObject.SetActive (true);
        button_ENDTURN.gameObject.SetActive (true);
        InputField.gameObject.SetActive (true);
        TextPlayer.gameObject.SetActive (true);
        TextMenu.gameObject.SetActive (false);
        stateOfGame = _PLAYER1;
    }

    void TaskOnClickResignButton () {
        button_RESIGN.gameObject.SetActive (false);
        button_ENDTURN.gameObject.SetActive (false);
        button_PLAY.gameObject.SetActive (true);
        InputField.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive (true);
        stateOfGame = _INGUI;
    }

    void TaskOnClickEndTurnButton () {
        if (stateOfGame == _PLAYER1) stateOfGame = _PLAYER2;
        else stateOfGame = _PLAYER1;
    }

    private IEnumerator Plyr2WinAnim () {
        suspendActions = true;
        WinPlyr2.gameObject.SetActive (true);
        yield return new WaitForSeconds (3.0f);
        WinPlyr2.gameObject.SetActive (false);

        ResetGame ();
        suspendActions = false;
    }

    private IEnumerator Plyr1WinAnim () {
        suspendActions = true;
        WinPlyr1.gameObject.SetActive (true);
        yield return new WaitForSeconds (3.0f);
        WinPlyr1.gameObject.SetActive (false);

        ResetGame ();
        suspendActions = false;

    }

    void ResetGame () {
        button_RESIGN.gameObject.SetActive (false);
        button_ENDTURN.gameObject.SetActive (false);
        button_PLAY.gameObject.SetActive (true);
        InputField.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive (true);
        stateOfGame = _INGUI;
        Carte.GetComponent<Grille> ().ResetMap ();

        casePlyr1.stateOfCase = _PLAYER1;
        casePlyr2.stateOfCase = _PLAYER2;
    }

}