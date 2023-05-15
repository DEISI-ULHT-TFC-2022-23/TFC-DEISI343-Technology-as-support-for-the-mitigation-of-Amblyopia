using UnityEngine;

public class DebugCheat : MonoBehaviour
{
    CollisionHandler cl;
    Component[] components;

    bool collisions = true;
    // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<CollisionHandler>();
        components = gameObject.GetComponentsInChildren(typeof(Collider));
        collisions = true;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.L)){
            cl.NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            cl.ReloadLevel();
        }
        if (Input.GetKeyDown(KeyCode.C)){
            Debug.Log("Tecla C");
            if (collisions) DisableCollisions();
            else EnableCollisions();
        }
    }

    void CheatLoadNextLevel(){

    }

    void CheatReloadLevel(){

    }

    void CollisionToggle(){

    }

    void DisableCollisions(){
        Debug.Log("Entrou!!");
        collisions = false;
        var stAux = "";
        foreach (Collider collider in components){
            stAux = collider.name + ": " + collider.enabled + " |->| ";
            collider.enabled = false;
            Debug.Log(stAux + collider.enabled);
        }
    }

    void EnableCollisions(){
        collisions = true;
        foreach (Collider collider in components){
            collider.enabled = true;
        }
    }
}
