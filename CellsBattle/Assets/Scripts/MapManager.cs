using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{   
    // contient: Duplication() qui va être appelée par GUIHandler à la fin de chaque tour
    // contient booleéen hasGameBeenWon, qui va être appelé par GUIHandler


    //contient variables globales cases de départ

    //contient variable globale de type Case caseDeReference, et booléan IsReferenceSelected

    //contient Send(int n, Case casededepart, Case casedarrivee)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Duplication()
    {
        // Parcourt chaque case
        // Duplique la quantité de cellule  Pi
        // Remet toutes les variables internes de la case au state inital (booléen, Pi, Pf ...)
    }
}
