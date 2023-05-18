using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();

    public SortedSet<GameObject> totalInventory = new SortedSet<GameObject>();


    private int maxCapacity = 10;
    int index = 0;

    bool addToBag;
    int bagTime = 3; //time to place object in bag

    public bool canPick;

  

    // Update is called once per frame
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

        for(int i=0; i<playerInventory.Count; i++)
        {
            totalInventory.Add(playerInventory[i]);
        }


        for(int i=0; i<handInventory.Count; i++)
        {
            totalInventory.Add(handInventory[i]);
        }
        
  //rotate clockwise through bag objects in hand
        // if(Input.GetKeyDown(KeyCode.J))
        // {
        //     if(index<totalInventory.Count)
        //     {
        //         index++;
        //     } else if(index==totalInventory.Count) {
        //         index = 0;
        //     }

            
        //     playerInventory.Add(handInventory[0]);
        //     playerInventory.Add(handInventory[1]);
        //     playerInventory.Remove(totalInventory[index]);
        //     playerInventory.Remove(totalInventory[index+1]);

        //     handInventory[0] = totalInventory[index];
        //     handInventory[1] = totalInventory[index+1];

            

        //     if(Input.GetKeyDown(KeyCode.K)) //put left hand object in bag 
        //     {
        //         //highlight handInventory[0];
        //         playerInventory.Add(handInventory[0]);
        //         handInventory.RemoveAt(0);
        //     }   
            
        // } else if(Input.GetKeyDown(KeyCode.L)) //rotate anticlockwise through bag objects in hand
        // {
        //      if(index<playerInventory.Count)
        //     {
        //         index--;
        //     } else if(index==playerInventory.Count) {
        //         index = 0;
        //     }

        //     handInventory[0] = playerInventory[index];
        //     handInventory[1] = playerInventory[index-1];

        //     playerInventory.Remove(handInventory[0]);
        //     playerInventory.Remove(handInventory[1]);

        //       if(Input.GetKeyDown(KeyCode.K)) //put right hand object in bag 
        //     {
        //         //highlight handInventory[0];
        //         playerInventory.Add(handInventory[1]);
        //         handInventory.RemoveAt(1);
        //     } 
        // }

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

    // void OnCollisionEnter(Collision other)
    void FixedUpdate() 
    { 
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

    void rotateClockwiseInventory() //think through how functions will work
    {

    }

     void rotateAntiClockwiseInventory()
    {
        
    }

    void dropObj()
    {
        
    }

    public IEnumerator moveObjToBag(GameObject obj)
    {

        if(addToBag)
        {
                //show object on screen
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

                for(int i = 0; i < handInventory.Count; i++) {
                    Debug.Log("handInventory size: "+handInventory.Count);
                    Debug.Log("handInventory: "+handInventory[i]);
                }

                for(int i = 0; i < playerInventory.Count; i++) {
                    Debug.Log("playerInventory size: "+playerInventory.Count);
                    Debug.Log("personalInventory: "+playerInventory[i]);
                }

                canPick = true;
        }
        }else if((addToBag==false) && (handInventory.Count==1)) { //if bag full and object in one hand, can only add one more object in other hand

            for(int i = 2; i < handInventory.Count; i++) {
                handInventory.RemoveAt(i);
            }

            handInventory.Add(obj);
            //show object in left hand

            for(int i = 0; i < handInventory.Count; i++) {
                    Debug.Log("handInventory size: "+handInventory.Count);
                    Debug.Log("handInventory: "+handInventory[i]);
                }

                for(int i = 0; i < playerInventory.Count; i++) {
                    Debug.Log("playerInventory size: "+playerInventory.Count);
                    Debug.Log("personalInventory: "+playerInventory[i]);
                }

                canPick = true;
        } else{
            Debug.Log("The bag is full, please remove an item"); //leaving object in had so player can drop
        }

       
    }
    
}
