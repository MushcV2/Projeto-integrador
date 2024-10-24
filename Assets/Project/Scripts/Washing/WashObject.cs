using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WashObject : MonoBehaviour
{
    private ScoreCounting scoreCounting;
    private GameManager gameManager;
    private Rigidbody rb;

    [SerializeField] private WashingTask washingTask; 
    [SerializeField] private AudioSource completedSound;
    [SerializeField] private float washProgression;
    [SerializeField] private float offset;
    public bool touchOnObject;
    private bool firstTime;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scoreCounting =GameObject.FindGameObjectWithTag("ScoreCounting").GetComponent<ScoreCounting>();
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

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Water"))
        {
            StartCoroutine(WashProgression());
            Debug.Log("Esta na agua");
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Water")) StopCoroutine(WashProgression());
    }

    private IEnumerator WashProgression()
    {
        washProgression++;

        if (washProgression == 100)
        {
            completedSound.Play();

            scoreCounting.taskScore += 50;

            Debug.Log("Finalizado");
            yield break;
        }

        yield return new WaitForSeconds(0.04f);
        StartCoroutine(WashProgression());
    }
}