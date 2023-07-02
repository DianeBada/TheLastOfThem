using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{

    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();
    
    InventoryUI inventoryUI;
    GameObject Canvas;

    GameObject Player;
    private noiseMeter NoiseMeter;

    private void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        inventoryUI = Canvas.GetComponent<InventoryUI>();
        Player = GameObject.FindGameObjectWithTag("Player");
        NoiseMeter = FindObjectOfType<noiseMeter>();
    }

    public void AddObjectToInventory(GameObject obj)
    {
        if (!playerInventory.Contains(obj))
        {
            playerInventory.Add(obj);
            obj.transform.SetParent(Player.transform);

            if (handInventory.Count > 0)
            {
                for (int i = 0; i < handInventory.Count - 1; i++)
                {
                    handInventory.RemoveAt(i);
                    handInventory[i].SetActive(false);
                }
            }

            handInventory.Add(obj);
            if (obj.name.Contains("TestTube") || obj.name.Contains("Radio") || obj.name.Contains("Syringe"))
            {
                inventoryUI.updatePCList();
            }

            NoiseMeter.CheckTestTubes();
        }
        
    }

    public void RemoveFromHand(GameObject obj)
    {
        handInventory.Clear();
        obj.SetActive(false);
    }
        
    public void RemoveTestTubeFromInventory(TestTube testTube)
    {
        //playerInventory.Remove(obj);

        GameObject currentTestTube = null;

        foreach(GameObject obj in playerInventory)
        {
            if (obj.name.Contains(testTube.GetChemical()))
            {
                currentTestTube = obj;
            }
        }
        playerInventory.Remove(currentTestTube);
        inventoryUI.updatePCList();
    }
    
}
