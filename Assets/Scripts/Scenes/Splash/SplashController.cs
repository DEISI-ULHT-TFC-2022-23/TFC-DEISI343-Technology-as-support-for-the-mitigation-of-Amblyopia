using UnityEngine;

public class SplashController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey){
            Globals.NextLevel();
        }
        
    }
}
