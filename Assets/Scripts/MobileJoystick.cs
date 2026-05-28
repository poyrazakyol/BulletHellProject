using UnityEngine;
using UnityEngine.EventSystems; // Dokunmatik kontroller için zorunlu

// IPointer arayüzleri, objenin ekrandaki dokunmaları algılamasını sağlar
public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickKnob;
    
    private Vector2 inputVector;
    
    // YENİ: Çoklu dokunma çakışmalarını önlemek için parmak kimliği
    private int currentFingerId = -1; 

    void Start()
    {
        // Oyun başlarken joystick'i gizle
        HideJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Eğer zaten bir parmak joystick'i kullanıyorsa, diğer dokunuşları yoksay
        if (currentFingerId != -1) return; 
        
        // Dokunan parmağın kimliğini kaydet
        currentFingerId = eventData.pointerId; 

        // 1. Ekrana dokunulduğunda joystick'i parmağın olduğu yere taşı ve görünür yap
        joystickBackground.position = eventData.position;
        joystickBackground.gameObject.SetActive(true);
        
        // İlk dokunuşta hareketi de başlatmak için Drag'i çağır
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Eğer sürükleyen parmak, ilk dokunan parmak değilse yoksay
        if (eventData.pointerId != currentFingerId) return; 

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
        // Kalkan parmak, bizim joystick'i tutan parmak değilse hiçbir şey yapma
        if (eventData.pointerId != currentFingerId) return; 

        // 3. Parmak ekrandan çekildiğinde değerleri sıfırla ve joystick'i gizle
        currentFingerId = -1; // Parmağı boşa çıkar
        inputVector = Vector2.zero;
        joystickKnob.anchoredPosition = Vector2.zero;
        HideJoystick();
    }

    void HideJoystick()
    {
        joystickBackground.gameObject.SetActive(false);
    }
    
    // Bu obje kodla veya menü açılışıyla kapatıldığında otomatik çalışır
    void OnDisable()
    {
        currentFingerId = -1; // Kapanırken parmak kilidini de sıfırla
        inputVector = Vector2.zero; // Hareketi sıfırla
        if (joystickKnob != null) 
        {
            joystickKnob.anchoredPosition = Vector2.zero; // Topuzu merkeze al
        }
        HideJoystick(); // Görseli gizle
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