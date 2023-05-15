using UnityEngine;
using UnityEngine.EventSystems;

public class SetAmblyopicEye : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] int buttonPressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // set global definition of amblyopic eye
        Globals.amblyopicEye = buttonPressed;
        Globals.NextLevel();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
