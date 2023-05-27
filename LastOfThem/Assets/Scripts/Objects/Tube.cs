using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private TestTube testTube;
    [SerializeField] private GameObject indicator;

    private PCInventory pcInventory;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        testTube.PrintChemical();
        indicator.SetActive(false);
        pcInventory = FindObjectOfType<PCInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PickUpObject());

        }

        if (other.CompareTag("Player") && !testTube.Picked() )
        {
            if (this.gameObject.activeInHierarchy)
            {
                Debug.Log("bugggy");
                StartCoroutine(PickUpObject());
            }
            
        }
    }

    private IEnumerator PickUpObject()
    {
        indicator.SetActive(false);
        pcInventory.AddObjectToInventory(this.gameObject);
        this.transform.SetParent(player.transform);
        testTube.PickUp();
        yield return new WaitForSecondsRealtime(2.5f);
        pcInventory.RemoveFromHand(this.gameObject);
        
    }


}
