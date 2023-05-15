using UnityEngine;

public class BarLevelVars : MonoBehaviour
{
    public enum Orientation {
        Vertical,
        horizontal
    }

    [SerializeField] public static  BarLevelVars    instance    ;               // reference to this class
    [SerializeField] public         int             level       ;               // level being played
    [SerializeField] public         int             difficulty  ;               // difficulty
    [SerializeField] public         uint            numObjects  ;               // number of objects to spawn / draw
    [SerializeField] public         Color           color       ;               // oject's color
    [SerializeField] public         Orientation     orientation ;               // Vertical / Horizontal
    [SerializeField] public         Vector3         center      ;               // reference "center" for object spawn

    void Awake() 
    {
        if (instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        //init();
    }

    void init(){
        level       = 0                             ;
        difficulty  = 0                             ;
        numObjects  = 3                             ;
        orientation = Orientation.Vertical          ;
        color       = new Color (255, 0, 0, 255)    ;
    }

    public Quaternion GetRotation(){
        return Quaternion.Euler(0,0,90 * (int)orientation);
    }

}
