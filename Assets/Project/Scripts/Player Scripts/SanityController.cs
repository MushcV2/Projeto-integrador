using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityController : MonoBehaviour
{
    [Header("Sanity Variables")]
    [SerializeField] private float maxSanity;
    public float currentSanity;

    [Header("Sanity UI")]
    [SerializeField] private Slider sanityBar;

    protected virtual void Start()
    {
        currentSanity = maxSanity;
        sanityBar.maxValue = maxSanity;

        sanityBar.gameObject.SetActive(false);
    }
    
    protected virtual void Update()
    {
        sanityBar.value = Mathf.Lerp(sanityBar.value, currentSanity, 0.6f);
    }

    public void GainSanity(float _sanity)
    {
        sanityBar.gameObject.SetActive(true);
        Invoke(nameof(DisableBar), 3f);

        currentSanity += Mathf.Min(currentSanity + _sanity, maxSanity);
    }

    public void LostSanity(float _sanity)
    {
        sanityBar.gameObject.SetActive(true);
        Invoke(nameof(DisableBar), 3f);

        currentSanity -= Mathf.Max(_sanity, 0);

        if (currentSanity == 0)
            Debug.Log("Sem sanidade");
    }

    private void DisableBar()
    {
        sanityBar.GetComponent<Animator>().SetTrigger("Close");
    }
}
