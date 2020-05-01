using UnityEngine;

public class Grille : MonoBehaviour {
    public Transform CaseDepart1;
    public Transform CaseDepart2;
    public static int Scale = 17;
    
    
    void Start() {
        Case case1 = CaseDepart1.GetComponent<Case>();
        Debug.Log(case1.pop);
    }
   

    bool isNeighbour(Transform case1, Transform case2){
        Vector3 position1 = case1.position;
        Vector3 position2 = case2.position;

        // Debug.Log(System.Math.Abs(position1.x - position2.x)*Scale);

        if (System.Math.Abs(position1.x - position2.x) >1.01*Scale) return false;

        // Debug.Log(System.Math.Abs(position1.y - position2.y)*Scale);

        if (System.Math.Abs(position1.y - position2.y) >0.76*Scale) return false;
        
        return true;
    }


}