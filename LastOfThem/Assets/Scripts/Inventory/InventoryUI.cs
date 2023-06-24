using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    GameObject InventoryPanel;
    bool panelOpen;
    PCInventory pcInventory;
    GameObject parentPickUp;
    bool activate;

    private GameObject[] tubeImg;
    private GameObject[] tubeBtns;

    TextMeshProUGUI tubeText;

    private bool keyPressed;

    private bool rightPressed;
    private bool leftPressed;

    int cycleIndex = 0;

    TextMeshProUGUI chemicalName;

    void Start()
    {
        InventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        parentPickUp = GameObject.FindGameObjectWithTag("ParentPickUp");
        pcInventory = parentPickUp.GetComponent<PCInventory>();

        tubeImg = GameObject.FindGameObjectsWithTag("TubeImg");
        tubeBtns = GameObject.FindGameObjectsWithTag("TubeBtn");

        foreach(GameObject btn in tubeBtns) {
            btn.SetActive(false);  //set active when object added
        }

        tubeText = GameObject.Find("TubeText").GetComponent<TextMeshProUGUI>();
        chemicalName = GameObject.Find("ChemicalName").GetComponent<TextMeshProUGUI>();

        updatePCList();
        initialhighlight();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(panelOpen)
            {
                InventoryPanel.SetActive(false);
                cycleIndex = 0;
                initialhighlight();
                panelOpen = false;
                //player should not be able to walk with arrow keys
            } else{
                InventoryPanel.SetActive(true);
                panelOpen = true;
            }
        } 

        tubeText.text = pcInventory.playerInventory.Count.ToString();
        
        if(pcInventory.playerInventory.Count>0)
        {
            if (cycleIndex >= pcInventory.playerInventory.Count)
            {
                cycleIndex = 0;
            }
            else if (cycleIndex < 0)
            {
                cycleIndex = (pcInventory.playerInventory.Count) - 1;
            }
            //Debug.Log(cycleIndex);
            chemicalName.text = pcInventory.playerInventory[cycleIndex].name;
        }


        //unequip
        if(Input.GetKeyDown(KeyCode.E))
        {
            keyPressed = true;

            if(keyPressed && panelOpen)
            {
                keyPressed = false;
                dropObj();  
            }
      
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightPressed = true;

            if(rightPressed && panelOpen)
            {
                rightPressed = false;
                cycle("right");
                
            }
        } else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
             leftPressed = true;

            if(leftPressed && panelOpen)
            {
                leftPressed = false;
                cycle("left");
            }   
        }

        //updateCycleInventory();
        //updatePCList();
    }

    public void initialhighlight() //selects where the player begins cycling from
    {

        tubeBtns[cycleIndex].GetComponent<Image>().color = Color.yellow;

        for(int i = 0; i < pcInventory.playerInventory.Count; i++) {

            if(i!=cycleIndex)
            {
                tubeBtns[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

//  UI BUTTONS
    public void dropObj()
    {
        pcInventory.playerInventory[cycleIndex].GetComponent<Tube>().drop=true;

        pcInventory.playerInventory[cycleIndex].transform.SetParent(null);
        pcInventory.playerInventory[cycleIndex].tag = "PickUp";
        pcInventory.playerInventory[cycleIndex].SetActive(true);
        pcInventory.playerInventory.RemoveAt(cycleIndex); //check if this is working
        Debug.Log("pc inventory: "+pcInventory.playerInventory.Count);

        tubeBtns[cycleIndex].SetActive(false);
    
        //updatePCList();
    }

    public void cycle(String direction)
    {
        if(direction=="right")
        {
            cycleIndex++;
        }else{
            cycleIndex--;
        }

        if(cycleIndex>=pcInventory.playerInventory.Count)
        {
            cycleIndex=0;
        } else if(cycleIndex<0)
        {
            cycleIndex=(pcInventory.playerInventory.Count)-1;
        }

        initialhighlight();
    }

    public void updatePCList() 
    {
        for(int i = 0; i < pcInventory.playerInventory.Count; i++) {
            tubeBtns[i].SetActive(true);
        }
    }
}

  