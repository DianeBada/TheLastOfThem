using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();

    public List<string> pickableObjs = new List<string>(){"Flower"};
    public List<string> pickableWeapons = new List<string>(){"Gun", "Stick"};

    private int maxCapacity = 10;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
           if(Input.GetKeyDown(KeyCode.Return)) //moving items from hand ti bag
        {
            if(playerInventory.Count != (maxCapacity))
            {
                playerInventory.Add(handInventory[0]);
                handInventory.RemoveAt(0); 
            }else{
                Debug.Log("Bag full");
            }

        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            if(index<playerInventory.Count)
            {
                index++;
                handInventory[0] = playerInventory[index];
                // handInventory[1]
            }
            
        } else if(Input.GetKeyDown(KeyCode.K))
        {

        }

        if(Input.GetKeyDown(KeyCode.L)) {
            if(handInventory!=null)
            {
                handInventory.RemoveAt(0);
            } else{
                Debug.Log("There is nothing in your hands to drop");
            }
        }
    }

    void OnCollisionStay(Collision other)
    {
     
          
        if(Input.GetKeyDown(KeyCode.P)) //pick up - hand
        {
            if(handInventory.Count<2)
            {
                if(handInventory==null)
                {
                    for(int i=0; i<pickableObjs.Count; i++)
                    {
                        if(other.gameObject.tag == pickableObjs[i])
                        {
                            handInventory.Add(other.gameObject);
                            //move position of other gameObject to player's hand
                        }
                    }
                } else if((handInventory[0].tag!="Gun") && (other.gameObject.tag!="Gun"))
                {
                    for(int i=0; i<pickableObjs.Count; i++)
                    {
                        if(other.gameObject.tag == pickableObjs[i])
                            {
                                handInventory.Add(other.gameObject);
                                //move position of other gameObject to player's hand
                            }
                    }
               
                } else if(((handInventory[0].tag=="Gun") && other.gameObject.tag!="Gun") || ((handInventory[0].tag!="Gun") && other.gameObject.tag=="Gun")) {
                   for(int i=0; i<pickableObjs.Count; i++)
                   {
                        if(other.gameObject.tag == pickableObjs[i])
                        {
                            playerInventory.Add(handInventory[0]); //add first object to bag
                            handInventory.RemoveAt(0); //remove object from hand
                            
                            handInventory.Add(other.gameObject);
                            //move position of other gameObject to player's hand

                        }
                   }
                   
                }
            } else{
                Debug.Log("hand full, move something to bag first");
            }

           
        }

    }

    void handChange() //when player is holding another object or placing more items in bag
    {

        // for(int i = 0; i < handInventory.Count; i++) {
        //     if(handInventory)
        // }
    }
}
