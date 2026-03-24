using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDragging = false;
    private bool canDrag = true;
    private Vector3 dragStartPos;

    public float forceMultiplier = 10f; // increase if ball is too slow
    public float minSpeed = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // start stopped
    }

    void Update()
    {
        if (!canDrag) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z)
            );

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Vector3.Distance(touchWorld, transform.position) < 0.5f) // hit ball
                    {
                        isDragging = true;
                        dragStartPos = transform.position;
                        rb.isKinematic = true;
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        Vector3 force = dragStartPos - touchWorld;
                        force.y = 0;
                        rb.isKinematic = false;
                        rb.AddForce(force * forceMultiplier, ForceMode.Impulse);
                        isDragging = false;
                        canDrag = false;
                    }
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canDrag && !rb.isKinematic)
        {
            if (rb.velocity.magnitude < minSpeed)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                canDrag = true;
            }
        }
    }
}
