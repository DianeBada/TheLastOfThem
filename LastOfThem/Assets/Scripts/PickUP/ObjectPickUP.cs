using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUP : MonoBehaviour
{
   private Transform parentObject;

    GameObject player;
    PCInventory pcInventory;



    void Start()
    {
       parentObject = GameObject.FindGameObjectWithTag("ParentPickUp").transform;
        pcInventory = parentObject.GetComponent<PCInventory>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            // Debug.Log("Can press E");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(other.name);
                this.gameObject.transform.SetParent(parentObject);
                pcInventory.canPick = false;

                StartCoroutine(pcInventory.moveObjToBag(this.gameObject));
                
            }
        }
    }
}

