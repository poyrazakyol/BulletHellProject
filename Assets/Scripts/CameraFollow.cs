using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float height = 15f;

    [Header("Camera Shake Settings")]
    public float shakeDuration = 0f; 
    public float shakeMagnitude = 0.3f; 
    public float dampingSpeed = 1.0f; 

    Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        if (target == null) return;

        
        Vector3 targetPos = new Vector3(target.position.x, height, target.position.z);

        
        if (Time.timeScale == 0f)
        {
            transform.position = targetPos;
            return; 
        }

        
        if (shakeDuration > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            shakeOffset.y = 0; 

            transform.position = targetPos + shakeOffset;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = targetPos;
        }
    }

    
    public void TriggerShake(float duration)
    {
        shakeDuration = duration;
    }
}