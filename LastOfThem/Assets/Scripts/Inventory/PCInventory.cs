using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<Object> playerInventory = new List<Object>();
    public List<string> pickableObjs = new List<string>(){"Flower", "Stick"};
    private int maxCapacity = 10;
    public int a;

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
            if(playerInventory.Count != (maxCapacity-1))
            {
                for(int i=0; i<pickableObjs.Count; i++)
                {
                    if(other.gameObject.tag == pickableObjs[i])
                    {
                        playerInventory.Add(other.gameObject);
                    }
                }
            }else{
                Debug.Log("Inventory full");
            }
          
        }
    }
}
