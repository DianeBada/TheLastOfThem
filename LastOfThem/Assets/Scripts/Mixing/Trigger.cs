using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    /*   private void OnTriggerEnter(Collider other)
       {
           if (other.gameObject.CompareTag("Player"))
           {
               Debug.Log("In mixing room now lol");
               //gameManager.ToggleInMixingRoom();
           }
       }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("In mixing room now lol");
            
            if(other.gameObject.transform.position.x > this.transform.position.x)
            {
                Debug.Log("exiting mixing room");
                gameManager.ExitMixingRoom();
            }
            else
            {
                Debug.Log("in mixing room");
                gameManager.EnterMixingRoom();
            }
            //gameManager.ToggleInMixingRoom();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("In mixing room now lol");
            gameManager.ToggleInMixingRoom();
        }*/
    }
}
