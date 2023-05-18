using UnityEngine;

public class SpawnBar : MonoBehaviour
{
    public enum Orientation{
        vertical,
        horizontal
    };

    [SerializeField] GameObject     spawnBar        ;   // object (prefab) to spawn
    
    BarLevelVars                    vars            ;
    GameObject[]                    bar             ;   // array of objects to display

    const float                     unit        = 1 ;   // a unit...

    // Start is called before the first frame update
    void Start()
    {
        vars = BarLevelVars.instance                ;
        spawnObjects(vars.numObjects)               ;
    }   

    void Update(){ }//spawnObjects(numberOfObjects); }                                        // for debugging purposes only

    void spawnObjects(uint num){
        bar                 = new GameObject[vars.numObjects]               ;   // array of objects

        for (int i = 0; i < vars.numObjects; i++){
            bar[i] = Instantiate(spawnBar, SpawnPosition(vars.center, vars.numObjects, i), vars.GetRotation()); //, transform);
            
            Renderer renderer       = bar[i].GetComponent<Renderer>()       ;   // get gameobject's renderer
            Material uniqueMaterial = renderer.material                     ;   // get gameobject's material
            uniqueMaterial.SetColor ("_Color", vars.color)                  ;   // change gameobject's material
        }
    }

    Vector3 SpawnPosition(Vector3 center, uint numberOfObjects, int objNumber){
        float pos = ( objNumber * 2 * unit) - ( ( numberOfObjects * 2 ) - 1 ) / 2 ;

        return new Vector3( 
                ( center.x + pos ) * ((int)( vars.orientation + 1 ) %2 )    ,   // x component for vertical placement
                ( center.y + pos ) *  (int)  vars.orientation               ,   // y component for horizontal placement
                0                                                 )         ;   // z component -> not used
    }
}
