using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SanityController
{
    public static PlayerController instance;

    [Header("Movement Variables")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform headPos;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private bool isCrouching;
    public bool walking;
    public bool canMove;
    public bool isSitting;
    private Vector3 strafe;
    private Vector3 forward;
    private Vector3 vertical;
    private Vector3 finalVelocity;

    private void Awake()
    {
        instance = this;

        controller = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();

        currentSpeed = walkSpeed;
        canMove = true;
    }

    protected override void Update()
    {
        base.Update();

        if (strafe.x != 0 || strafe.z != 0 || forward.x != 0 || forward.z != 0)
            walking = true;

        else
            walking = false;

        if (!canMove) return;

        PlayerInputs();
    }

    private void PlayerInputs()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");

        strafe = _x * currentSpeed * transform.right;
        forward = _y * currentSpeed * transform.forward;
        vertical = new Vector3(0f, -gravity, 0f);


        if (Input.GetKeyDown(KeyCode.C) && !isCrouching)
        {
            isCrouching = true;

            transform.localScale = new Vector3(transform.localScale.x, 0.7f, transform.localScale.z);
            currentSpeed = crouchSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CanUp();
            if (!CanUp()) return;

            isCrouching = false;

            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            currentSpeed = walkSpeed;
        }

        finalVelocity = strafe + forward + vertical;
        controller.Move(finalVelocity * Time.deltaTime);
    }

    private bool CanUp()
    {
        Ray _ray = new Ray(headPos.position, Vector3.up);

        if (Physics.Raycast(_ray, 100f, obstacleLayer))
            return false;
        else
            return true;
    }
}