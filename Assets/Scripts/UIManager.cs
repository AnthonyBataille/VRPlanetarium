using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject canva;
    private GameObject currentObject;
    private float labelHeightOffset;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowLabel(string text, GameObject obj)
    {
        currentObject = obj;

        Collider col = currentObject.GetComponent<Collider>();
        labelHeightOffset = col.bounds.extents.y * 1.2f;
        float scale = labelHeightOffset * 0.5f;

        GameObject labelObject = canva.transform.GetChild(0).gameObject;
        labelObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        labelObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().transform.localScale = scale * Vector3.one;

        canva.transform.position = currentObject.transform.position + labelHeightOffset * Vector3.up;

        canva.SetActive(true);
    }

    public void HideLabel()
    {
        currentObject = null;
        canva.SetActive(false);
    }

    private void LateUpdate()
    {
        if (canva.activeSelf)
        {
            canva.transform.position = currentObject.transform.position + labelHeightOffset * Vector3.up;
            canva.transform.LookAt(Camera.main.transform);
            canva.transform.Rotate(0, 180, 0);
        }
    }
}
