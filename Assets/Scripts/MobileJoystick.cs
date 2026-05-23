using UnityEngine;
using UnityEngine.EventSystems; 


public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickKnob;
    
    private Vector2 inputVector;
    
    
    private int currentFingerId = -1; 

    void Start()
    {
        
        HideJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (currentFingerId != -1) return; 
        
        
        currentFingerId = eventData.pointerId; 

        
        joystickBackground.position = eventData.position;
        joystickBackground.gameObject.SetActive(true);
        
        
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (eventData.pointerId != currentFingerId) return; 

        
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

       
        position.x = (position.x / joystickBackground.sizeDelta.x) * 2;
        position.y = (position.y / joystickBackground.sizeDelta.y) * 2;

        inputVector = new Vector2(position.x, position.y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        
        joystickKnob.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.sizeDelta.x / 2), inputVector.y * (joystickBackground.sizeDelta.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        if (eventData.pointerId != currentFingerId) return; 

        
        currentFingerId = -1; 
        inputVector = Vector2.zero;
        joystickKnob.anchoredPosition = Vector2.zero;
        HideJoystick();
    }

    void HideJoystick()
    {
        joystickBackground.gameObject.SetActive(false);
    }
    
    
    void OnDisable()
    {
        currentFingerId = -1; 
        inputVector = Vector2.zero; 
        if (joystickKnob != null) 
        {
            joystickKnob.anchoredPosition = Vector2.zero; 
        }
        HideJoystick(); 
    }

    
    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}