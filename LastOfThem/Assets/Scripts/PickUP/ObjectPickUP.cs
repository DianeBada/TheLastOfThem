using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUP : MonoBehaviour
{
//    private Transform parentObject;
   private Transform objPickUp;

    GameObject player;
    PCInventory pcInventory;

    bool keyPressed;

    void Start()
    {
    //    parentObject = GameObject.FindGameObjectWithTag("ParentPickUp").transform;

        pcInventory = this.gameObject.GetComponent<PCInventory>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            keyPressed = true;
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            if (keyPressed)
            {
                keyPressed = false;

                    pcInventory.handInventory.Add(other.gameObject);

                    if (pcInventory.handInventory.Count>1) //can only pick up one object at a time
                    {
                        for(int i = 1; i < pcInventory.handInventory.Count; i++) {
                            pcInventory.handInventory.RemoveAt(i);
                        }
                    } 

                    StartCoroutine(pcInventory.moveObjToBag(other.gameObject));
                    // other.gameObject.transform.SetParent(gameObject);  
            }
        }
    }
}
