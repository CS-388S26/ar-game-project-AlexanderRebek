using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject ballInstance;

    void Start()
    {
        if (ballPrefab != null && ballInstance == null)
        {
            ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        }
    }
}
