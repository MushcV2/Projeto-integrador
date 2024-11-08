using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void DisableWithAnimation()
    {
        Invoke(nameof(DisableClock), 3f);
    }

    private void DisableClock()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Close");

        IEnumerator Disable()
        {
            yield return new WaitForSeconds(0.5f);
            DisableObject();
        }

        if (gameObject.activeSelf) StartCoroutine(Disable());
    }
}