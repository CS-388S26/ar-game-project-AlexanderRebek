using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    float destroyDelay = 2f;
    public LevelManager levelManager;

    //to despawn balls when out of view frustum
    private void OnBecameInvisible()
    {
        Destroy(gameObject, destroyDelay);
    }

    //to detect win
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            if (levelManager != null)
            {
                levelManager.NextLevel();
            }

            Destroy(gameObject);
        }
    }
}
