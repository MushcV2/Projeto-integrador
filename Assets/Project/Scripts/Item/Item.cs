using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Item Variables")]
    [SerializeField] protected Tasks task;
    [SerializeField] protected TMP_Text usableItem;
    [SerializeField] protected Collider objectCollider;
    [SerializeField] protected PlayerInteract playerInteract;
    [SerializeField] protected PlayerController player;
    [SerializeField] protected Image crossHair;
    [SerializeField] protected Camera cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform itemPos;
    [SerializeField] private Transform oldParent;
    [SerializeField] protected int itemIndex;
    [SerializeField] protected bool usable;
    [SerializeField] protected bool canOpen;

    protected void Awake()
    {
        playerInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        task = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<Tasks>();
    }

    protected virtual void Start()
    {
        objectCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        if (usable) usableItem.gameObject.SetActive(false);
    }

    public virtual void InteractFunction()
    {
        if (usable && usableItem != null)
            usableItem.gameObject.SetActive(true);

        rb.isKinematic = true;
        rb.useGravity = false;
        objectCollider.isTrigger = true;

        transform.parent = itemPos;
        transform.position = itemPos.position;
        transform.localEulerAngles = Vector3.zero;

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public virtual void StopInteract()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        objectCollider.isTrigger = false;

        transform.parent = oldParent;

        if (!usable)
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);

        gameObject.layer = LayerMask.NameToLayer("Interact");
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag != "Player" && !canOpen)
        {
            StopInteract();
            playerInteract.alreadyInteract = false;
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.tag == "CompletedZone" && !usable)
        {
            task.MissionCompleted(itemIndex);
            Debug.Log("Cubo acertado");
        }
    }
}