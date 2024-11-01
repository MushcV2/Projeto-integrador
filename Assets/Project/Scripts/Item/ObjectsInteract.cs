using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectsInteract : MonoBehaviour
{
    [Header("Item Variables")]
    [SerializeField] protected AudioSource audioS;
    [SerializeField] protected TaskManager task;
    [SerializeField] protected GameObject popUpUsable;
    [SerializeField] protected Collider objectCollider;
    [SerializeField] protected PlayerInteract playerInteract;
    [SerializeField] protected PlayerController player;
    [SerializeField] protected Image crossHair;
    [SerializeField] protected GameObject sanityBar;
    [SerializeField] protected GameObject taskPanel;
    [SerializeField] protected GameObject clockPanel;
    [SerializeField] protected Camera cam;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private Transform itemPos;
    [SerializeField] private Transform oldParent;
    [SerializeField] protected int itemIndex;
    [SerializeField] protected bool usable;
    [SerializeField] protected bool canOpen;
    public int dayToDestroy;

    protected void Awake()
    {
        playerInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        task = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager>();
        itemPos = GameObject.FindGameObjectWithTag("ItemPos").GetComponent<Transform>();
        //popUpUsable = FindAnyObjectByType<Canvas>().transform.Find("UsableTXT").GetComponent<TextMeshProUGUI>();
    }

    protected virtual void Start()
    {
        objectCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        if (usable) popUpUsable.SetActive(false);
    }

    public virtual void InteractFunction()
    {
        if (usable && popUpUsable != null)
            popUpUsable.SetActive(true);

        rb.isKinematic = true;
        objectCollider.isTrigger = true;
        rb.useGravity = false;

        transform.parent = itemPos;
        transform.position = itemPos.position;
        transform.localEulerAngles = Vector3.zero;

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public virtual void StopInteract()
    {
        if (usable && popUpUsable != null)
            popUpUsable.SetActive(false);

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

            if (usable && popUpUsable != null)
                popUpUsable.SetActive(false);
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