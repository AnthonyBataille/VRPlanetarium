using UnityEngine;
using UnityEngine.InputSystem;

public class LabelInput : MonoBehaviour
{
    public InputActionReference toggleLabelsAction;
    public LabelManager labelManager;

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
        labelManager.ToggleLabels();
    }
}