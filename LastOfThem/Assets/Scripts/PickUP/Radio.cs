using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    noiseMeter noiseMeter;

    private GameObject player;
    private bool picked = false;
    private bool canDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        noiseMeter = player.GetComponent<noiseMeter>();
        picked = false;
        canDrop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !picked)
        {
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !picked)
        {
            indicator.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !picked)
        {
            StartCoroutine(PickUpObject());

        }
        
        if (Input.GetKeyDown(KeyCode.E) && canDrop)
        {
            StartCoroutine(DropObject());

        }
    }



    private IEnumerator PickUpObject()
    {
        indicator.SetActive(false);
        noiseMeter.RadioOn();
        picked = true;
        this.transform.SetParent(player.transform);
        yield return new WaitForSecondsRealtime(0.5f);
        canDrop = true;

    }

    private IEnumerator DropObject()
    {
        
        this.transform.SetParent(null);
        canDrop = false;
        yield return new WaitForSecondsRealtime(0.5f);
        picked = false;
        noiseMeter.RadioOff();
    }

    public bool IsPicked()
    {
        
        return picked;
    }

}
