using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SetAmblyopicEye : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] int buttonPressed;
    [SerializeField] bool AdditivelyLodad = false;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // set global definition of amblyopic eye
        Globals.amblyopicEye = buttonPressed;

        if (AdditivelyLodad) {
            SceneManager.UnloadSceneAsync("01 - SelectAmbEye - additive");
            SceneManager.LoadScene("01 - Menu");
        }
        else Globals.NextLevel();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
