using UnityEngine;
using UnityEngine.EventSystems; // Dokunmatik kontroller için zorunlu

// IPointer arayüzleri, objenin ekrandaki dokunmaları algılamasını sağlar
public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickKnob;
    
    private Vector2 inputVector;
    private Vector2 startingPosition; // Joystick'in ilk saklandığı yer

    void Start()
    {
        // Oyun başlarken joystick'i gizle
        HideJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 1. Ekrana dokunulduğunda joystick'i parmağın olduğu yere taşı ve görünür yap
        joystickBackground.position = eventData.position;
        joystickBackground.gameObject.SetActive(true);
        
        // İlk dokunuşta hareketi de başlatmak için Drag'i çağır
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 2. Parmağı kaydırdıkça topuzu (Knob) arka planın içinde hareket ettir
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out position);

        // Normalize edilmiş yön vektörünü hesapla (-1 ile 1 arası değerler)
        position.x = (position.x / joystickBackground.sizeDelta.x) * 2;
        position.y = (position.y / joystickBackground.sizeDelta.y) * 2;

        inputVector = new Vector2(position.x, position.y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        // Topuzu fiziksel olarak sınırların içinde hareket ettir
        joystickKnob.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.sizeDelta.x / 2), inputVector.y * (joystickBackground.sizeDelta.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 3. Parmak ekrandan çekildiğinde değerleri sıfırla ve joystick'i gizle
        inputVector = Vector2.zero;
        joystickKnob.anchoredPosition = Vector2.zero;
        HideJoystick();
    }

    void HideJoystick()
    {
        joystickBackground.gameObject.SetActive(false);
    }

    // Karakter hareket scriptinin (PlayerMovement) buradaki değerleri okuyabilmesi için:
    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}