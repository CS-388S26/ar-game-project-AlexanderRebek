using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SingleCoursePlacer : MonoBehaviour
{
    public GameObject golfCoursePrefab; // assign your golf course prefab
    private GameObject currentCourse;

    private PlaneFinderBehaviour planeFinder;

    void Start()
    {
        planeFinder = GetComponent<PlaneFinderBehaviour>();
        if (planeFinder != null)
        {
            // Subscribe correctly using a delegate
            planeFinder.OnInteractiveHitTest.AddListener(OnPlaneHit);
        }
    }

    private void OnPlaneHit(HitTestResult result)
    {
        // Only place one course
        if (currentCourse == null && golfCoursePrefab != null)
        {
            currentCourse = Instantiate(golfCoursePrefab, result.Position, result.Rotation);

            // Disable Plane Finder so no more courses can be placed
            planeFinder.enabled = false;
            planeFinder.gameObject.SetActive(false);
        }
    }
}
