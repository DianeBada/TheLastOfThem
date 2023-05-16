using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUP : MonoBehaviour
{
   private Transform parentObject;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = GameObject.FindGameObjectWithTag("ParentPickUp").transform;
    }

    // Update is called once per frame
    /* void Update()
     {

     }*/

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(other.name);
                this.transform.SetParent(parentObject);
            }
                
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(other.name);
                this.gameObject.transform.SetParent(parentObject);
            }

        }
    }


}

