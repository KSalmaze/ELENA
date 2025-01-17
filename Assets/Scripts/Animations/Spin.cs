using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed;
    
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * -1 * Time.deltaTime);
    }
}
