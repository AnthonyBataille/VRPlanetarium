using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    private Vector3 rotationAxis = Vector3.down;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * rotationAxis);
    }
}
