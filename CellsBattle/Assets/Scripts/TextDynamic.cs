using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDynamic : MonoBehaviour {
    Text text;

    private const int _INGUI = 0;
    private const int _DmdCaseDepart = 1;
    private const int _DmdCaseArrivee = 2;
    private const int _DmdNmbrCellule = 3;

    public static int TextState = _INGUI;

    // Start is called before the first frame update
    void Start () {
        text = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update () {
        if (TextState == _INGUI) {
            text.text = "Appuyez sur Play pour commencer ...";
        } else if (TextState == _DmdCaseDepart) {
            text.text = "Choisissez une case de départ.";
        } else if (TextState == _DmdCaseArrivee) {
            text.text = "Choisissez une case de destination.";
        } else if (TextState == _DmdNmbrCellule) {
            text.text = "Combien de cellules ?";
        }
    }
}