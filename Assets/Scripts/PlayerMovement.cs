using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float acceleration = 15f; 
    public float rotationSpeed = 15f;
    
    [Header("Mobil Kontroller")]
    public MobileJoystick joystick;

    [Header("Animasyon")]
    public Animator anim; // YENİ: Gorilin Animator referansı

    private Rigidbody rb;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        
        if (joystick != null && (joystick.Horizontal() != 0 || joystick.Vertical() != 0))
        {
            moveX = joystick.Horizontal();
            moveZ = joystick.Vertical();
        }
        
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        // --- YENİ: ANİMASYONU TETİKLEME ---
        if (anim != null)
        {
            // moveInput.magnitude değeri karakter duruyorsa 0, hareket ediyorsa 0'dan büyük bir değer (maksimum 1) verir.
            // Bu değeri Animator'deki Speed parametresine gönderiyoruz.
            anim.SetFloat("Speed", moveInput.magnitude);
        }
        // ----------------------------------
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = moveInput * moveSpeed;
        
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, rb.linearVelocity.z);

        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}