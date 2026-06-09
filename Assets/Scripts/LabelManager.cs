using UnityEngine;

public class LabelManager : MonoBehaviour
{
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
}
