using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo 
{   
    public const int _INGUI = 0;
    public const int _PLAYER1 = 1;
	public const int _PLAYER2 = 2;

	

	private int _state = _INGUI;
    
    public int state {
				get {
						return _state;
				}
				set {
						_state = value;
				}
		}
    
    Player player1 = new Player ();
	Player player2 = new Player ();

   
}
