using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private List<Object> playerInventory = new List<Object>();
    string[] pickableObjs = new string[] {"Flower", "Stick"};  //if the object can be picked up?
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            for(int i=0; i<pickableObjs.Length; i++)
            {
                if(other.gameObject.tag == pickableObjs[i])
                {
                    playerInventory.Add(other.gameObject);
                }
            }
          
        }
    }
}
