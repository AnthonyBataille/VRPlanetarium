using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartExploring()
    {
        Debug.Log("Loading SolarSystem scene");
        SceneManager.LoadScene("SolarSystemScene");
    }

    public void QuitApplication()
    {
        Debug.Log("Quit");

        Application.Quit();
    }
}