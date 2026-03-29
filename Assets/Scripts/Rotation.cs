using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    private Vector3 rotatonAxis = Vector3.down;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.down);
    }
}
