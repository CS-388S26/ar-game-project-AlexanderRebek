using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using Vuforia;

public class ImageTargetCourseSpawner : MonoBehaviour
{
    public GameObject golfCoursePrefab; // Prefab containing all courses as children
    private GameObject currentCourse;

    private ObserverBehaviour observerBehaviour;
    private bool hasSpawned = false;

    public LevelManager levelManager; // assign in inspector

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        else
        {
            UnityEngine.Debug.LogError("ObserverBehaviour not found!");
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (!hasSpawned &&
            (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED))
        {
            SpawnCourse();
            hasSpawned = true;
        }
    }

    private void SpawnCourse()
    {
        if (golfCoursePrefab == null)
        {
            UnityEngine.Debug.LogError("Golf course prefab not assigned!");
            return;
        }

        // Spawn course as child of image target
        currentCourse = Instantiate(golfCoursePrefab, transform);
        currentCourse.transform.localPosition = new Vector3(0f, -0.2f, 0f);
        currentCourse.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        currentCourse.transform.localRotation = Quaternion.identity;

        // --- Integrate LevelManager ---
        if (levelManager != null)
        {
            levelManager.SetCompleteCourse(currentCourse.transform);
        }
    }
}
