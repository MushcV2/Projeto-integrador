using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityController : MonoBehaviour
{
    [Header("Sanity Variables")]
    [SerializeField] private float maxSanity;
    [SerializeField] private Sprite[] logoImages;
    public float currentSanity;

    [Header("Sanity UI")]
    [SerializeField] private Slider sanityBar;
    [SerializeField] private Image logoImage;

    protected virtual void Start()
    {
        currentSanity = maxSanity;
        sanityBar.maxValue = maxSanity;

        logoImage.sprite = logoImages[0];

        sanityBar.gameObject.SetActive(false);
    }
    
    protected virtual void Update()
    {
        sanityBar.value = Mathf.Lerp(sanityBar.value, currentSanity, 0.6f);

        if (currentSanity < 75 && currentSanity >= 50) logoImage.sprite = logoImages[1];
        else if (currentSanity < 50 && currentSanity >= 25) logoImage.sprite = logoImages[2];
        else if (currentSanity < 25) logoImage.sprite = logoImages[3];
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
