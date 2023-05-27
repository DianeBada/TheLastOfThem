using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();
    
    InventoryUI inventoryUI;
    GameObject Canvas;

    private int maxCapacity = 10;
    //private int maxHand = 1; //for now player can only have one obj in hand. Makes picking and dropping more intuitive for player

    bool addToBag;

    private void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        inventoryUI = Canvas.GetComponent<InventoryUI>();
    }

    private void Update()
    {
    
        if(playerInventory.Count >= (maxCapacity))
        {

            addToBag = false;
            Debug.Log("Bag full");

        } else{
            addToBag = true;
        }


        //unequip
        if(Input.GetKeyDown(KeyCode.L)) {
            if(handInventory!=null)
            {
                handInventory.RemoveAt(0);
                //place object on ground
            } else{
                Debug.Log("There is nothing in your hands to drop");
            }
        }
    }

    public void moveObjToBag(GameObject obj)
    {

        if(addToBag)
        {
            //play pick up animation   
                      
            playerInventory.Add(handInventory[0]);
            //change position of child so visible on screen         
            handInventory[0].transform.SetParent(this.gameObject.transform);
            handInventory.Clear();

            if(obj.name =="TestTube")
            {
                inventoryUI.updateTestTubeList();
            }

            Debug.Log("playerInventory size: "+playerInventory.Count);
        }
        else{
            handInventory.Clear();
            Debug.Log("The bag is full, please remove an item"); //leaving object in had so player can drop
        }
       
    }

    public void AddObjectToInventory(GameObject obj)
    {
        playerInventory.Add(obj);
        
        if (handInventory.Count > 0)
        {
            for (int i = 0; i <handInventory.Count-1; i++)
            {
                handInventory.RemoveAt(i);
                handInventory[i].SetActive(false);
            }
        }
        handInventory.Add(obj);
        if (obj.name.Contains("TestTube"))
        {
            inventoryUI.updateTestTubeList();
        }
    }

    public void RemoveFromHand(GameObject obj)
    {
        handInventory.Clear();
        obj.SetActive(false);
    }
        

    
}
