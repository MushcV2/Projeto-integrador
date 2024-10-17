using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Variables")]
    [SerializeField] private Transform character;
    [SerializeField] private Transform characterHead;
    [SerializeField] private float sensibility;
    public bool stopFollowing;
    private float rotX;
    private float rotY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stopFollowing = false;
    }

    private void Update()
    {
        if (stopFollowing)
        {
            //transform.localEulerAngles = Vector3.zero;
            return;
        }

        float _x = Input.GetAxisRaw("Mouse X") * sensibility;
        float _y = Input.GetAxisRaw("Mouse Y") * sensibility;

        rotX += _x;
        rotY += _y;

        rotY = Mathf.Clamp(rotY, -90, 90);

        character.localEulerAngles = new Vector3(0f, rotX, 0f);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);
    }

    private void LateUpdate()
    {
        transform.position = characterHead.position;
    }
}
