using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject pauseMenuCanvas;

    [Header("XR")]
    [SerializeField] private Transform xrCamera;
    [SerializeField] private InputActionReference pauseAction;

    [Header("Locomotion")]
    [SerializeField] private Behaviour continuousMoveProvider;
    [SerializeField] private Behaviour continuousTurnProvider;

    [Header("User input")]
    [SerializeField] private LabelManager labelManager;
    [SerializeField] private AnimationManager animationManager;

    [Header("Menu Placement")]
    [SerializeField] private float distanceFromCamera = 1.0f;
    [SerializeField] private float verticalOffset = 0.0f;

    [Header("Scene Navigation")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused;

    private void Awake()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        isPaused = false;
    }

    private void OnEnable()
    {
        if (pauseAction != null)
        {
            pauseAction.action.performed += OnPauseActionPerformed;
            pauseAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (pauseAction != null)
        {
            pauseAction.action.performed -= OnPauseActionPerformed;
            pauseAction.action.Disable();
        }
    }

    private void OnPauseActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Pause button pressed");
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        if (animationManager != null)
        {
            animationManager.SetPaused(true);
        }
        SetLocomotionEnabled(false);
        SetUserInputEnabled(false);
        PositionMenuInFrontOfCamera();
        pauseMenuCanvas.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);
        SetLocomotionEnabled(true);
        SetUserInputEnabled(true);
        if (animationManager != null)
        {
            animationManager.SetPaused(false);
        }
    }

    public void BackToMainMenu()
    {
        // Ensure no paused state leaks into the next scene.
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void SetLocomotionEnabled(bool enabled)
    {
        if (continuousMoveProvider != null)
        {
            continuousMoveProvider.enabled = enabled;
        }

        if (continuousTurnProvider != null)
        {
            continuousTurnProvider.enabled = enabled;
        }
    }

    private void SetUserInputEnabled(bool enabled)
    {
        if (labelManager != null)
        {
            labelManager.enabled = enabled;
        }

        if (animationManager != null)
        {
            animationManager.enabled = enabled;
        }
    }

    private void PositionMenuInFrontOfCamera()
    {
        if (xrCamera == null || pauseMenuCanvas == null)
        {
            return;
        }

        Vector3 horizontalForward = xrCamera.forward;
        horizontalForward.y = 0.0f;

        if (horizontalForward.sqrMagnitude < 0.001f)
        {
            horizontalForward = xrCamera.forward;
        }

        horizontalForward.Normalize();

        Vector3 menuPosition =
            xrCamera.position +
            horizontalForward * distanceFromCamera +
            Vector3.up * verticalOffset;

        pauseMenuCanvas.transform.position = menuPosition;

        Vector3 directionFromCamera =
            pauseMenuCanvas.transform.position - xrCamera.position;

        pauseMenuCanvas.transform.rotation =
            Quaternion.LookRotation(directionFromCamera.normalized, Vector3.up);
    }
}