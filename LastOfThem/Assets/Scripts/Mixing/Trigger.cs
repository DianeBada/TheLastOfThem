using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if(other.gameObject.transform.position.x > this.transform.position.x)
            {
                //Debug.Log("exiting mixing room");
                gameManager.ExitMixingRoom();
            }
            else
            {
                //Debug.Log("in mixing room");
                gameManager.EnterMixingRoom();
            }
        }
    }
}
