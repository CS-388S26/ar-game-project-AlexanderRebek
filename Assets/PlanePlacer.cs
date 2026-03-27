using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Vuforia;

public class SingleCoursePlacer : MonoBehaviour
{
    public GameObject golfCoursePrefab;
    private GameObject currentCourse;

    public GameObject ballPrefab;
    private GameObject currentBall;

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
        // Prevent placing multiple courses
        if (currentCourse != null) return;

        // Instantiate the golf course at detected plane
        currentCourse = Instantiate(golfCoursePrefab, result.Position, result.Rotation);

        // Try to find the BallStartPoint inside the spawned course
        Transform ballStart = currentCourse.transform.Find("BallStartPoint");

        if (ballStart != null)
        {
            if (ballPrefab != null)
            {
                currentBall = Instantiate(ballPrefab, ballStart.position, Quaternion.identity);
            }
        }

        // Disable Plane Finder so no more placements happen
        if (planeFinder != null)
        {
            planeFinder.enabled = false;
            planeFinder.gameObject.SetActive(false);
        }
    }
}
