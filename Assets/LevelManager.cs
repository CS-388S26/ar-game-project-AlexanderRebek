using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform completeCourse;

    private int currentLevel = 0;
    private int totalLevels = 0;
    private bool initialized = false;

    void Update()
    {
        if (!initialized && completeCourse != null)
        {
            InitializeLevels();
        }
    }

    // Called when the tracker/floor is detected
    public void SetCompleteCourse(Transform courseParent)
    {
        completeCourse = courseParent;
        InitializeLevels();
    }

    private void InitializeLevels()
    {
        if (completeCourse == null) return;

        totalLevels = completeCourse.childCount;

        for (int i = 0; i < totalLevels; i++)
        {
            completeCourse.GetChild(i).gameObject.SetActive(i == currentLevel);
        }

        initialized = true;
    }

    public void NextLevel()
    {
        if (!initialized) return;

        // Deactivate current
        if (currentLevel < totalLevels)
            completeCourse.GetChild(currentLevel).gameObject.SetActive(false);

        currentLevel++;

        if (currentLevel < totalLevels)
            completeCourse.GetChild(currentLevel).gameObject.SetActive(true);
        else
            UnityEngine.Debug.Log("All levels completed!");
    }
}
