using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour
{
    public Transform button_PLAY;
	public Transform button_RESIGN;
    public Transform button_ENDTURN;
    public Transform InputField;
    public Transform TextPlayer;
    public Transform TextMenu;

    public const int _INGUI = 0;
    public const int _PLAYER1 = 1;
	public const int _PLAYER2 = 2;

	

	private static int _state = _INGUI;
    
    public static int state {
				get {
						return _state;
				}
				set {
						_state = value;
				}
		}
    

    // Start is called before the first frame update
    void Start()
    {
        Button playbtn = button_PLAY.GetComponent<Button> ();
        playbtn.onClick.AddListener(TaskOnClickPlayButton);

        Button resignbtn = button_RESIGN.GetComponent<Button> ();
        resignbtn.onClick.AddListener(TaskOnClickResignButton);

        Button endTurnbtn = button_ENDTURN.GetComponent<Button> ();
        endTurnbtn.onClick.AddListener(TaskOnClickEndTurnButton);

        button_RESIGN.gameObject.SetActive (false);
<<<<<<< Updated upstream

=======
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
        // grid.Duplicate(_PLAYER1);

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
        // InputField.gameObject.SetActive (true);
        TextPlayer.gameObject.SetActive (true);
        TextMenu.gameObject.SetActive (false);
        stateOfGame = _PLAYER1;
    }

    void TaskOnClickResignButton () {
        ResetGame ();
    }

    void TaskOnClickEndTurnButton () {
<<<<<<< Updated upstream
        if (stateOfGame == _PLAYER1) {
            grid.Duplicate(_PLAYER1);
            stateOfGame = _PLAYER2;
            ResetMidTurn ();

        } else {
            grid.Duplicate(_PLAYER2);
            stateOfGame = _PLAYER1;
            ResetTurn ();
=======
        if (!suspendActions) {
            if (stateOfGame == _PLAYER1) {
                // grid.Duplicate (_PLAYER1);
                stateOfGame = _PLAYER2;
                ResetMidTurn ();
            } else {
                grid.Duplicate (_PLAYER1);
                grid.Duplicate (_PLAYER2);
                stateOfGame = _PLAYER1;
                ResetTurn ();
            }
>>>>>>> Stashed changes
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
>>>>>>> Stashed changes
        button_ENDTURN.gameObject.SetActive (false);
        InputField.gameObject.SetActive (false);
        TextPlayer.gameObject.SetActive (false);
        TextMenu.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        
   }

   void TaskOnClickPlayButton(){
       button_PLAY.gameObject.SetActive (false);
       button_RESIGN.gameObject.SetActive (true);
       button_ENDTURN.gameObject.SetActive (true);
       InputField.gameObject.SetActive (true);
       TextPlayer.gameObject.SetActive (true);
       TextMenu.gameObject.SetActive(false);
       state = _PLAYER1;
   }

   void TaskOnClickResignButton(){
       button_RESIGN.gameObject.SetActive (false);
       button_ENDTURN.gameObject.SetActive (false);
       button_PLAY.gameObject.SetActive (true);
       InputField.gameObject.SetActive (false);
       TextPlayer.gameObject.SetActive (false);
       TextMenu.gameObject.SetActive(true);
       state = _INGUI;       
   }

   void TaskOnClickEndTurnButton(){
       if (state == _PLAYER1) state = _PLAYER2;
       else state = _PLAYER1;
   }
}
