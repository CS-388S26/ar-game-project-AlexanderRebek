using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab;        // The ball prefab
    public Transform arCamera;           // Your ARCamera transform
    public float spawnOffsetY = -0.1f;   // Slightly below camera
    public float spawnOffsetZ = 0.5f;    // Slightly in front of camera
    public float launchSpeed = 10f;      // Forward speed
    public float upwardForce = 5f;       // Upward arc
    public LevelManager levelManager;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        if (arCamera == null)
        {
            UnityEngine.Debug.LogWarning("ARCamera not assigned. Defaulting to main camera.");
            arCamera = mainCam.transform;
        }
    }

    void Update()
    {
        // Touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            LaunchBall(Input.GetTouch(0).position);
        }
        // Mouse input
        else if (Input.GetMouseButtonDown(0))
        {
            LaunchBall(Input.mousePosition);
        }
    }

    private void LaunchBall(Vector2 screenPos)
    {
        // 1. Spawn ball slightly in front of camera
        Vector3 spawnPos = arCamera.position + arCamera.forward * spawnOffsetZ + arCamera.up * spawnOffsetY;
        GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        ball.GetComponent<BallLogic>().levelManager = levelManager;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb == null)
        {
            UnityEngine.Debug.LogWarning("Ball prefab needs a Rigidbody!");
            return;
        }

        // 2. Determine direction from camera to a point in front of camera based on tap
        Ray ray = mainCam.ScreenPointToRay(screenPos);

        // Project the ray forward at a reasonable distance (1–5 meters)
        float targetDistance = 2f; // you can tweak this
        Vector3 targetPoint = ray.GetPoint(targetDistance);

        // 3. Compute direction vector and apply upward force
        Vector3 direction = (targetPoint - spawnPos).normalized;
        Vector3 launchVelocity = direction * launchSpeed + Vector3.up * upwardForce;

        // 4. Apply velocity
        rb.velocity = launchVelocity;
    }
}
