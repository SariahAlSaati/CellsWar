using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Case : MonoBehaviour {

    // Contient IsNeighbour(Case deuxiemecase)
    // Population Pi, population qu'il est possible d'envoyer
    // Population Preceived: population reçue au cour du tour
    // booléen HasReceived (inutile selon Ahmed)
    // booléen HasSent (inutiles selon Ahmed)
    // booléen  HasChangedTeam

    

    private const int popmax = 20;
    public int pop;
    private int condition;

    private Animator anim;
    
    enum CaseState {
        neutral = 0,
        plyr1 = 1,
        plyr2 = 2,
    }

    CaseState casestate = CaseState.neutral;

    void Start(){
        pop = 1;
        anim = GetComponentInChildren<Animator> ();
        anim.SetInteger("AnimCaseState",(int)casestate);
    }

    void Update(){
    }

    //gère l'envoi des cellules avec varible globale de MapManager casedereference

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            if (casestate == CaseState.plyr1) casestate = CaseState.plyr2 ;
            else casestate = CaseState.plyr1;
            
            anim.SetInteger("AnimCaseState",(int)casestate);
        }
 }
    

}