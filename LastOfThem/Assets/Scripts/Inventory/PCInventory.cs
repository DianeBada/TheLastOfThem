using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();

    private List<GameObject> currentInventory = new List<GameObject>();

    //public List<GameObject> totalInventory = new List<GameObject>(); //playerInventory and handInventory

    private int maxCapacity = 10;
    private int maxHand = 1; //for now player can only have one obj in hand. Makes picking and dropping more intuitive for player

    bool addToBag;
    int bagTime = 1; //time to place object in bag

    public bool canPick;

    void Update()
    {
    
        if(playerInventory.Count >= (maxCapacity))
        {

            addToBag = false;
            Debug.Log("Bag full");

            if(handInventory.Count>0)
            {
                Debug.Log("Bag full, cannot place item in inventory");
            }

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

    public IEnumerator moveObjToBag(GameObject obj)
    {

        if(addToBag)
        {
            //play pick up animation
            if(handInventory.Count>=1) //can only pick up one object at a time, unless bag full then can hold two objects
            {
                bagTime = 0;

                for(int i = 1; i < handInventory.Count; i++) {
                    handInventory.RemoveAt(i);
                }
            } else{
                handInventory.Add(obj);

                yield return new WaitForSeconds(bagTime);
                //show object in right hand

                obj.SetActive(false);
                playerInventory.Add(handInventory[0]);
                handInventory.RemoveAt(0);

                canPick = true;
            }
        }
        else{
            Debug.Log("The bag is full, please remove an item"); //leaving object in had so player can drop
        }

        Debug.Log("handInventory size: "+handInventory.Count);
        Debug.Log("playerInventory size: "+playerInventory.Count);
    }
    
}

