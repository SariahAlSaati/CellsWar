using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {
    public Transform button_PLAY;
    public Transform button_RESIGN;
    public Transform button_ENDTURN1;
    public Transform button_ENDTURN2;
    public Transform button_SEND;
    public Transform InputField;
    private InputField fieldinput;
    public Transform TextPlayer;
    public Transform TextMenu;
    public Transform TextError;
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

    public static bool IsError = false;
    public static string ErrorMessage;

    // Start is called before the first frame update
    void Start () {
        Button playbtn = button_PLAY.GetComponent<Button> ();
        playbtn.onClick.AddListener (TaskOnClickPlayButton);

        Button resignbtn = button_RESIGN.GetComponent<Button> ();
        resignbtn.onClick.AddListener (TaskOnClickResignButton);

        Button endTurnbtn1 = button_ENDTURN1.GetComponent<Button> ();
        endTurnbtn1.onClick.AddListener (TaskOnClickEndTurnButton1);

        Button endTurnbtn2 = button_ENDTURN2.GetComponent<Button> ();
        endTurnbtn2.onClick.AddListener (TaskOnClickEndTurnButton2);

        Button sendbtn = button_SEND.GetComponent<Button> ();
        fieldinput = InputField.GetComponent<InputField> ();

        button_SEND.gameObject.SetActive (false);
        InputField.gameObject.SetActive (false);
        button_RESIGN.gameObject.SetActive (false);
        button_ENDTURN1.gameObject.SetActive (false);
        button_ENDTURN2.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive (true);
        WinPlyr1.gameObject.SetActive (false);
        WinPlyr2.gameObject.SetActive (false);

        TextError.gameObject.SetActive (false);

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

        if (IsError) {
            IsError = false;
            StartCoroutine (ErrorText ());
        }

    }
    private IEnumerator ErrorText () {
        TextError.GetComponent<Text> ().text = ErrorMessage;
        TextError.gameObject.SetActive (true);

        yield return new WaitForSeconds (2.0f);

        TextError.gameObject.SetActive (false);
    }

    void TaskOnClickPlayButton () {
        if (!suspendActions) {
            button_PLAY.gameObject.SetActive (false);
            button_RESIGN.gameObject.SetActive (true);
            button_ENDTURN1.gameObject.SetActive (true);
            // InputField.gameObject.SetActive (true);
            TextPlayer.gameObject.SetActive (true);
            TextMenu.gameObject.SetActive (false);

            TextDynamic.TextState = 1;

            stateOfGame = _PLAYER1;
        }
    }

    void TaskOnClickResignButton () {
        if (!suspendActions) {
            ResetGame ();

            TextDynamic.TextState = 0;
        }
    }

    void TaskOnClickEndTurnButton1 () {
        button_ENDTURN1.gameObject.SetActive (false);
        EndTurn ();

        button_ENDTURN2.gameObject.SetActive (true);
    }

    void TaskOnClickEndTurnButton2 () {
        button_ENDTURN2.gameObject.SetActive (false);
        EndTurn ();
        button_ENDTURN1.gameObject.SetActive (true);
    }
    void EndTurn () {
        if (!suspendActions) {
            if (stateOfGame == _PLAYER1) {
                stateOfGame = _PLAYER2;
                ResetMidTurn ();
            } else {
                grid.Duplicate (_PLAYER1);
                grid.Duplicate (_PLAYER2);
                stateOfGame = _PLAYER1;
                ResetTurn ();
            }
            TextDynamic.TextState = 1;
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
        button_ENDTURN1.gameObject.SetActive (false);
        button_ENDTURN2.gameObject.SetActive (false);
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
        TextDynamic.TextState = 3;

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

        TextDynamic.TextState = 1;
    }

    public void OnSubmit () {
        cellsToSend = int.Parse (fieldinput.text);
        // Debug.Log ("You sent " + cellsToSend + " cells !");
        fieldinput.text = "";
        hasReceivedInt = true;
        // return true;
    }
}