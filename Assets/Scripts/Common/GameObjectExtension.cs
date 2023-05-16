using UnityEngine;
using System.Collections;
 
public static class GameObjectExtension {
 
    public static void SetLayer(this GameObject parent, int layer, bool includeChildren = true)
    {
        parent.layer = layer;
        if (includeChildren)
        {
            foreach (Transform trans in parent.transform.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = layer;
            }
        }
    }


    public static void ApplyMaterial(this GameObject go, Material material, bool includeChildren = true){
        go.GetComponent<MeshRenderer>().material = material;

        if (includeChildren)
        {
            foreach (MeshRenderer render in go.transform.GetComponentsInChildren<MeshRenderer>(true))
            {
                render.material = material;
            }
        }
    }
}