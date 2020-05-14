using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDynamic : MonoBehaviour
{   
    Text text;

    private const int _INGUI = 0 ;
    private const int _DmdCaseDepart = 1  ;
    private const int _DmdCaseArrivee = 2 ;
    private const int _DmdNmbrCellule = 3 ;

    public static int TextState = _INGUI;

    // Start is called before the first frame update
    void Start()
    {
        text=GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (TextState == _INGUI) {
            text.text = "Click Play to begin...";
        } else if( TextState == _DmdCaseDepart) {
            text.text = "Choose a starting box...";
        } else if ( TextState == _DmdCaseArrivee) {
            text.text = "Choose a neighbouring box...";
        } else if (TextState== _DmdNmbrCellule) {
            text.text = "How many cells ?";
        }
    }
}
