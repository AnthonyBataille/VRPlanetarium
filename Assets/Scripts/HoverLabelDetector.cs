using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class HoverLabelDetector : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    private GameObject currentTarget;

    // Update is called once per frame
    void Update()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj != currentTarget)
            {
                currentTarget = hitObj;
                ShowLabel(hitObj);
            }
        }
        else
        {
            if (currentTarget != null)
            {
                HideLabel(currentTarget);
                currentTarget = null;
            }
        }
    }

    void ShowLabel(GameObject obj)
    {
        var body = obj.GetComponent<CelestialBody>();

        if (body != null)
        {
            UIManager.Instance.ShowLabel(body.displayName, obj);
        }
    }

    void HideLabel(GameObject obj)
    {
        var body = obj.GetComponent<CelestialBody>();

        if (body != null)
        {
            UIManager.Instance.HideLabel();
        }
    }
}

