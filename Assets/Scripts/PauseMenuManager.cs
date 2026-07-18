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


    [Header("Menu Placement")]
    [SerializeField] private float distanceFromCamera = 1.5f;
    [SerializeField] private float verticalOffset = 0.0f;

    [Header("Scene Navigation")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Planet Animation")]
    [SerializeField] private AnimationManager animationManager;

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
        SetLocomotionEnabled(false);
        PositionMenuInFrontOfCamera();
        pauseMenuCanvas.SetActive(true);

        if (animationManager != null)
        {
            animationManager.SetPaused(true);
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);

        if (animationManager != null)
        {
            animationManager.SetPaused(false);
        }
        SetLocomotionEnabled(true);
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