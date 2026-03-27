using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Vuforia;

public class SingleCoursePlacer : MonoBehaviour
{
    public GameObject coursePrefab; // prefab containing all courses as children
    private GameObject currentCourse;

    public LevelManager levelManager;

    private PlaneFinderBehaviour planeFinder;

    private bool hasSpawned = false;

    void Start()
    {
        planeFinder = GetComponent<PlaneFinderBehaviour>();
        if (planeFinder != null)
        {
            planeFinder.OnInteractiveHitTest.AddListener(OnPlaneHit);
        }
        else
        {
            UnityEngine.Debug.LogError("PlaneFinderBehaviour not found!");
        }
    }

    // when floor is detected and pressed
    private void OnPlaneHit(HitTestResult result)
    {
        if (hasSpawned) return;

        currentCourse = Instantiate(coursePrefab, result.Position, result.Rotation);

        if (levelManager != null)
        {
            levelManager.SetCompleteCourse(currentCourse.transform);
        }

        // disable planefinder
        if (planeFinder != null)
        {
            planeFinder.enabled = false;
            planeFinder.gameObject.SetActive(false);
        }

        hasSpawned = true;
    }
}
