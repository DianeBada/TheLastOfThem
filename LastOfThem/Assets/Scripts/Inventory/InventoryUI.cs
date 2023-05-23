using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryUI : MonoBehaviour
{
    GameObject InventoryPanel;
    bool panelOpen;
    PCInventory pcInventory;
    GameObject parentPickUp;
    bool activate;

    private GameObject[] tubeImg;
    private GameObject[] tubeBtns;

    private List<GameObject> pcTubes = new List<GameObject>(); //testTubes in the personal inventory


    void Start()
    {
        InventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        parentPickUp = GameObject.FindGameObjectWithTag("ParentPickUp");
        pcInventory = parentPickUp.GetComponent<PCInventory>();

        tubeImg = GameObject.FindGameObjectsWithTag("TubeImg");
        tubeBtns = GameObject.FindGameObjectsWithTag("TubeBtn");

        //set all buttons inactive
        
        //sort arrays

        
        Debug.Log("tube image count: "+tubeImg.Length+ " tube btn count: "+tubeBtns.Length);

        updateTestTubeList();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(panelOpen)
            {
                InventoryPanel.SetActive(false);
                panelOpen = false;
            } else{
                InventoryPanel.SetActive(true);
                panelOpen = true;
            }
        } 
    }

    public void updateTestTubeList()
    {

        pcTubes.Clear();
        for(int i = 0; i < pcInventory.playerInventory.Count; i++) { 
            
            if(pcInventory.playerInventory[i].name=="TestTube")
            {
                pcTubes.Add(pcInventory.playerInventory[i]);
                Debug.Log("Test tubes: "+pcTubes.Count);
            }
        }

        //update UI
          for(int i = 0; i < pcTubes.Count; i++) {
                // tubeBtns[i].SetActive(true);
                // tubeImg[i].SetActive(true);
                tubeBtns[i].SetActive(true);
            }

            for(int i = pcTubes.Count; i < tubeBtns.Length; i++) {
                // tubeBtns[i].GetComponent<Button>().interactable = false; 
                tubeBtns[i].SetActive(false);
            }
    }

//  UI BUTTONS
    public void dropTestTube()
    {
        pcInventory.playerInventory.Remove(pcTubes[0]);
        Debug.Log("pc inventory: "+pcInventory.playerInventory.Count);
        updateTestTubeList();
    }

}
