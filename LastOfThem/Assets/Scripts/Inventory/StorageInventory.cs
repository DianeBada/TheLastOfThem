using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventory : MonoBehaviour
{

    private List<Object> storage = new List<Object>();
    private int maxCapacity = 50;

    PCInventory playerInventoryScript;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventoryScript = player.GetComponent<PCInventory>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {

            if(other.gameObject.tag == "Storage")
            {
                if((storage.Count != (maxCapacity-1)))
                {
                   playerInventoryScript.playerInventory.Remove(other.gameObject);  
                    storage.Add(other.gameObject);  

                } else{
                    Debug.Log("Inventory full");
                }
               
            } 
            
          
        }
    }
}
