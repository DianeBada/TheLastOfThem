using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();

    private int maxCapacity = 10;
    int index = 0;

    // Update is called once per frame
    void Update()
    {

//moving items from hand to bag      
           if(Input.GetKeyDown(KeyCode.Return)) 
        {
            if(playerInventory.Count != (maxCapacity))
            {
                playerInventory.Add(handInventory[0]);
                handInventory.RemoveAt(0); 
            }else{
                Debug.Log("Bag full");
            }

        }

  //rotate clockwise through bag objects in hand
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(index<playerInventory.Count)
            {
                index++;
            } else if(index==playerInventory.Count) {
                index = 0;
            }

            handInventory[0] = playerInventory[index];
            handInventory[1] = playerInventory[index+1];

            playerInventory.Remove(handInventory[0]);
            playerInventory.Remove(handInventory[1]);

            if(Input.GetKeyDown(KeyCode.K)) //put left hand object in bag 
            {
                //highlight handInventory[0];
                playerInventory.Add(handInventory[0]);
                handInventory.RemoveAt(0);
            }   
            
        } else if(Input.GetKeyDown(KeyCode.L)) //rotate anticlockwise through bag objects in hand
        {
             if(index<playerInventory.Count)
            {
                index--;
            } else if(index==playerInventory.Count) {
                index = 0;
            }

            handInventory[0] = playerInventory[index];
            handInventory[1] = playerInventory[index-1];

            playerInventory.Remove(handInventory[0]);
            playerInventory.Remove(handInventory[1]);

              if(Input.GetKeyDown(KeyCode.K)) //put right hand object in bag 
            {
                //highlight handInventory[0];
                playerInventory.Add(handInventory[1]);
                handInventory.RemoveAt(1);
            } 
        }

        //unequip
        // if(Input.GetKeyDown(KeyCode.L)) {
        //     if(handInventory!=null)
        //     {
        //         handInventory.RemoveAt(0);
        //     } else{
        //         Debug.Log("There is nothing in your hands to drop");
        //     }
        // }
    }

    // void OnCollisionEnter(Collision other)
    void FixedUpdate() 
    {
                
                    
                //} 
                //no weapons - this designed for single hand held gun
                // else if((handInventory[0].tag!="Gun") && (other.gameObject.tag!="Gun"))
                // {
                //     for(int i=0; i<pickableObjs.Count; i++)
                //     {
                //         if(other.gameObject.tag == pickableObjs[i])
                //             {
                //                 handInventory.Add(other.gameObject);
                //                 //move position of other gameObject to player's hand
                //             }
                //     }
               
                // } else if(((handInventory[0].tag=="Gun") && other.gameObject.tag!="Gun") || ((handInventory[0].tag!="Gun") && other.gameObject.tag=="Gun")) {
                //    for(int i=0; i<pickableObjs.Count; i++)
                //    {
                //         if(other.gameObject.tag == pickableObjs[i])
                //         {
                //             playerInventory.Add(handInventory[0]); //add first object to bag
                //             handInventory.RemoveAt(0); //remove object from hand
                            
                //             handInventory.Add(other.gameObject);
                //             //move position of other gameObject to player's hand

                //         }
                    
                   
                 
          
            

           
        }
    
}
