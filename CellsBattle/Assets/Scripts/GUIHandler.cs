using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {
    public Transform button_PLAY;
    public Transform button_RESIGN;
    public Transform button_ENDTURN;
    public Transform button_SEND;
    public Transform InputField;
    private InputField fieldinput;
    public Transform TextPlayer;
    public Transform TextMenu;
    public Transform Carte;
    public Transform WinPlyr1;
    public Transform WinPlyr2;

    private Grille grid;

    private Case casePlyr1;
    private Case casePlyr2;

    public const int _INGUI = 0;
    public const int _NEUTRAL = 0;
    public const int _PLAYER1 = 1;
    public const int _PLAYER2 = 2;

    public static bool suspendActions = false;
    public static bool doSend = false;
    public static bool hasReceivedInt = false;
    public static int cellsToSend = 0;

    private static int _stateOfGame = _INGUI;

    private int CellsAtFirst1 = 5;
    private int CellsAtFirst2 = 5;

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

        Button sendbtn = button_SEND.GetComponent<Button> ();
        fieldinput = InputField.GetComponent<InputField> ();

        button_SEND.gameObject.SetActive (false);
        InputField.gameObject.SetActive (false);
        button_RESIGN.gameObject.SetActive (false);
        button_ENDTURN.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive (true);
        WinPlyr1.gameObject.SetActive (false);
        WinPlyr2.gameObject.SetActive (false);

        grid = Carte.GetComponent<Grille> ();
        casePlyr1 = grid.CaseDepart1.GetComponent<Case> ();
        casePlyr2 = grid.CaseDepart2.GetComponent<Case> ();

        casePlyr1.stateOfCase = _PLAYER1;
        casePlyr2.stateOfCase = _PLAYER2;
        casePlyr1.pop = CellsAtFirst1;
        casePlyr2.pop = CellsAtFirst2;

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

        if (doSend) {
            doSend = false;
            suspendActions = true;
            StartCoroutine (AskAndSendCells ());
        }

    }

    void TaskOnClickPlayButton () {
        if (!suspendActions) {
            button_PLAY.gameObject.SetActive (false);
            button_RESIGN.gameObject.SetActive (true);
            button_ENDTURN.gameObject.SetActive (true);
            // InputField.gameObject.SetActive (true);
            TextPlayer.gameObject.SetActive (true);
            TextMenu.gameObject.SetActive (false);

            stateOfGame = _PLAYER1;
        }
    }

    void TaskOnClickResignButton () {
        if (!suspendActions) {
            ResetGame ();
        }
    }

    void TaskOnClickEndTurnButton () {
        if (!suspendActions) {
            if (stateOfGame == _PLAYER1) {
                grid.Duplicate (_PLAYER1);
                stateOfGame = _PLAYER2;
                ResetMidTurn ();
            } else {
                grid.Duplicate (_PLAYER2);
                stateOfGame = _PLAYER1;
                ResetTurn ();
            }
        }
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
        // InputField.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive (true);
        stateOfGame = _INGUI;
        Carte.GetComponent<Grille> ().ResetMap ();

        casePlyr1.stateOfCase = _PLAYER1;
        casePlyr2.stateOfCase = _PLAYER2;
        casePlyr1.pop = CellsAtFirst1;
        casePlyr2.pop = CellsAtFirst2;
    }

    void ResetTurn () {
        grid.ResetGridTurn ();
    }

    void ResetMidTurn () {
        grid.ResetGridMidTurn ();
    }

    public IEnumerator AskAndSendCells () {
        button_SEND.gameObject.SetActive (true);
        InputField.gameObject.SetActive (true);
        while (!hasReceivedInt) {
            yield return new WaitForSeconds (1.0f);
            // Debug.Log ("wait");
        }
        button_SEND.gameObject.SetActive (false);
        InputField.gameObject.SetActive (false);
        Grille.SendCells (cellsToSend);

        Grille.hasCaseDeReference = false;
        Grille.hasCaseDeReference2 = false;
        hasReceivedInt = false;
        suspendActions = false;
    }

    public void OnSubmit () {
        cellsToSend = int.Parse (fieldinput.text);
        // Debug.Log ("You sent " + cellsToSend + " cells !");
        fieldinput.text = "";
        hasReceivedInt = true;
        // return true;
    }
}