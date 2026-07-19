using UnityEngine;
using UnityEngine.InputSystem;

public class LabelManager : MonoBehaviour
{
    public InputActionReference toggleLabelsAction;
    private CelestialLabel[] labels;
    private bool labelvisible = false;

    void Start()
    {
        labels = FindObjectsByType<CelestialLabel>(FindObjectsSortMode.None);
    }

    public void ToggleLabels()
    {
        labelvisible = !labelvisible;

        foreach (var label in labels)
        {
            label.SetVisible(labelvisible);
        }
    }

    void OnEnable()
    {
        toggleLabelsAction.action.performed += OnToggle;
    }

    void OnDisable()
    {
        toggleLabelsAction.action.performed -= OnToggle;
    }

    private void OnToggle(InputAction.CallbackContext ctx)
    {
        ToggleLabels();
    }
}
