using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class WashObject : MonoBehaviour
{
    private ScoreCounting scoreCounting;
    private GameManager gameManager;
    private Rigidbody rb;

    [SerializeField] private WashingTask washingTask;
    [SerializeField] private Image feedbackSlider;
    [SerializeField] private AudioSource completedSound;
    [SerializeField] private float washProgression;
    [SerializeField] private float offset;
    public bool touchOnObject;
    private bool firstTime;
    private bool canWash;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scoreCounting =GameObject.FindGameObjectWithTag("ScoreCounting").GetComponent<ScoreCounting>();
        rb = GetComponent<Rigidbody>();

       // gameObject.layer = LayerMask.NameToLayer("Default");

        feedbackSlider.fillAmount = 0;
        feedbackSlider.gameObject.SetActive(false);
        firstTime = true;
    }

    private void Update()
    {
       // if (washingTask.isWashing && gameObject.layer != LayerMask.NameToLayer("Interact")) gameObject.layer = LayerMask.NameToLayer("Interact");
       // else gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnMouseDrag()
    {
        if (touchOnObject && !firstTime && !washingTask.isWashing) return;

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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Water") && washProgression < 100)
        {
            canWash = true;

            StartCoroutine(WashProgression());
            feedbackSlider.gameObject.SetActive(true);

            Debug.Log("Esta na agua");
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Water")) canWash = false;
    }

    private IEnumerator WashProgression()
    {
        if (!canWash) yield break;

        washProgression++;
        feedbackSlider.fillAmount = washProgression / 100;

        if (washProgression >= 100)
        {
            completedSound.Play();
            feedbackSlider.gameObject.SetActive(false);

            washingTask.Washed();
            scoreCounting.taskScore += 15;

            Debug.Log("Finalizado");
            yield break;
        }

        yield return new WaitForSeconds(0.04f);
        StartCoroutine(WashProgression());
    }
}