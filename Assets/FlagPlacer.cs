using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FlagPlacer : MonoBehaviour
{
    public GameObject flagPrefab; // Assign your flag prefab in the inspector
    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            PlaceFlag();
        }
    }

    private void PlaceFlag()
    {
        // Only place if it hasn't been placed yet
        if (flagPrefab != null && GameObject.FindWithTag("Flag") == null)
        {
            Vector3 spawnPosition = transform.position; // default to tracker position

            // Raycast down to see if there's a golf course/terrain below
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, out hit, 100f))
            {
                spawnPosition = hit.point + new Vector3(0, 0.5f, 0); // small offset above terrain
            }
            else
            {
                // No terrain detected, flag stays on top of tracker
                spawnPosition += new Vector3(0, 0.5f, 0); // small offset above tracker
            }

            GameObject flag = Instantiate(flagPrefab, spawnPosition, Quaternion.identity);
            flag.tag = "Flag";
        }
    }
}
