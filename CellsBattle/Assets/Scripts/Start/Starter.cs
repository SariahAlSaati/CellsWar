using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour {

    public Transform Intro;
    public Transform Regles;
    public Transform Choix;

    // Start is called before the first frame update

    private const int _INTRO = 0;
    private const int _REGLES = 1;
    private const int _CHOIX = 2;
    private const int _JEU = 3;

    private int stateScene = _INTRO;
    private int iteration = 0;

    // Update is called once per frame
    void Update () {
        if ((stateScene != _JEU) && (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Space))) {
            OnClickPass ();
        }
    }

    public void OnClickPass () {
        if (stateScene == _INTRO) {
            if (iteration == 0) {
                Intro.Find ("Story").gameObject.SetActive (true);
                iteration = 1;
            } else if (iteration == 1) {
                iteration = 0;
                stateScene = _REGLES;
                Intro.gameObject.SetActive (false);
                Regles.gameObject.SetActive (true);
            }
        } else if (stateScene == _REGLES) {
            if (iteration == 0) {
                Regles.Find ("Regles").gameObject.SetActive (true);
                iteration = 1;
            } else if (iteration == 1) {
                iteration = 2;
                Regles.Find ("Contraintes").gameObject.SetActive (true);
            } else if (iteration == 2) {
                iteration = 0;
                stateScene = _CHOIX;
                Regles.gameObject.SetActive (false);
                Choix.gameObject.SetActive (true);
            }
        }
    }
}