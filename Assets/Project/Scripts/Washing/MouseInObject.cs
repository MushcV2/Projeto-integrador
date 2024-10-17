using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInObject : MonoBehaviour
{
    [SerializeField] private float offset;
    public bool touchOnObject;
    private bool firstTime;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        firstTime = true;
    }

    private void OnMouseDrag()
    {
        if (touchOnObject && !firstTime) return;

        rb.useGravity = false;
        firstTime = false;

        Vector3 _mousePos = Input.mousePosition;
        _mousePos.z = offset;

        transform.position = Camera.main.ScreenToWorldPoint(_mousePos);
    }

    private void OnMouseDown()
    {
        touchOnObject = false;
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision _other)
    {
        touchOnObject = true;
    }

    private void OnCollisionExit(Collision _other)
    {
        touchOnObject = false;
    }
}