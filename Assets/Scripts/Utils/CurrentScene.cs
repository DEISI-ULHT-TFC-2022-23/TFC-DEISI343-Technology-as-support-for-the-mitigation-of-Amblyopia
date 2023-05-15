using UnityEngine;
using UnityEngine.SceneManagement;


public class CurrentScene : MonoBehaviour
{
    TMPro.TextMeshProUGUI txt;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "Scene: " + SceneManager.GetActiveScene().name + " || " + SceneManager.GetActiveScene().buildIndex;
    }
}
