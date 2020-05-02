using UnityEngine;
using UnityEngine.Tilemaps;

public class Grille : MonoBehaviour {
    public Transform CaseDepart1;
    public Transform CaseDepart2;
    public static int Scale = 17;
    private Transform map;

    void Start () {
        map = GetComponent<Transform> ();
        if (map == null) Debug.Log ("pas de map");
        // Case case1 = CaseDepart1.GetComponent<Case>();
        //Duplicate();
        // Debug.Log(case1.pop);
    }

    static bool isNeighbour (Transform case1, Transform case2) {
        Vector3 position1 = case1.position;
        Vector3 position2 = case2.position;

        // Debug.Log(System.Math.Abs(position1.x - position2.x)*Scale);

        if (System.Math.Abs (position1.x - position2.x) > 1.01 * Scale) return false;

        // Debug.Log(System.Math.Abs(position1.y - position2.y)*Scale);

        if (System.Math.Abs (position1.y - position2.y) > 0.76 * Scale) return false;

        return true;
    }

    void Duplicate () {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");

            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        mycase.Duplicate ();
                        //Debug.Log("duplication");

                    }
                }
            }
        }
    }

    public void ResetMap () {
        foreach (Transform tilemap in map) {
            if (tilemap == null) Debug.Log ("pas de tilemap");
            foreach (Transform children in tilemap) {
                if (children == null) Debug.Log ("pas de case");

                else {

                    //Debug.Log(children.position);
                    Case mycase = children.GetComponent<Case> ();

                    if (mycase == null) Debug.Log ("pas de composante case");

                    else {
                        mycase.ResetCase ();
                        //Debug.Log("duplication");

                    }
                }
            }
        }
    }

}