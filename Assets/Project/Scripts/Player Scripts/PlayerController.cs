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
    public bool isCrouching;
    public bool walking;
    public bool canMove;
    public bool isSitting;
    public Vector3 finalVelocity;
    private Vector3 strafe;
    private Vector3 forward;
    private Vector3 vertical;

    [Header("UI")]
    [SerializeField] private UseComputer useComputer;
    [SerializeField] private WashingTask washingTask;
    [SerializeField] private bool canOpenClock;
    public GameObject clockUI;

    private void Awake()
    {
        instance = this;

        controller = GetComponent<CharacterController>();
    }

    protected override void Start()
    {
        base.Start();

        clockUI.SetActive(false);
        currentSpeed = walkSpeed;
        canMove = true;
        canOpenClock = true;
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

        if (Input.GetKeyDown(KeyCode.C)) Crounch();

        if (Input.GetKeyDown(KeyCode.H) && canOpenClock && !GetComponent<PlayerInteract>().alreadyInteract && !useComputer.onPc && !washingTask.isWashing)
        {
            canOpenClock = false;
            EnableClock();
        }

        finalVelocity = strafe + forward + vertical;
        controller.Move(finalVelocity * Time.deltaTime);
    }

    public void Crounch()
    {
        if (!isCrouching)
        {
            isCrouching = true;

            transform.localScale = new Vector3(transform.localScale.x, 0.7f, transform.localScale.z);
            currentSpeed = crouchSpeed;
        }
        else
        {
            CanUp();
            if (!CanUp()) return;

            isCrouching = false;

            transform.localScale = new Vector3(transform.localScale.x, 0.96f, transform.localScale.z);
            currentSpeed = walkSpeed;
        }
    }

    private bool CanUp()
    {
        Ray _ray = new Ray(headPos.position, Vector3.up);

        if (Physics.Raycast(_ray, 100f, obstacleLayer))
            return false;
        else
            return true;
    }

    private void EnableClock()
    {
        if (!clockUI.activeSelf)
        {
            clockUI.SetActive(true);
            clockUI.GetComponent<Animator>().SetTrigger("Open");
        }
        else clockUI.GetComponent<Animator>().SetTrigger("Close");

        Invoke(nameof(CloseClock), 3f);
        Invoke(nameof(CanOpenClock), 0.4f);
    }

    private void CloseClock()
    {
        clockUI.GetComponent<Animator>().SetTrigger("Close");
    }

    private void CanOpenClock()
    {
        canOpenClock = true;
    }
}