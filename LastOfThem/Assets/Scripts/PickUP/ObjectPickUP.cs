using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUP : MonoBehaviour
{
    private Transform parentObject;
    [SerializeField] private GameObject textPanel;
    private bool pickedUp = false;

    PCInventory pcInventory;

    void Start()
    {

        parentObject = GameObject.FindGameObjectWithTag("ParentPickUp").transform;
        pcInventory = parentObject.GetComponent<PCInventory>();
        
        textPanel.SetActive(false);
        pickedUp = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && !pickedUp)
        {

            textPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(other.name);
                this.gameObject.transform.SetParent(parentObject);
                textPanel.SetActive(false);
                pickedUp = true;
                pcInventory.canPick = false;
                StartCoroutine(pcInventory.moveObjToBag(this.gameObject));
            }
             
            }
        }

    private void OnTriggerExit(Collider other)
    {
        textPanel.SetActive(false);
    }


}

