using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUP : MonoBehaviour
{
    private Transform parentObject;
    [SerializeField] private GameObject textPanel;
    private bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = GameObject.FindGameObjectWithTag("ParentPickUp").transform;
        textPanel.SetActive(false);
        pickedUp = false;
    }

    // Update is called once per frame
    /* void Update()
     {

     }*/

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !pickedUp)
        {
            textPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log(other.name);
                this.transform.SetParent(parentObject);
                textPanel.SetActive(false);
                pickedUp = true;
            }
                
        }
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
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        textPanel.SetActive(false);
    }


}

