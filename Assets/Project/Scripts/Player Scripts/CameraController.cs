using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Variables")]
    [SerializeField] private Transform character;
    [SerializeField] private Transform characterHead;
    [SerializeField] private float sensibility;
    private Animator anim;
    public bool stopFollowing;
    private float rotX;
    private float rotY;

    [Header("Camera Effect")]
    [SerializeField] private float magnitude;
    [SerializeField] private float frequency;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stopFollowing = false;
    }

    private void Update()
    {
        if (stopFollowing) return;

        float _x = Input.GetAxisRaw("Mouse X") * sensibility;
        float _y = Input.GetAxisRaw("Mouse Y") * sensibility;

        rotX += _x;
        rotY += _y;

        rotY = Mathf.Clamp(rotY, -90, 90);

        character.localEulerAngles = new Vector3(0f, rotX, 0f);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);

        if (character.gameObject.GetComponent<PlayerController>().finalVelocity.x != 0 || character.gameObject.GetComponent<PlayerController>().finalVelocity.z != 0) characterHead.position += HeadEffect();
    }

    private void LateUpdate()
    {
        transform.position = characterHead.position;
    }

    private Vector3 HeadEffect()
    {
        Vector3 _pos = Vector3.zero;

        _pos.y += Mathf.Sin(Time.time * frequency) * magnitude;
        _pos.x += Mathf.Cos(Time.time * frequency / 2) * magnitude * 2;
        return _pos;
    }
}
