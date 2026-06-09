using UnityEngine;

public class CelestialLabel : MonoBehaviour
{
    public GameObject canvaObject;

    public void SetVisible(bool visible) {
        canvaObject.SetActive(visible);
    }

    private void LateUpdate()
    {
        if (canvaObject.activeSelf)
        {
            canvaObject.transform.LookAt(Camera.main.transform);
            canvaObject.transform.Rotate(0, 180, 0);
        }
    }
}
