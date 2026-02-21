using UnityEngine;

public class OrbitMotor : MonoBehaviour
{
    public float rotationSpeed = 200f; 
    private float currentAngle = 0f; 

    void Update()
    {
        
        currentAngle += rotationSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}