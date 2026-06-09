using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public InputActionReference menuButtonAction;

    private Rotation[] rotations;
    private bool isPaused = false;

    private void Start()
    {
        rotations = FindObjectsByType<Rotation>(FindObjectsSortMode.None);
    }

    private void OnEnable()
    {
        menuButtonAction.action.performed += TogglePause;
    }

    private void OnDisable()
    {
        menuButtonAction.action.performed -= TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        isPaused = !isPaused;

        foreach (var rot in rotations)
        {
            rot.enabled = !isPaused;
        }
    }
}