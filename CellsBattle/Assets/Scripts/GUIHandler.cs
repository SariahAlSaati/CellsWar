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
