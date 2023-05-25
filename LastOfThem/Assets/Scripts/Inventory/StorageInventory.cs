using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageInventory : MonoBehaviour
{

    private List<GameObject> storage = new List<GameObject>();
    private List<Image> storageImage = new List<Image>();

    private int maxCapacity = 50;

    PCInventory playerInventoryScript;
    GameObject player;
    
    GameObject panel;
    bool panelActive;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        playerInventoryScript = player.GetComponent<PCInventory>();

        // panel = GameObject.FindGameObjectWithTag("InventoryPanel");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.Q)) //button to bring up inventory
        {
            if(other.gameObject.tag == "Storage")
            {

                if((storage.Count != (maxCapacity)))
                {
                    //cycle through playerInventory through hands and select item
                    playerInventoryScript.playerInventory.Remove(other.gameObject);  
                    storage.Add(other.gameObject); 
                    other.gameObject.SetActive(false);

                } else{
                    Debug.Log("Inventory full");
                }
            } 
        }
    }

    private void OnTriggerExit(Collider other) {

        if(other.gameObject.tag == "Storage")
        {
            panel.SetActive(false);
        }

    }
}
