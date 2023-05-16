using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUP : MonoBehaviour
{
   private Transform parentObject;

    GameObject player;
    PCInventory pcInventory;

    int bagTime = 3; //time to place object in bag

    void Start()
    {
       parentObject = GameObject.FindGameObjectWithTag("ParentPickUp").transform;
        pcInventory = parentObject.GetComponent<PCInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
                if (other.CompareTag("Player"))
        {

            // Debug.Log("Can press E");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(other.name);
                this.gameObject.transform.SetParent(parentObject);

            }

        }
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

                StartCoroutine("moveObjToBag");
            }
        }
    }

    IEnumerator moveObjToBag()
    {
        //show object on screen

        if(pcInventory.handInventory.Count==2)
        {
            bagTime = 0;
        }

        pcInventory.handInventory.Add(this.gameObject);
        Debug.Log(pcInventory.handInventory);
        yield return new WaitForSeconds(bagTime);
        this.gameObject.SetActive(false);
    }


}

