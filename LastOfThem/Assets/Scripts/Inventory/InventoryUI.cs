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

    private List<int> allowedIndices = new List<int>();

    TextMeshProUGUI tubeText;

    private bool hasRadio = false;
   noiseMeter soundMeter;
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
        soundMeter = FindObjectOfType<noiseMeter>();

        tubeImg = GameObject.FindGameObjectsWithTag("TubeImg");
        tubeBtns = GameObject.FindGameObjectsWithTag("TubeBtn");

        foreach(GameObject btn in tubeBtns) {
            btn.SetActive(false);  //set active when object added
        }

        tubeText = GameObject.Find("TubeText").GetComponent<TextMeshProUGUI>();
        chemicalName = GameObject.Find("Chemical Name Text").GetComponent<TextMeshProUGUI>();

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


            tubeBtns[cycleIndex].GetComponent<Image>().color = Color.yellow;

            for(int i = 0; i < pcInventory.playerInventory.Count; i++) {

                if(i!=cycleIndex)
                {
                    tubeBtns[i].GetComponent<Image>().color = Color.white;
                }
            }

            updatePCList();
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

        // tubeBtns[cycleIndex].GetComponent<Image>().color = Color.yellow;

        // for(int i = 0; i < pcInventory.playerInventory.Count; i++) {

        //     if(i!=cycleIndex)
        //     {
        //         tubeBtns[i].GetComponent<Image>().color = Color.white;
        //     }
        // }
    }

//  UI BUTTONS
    public void dropObj()
    {
        pcInventory.playerInventory[cycleIndex].GetComponent<Tube>().drop=true;

        pcInventory.playerInventory[cycleIndex].transform.SetParent(null);
        pcInventory.playerInventory[cycleIndex].SetActive(true);
        pcInventory.playerInventory[cycleIndex].tag = "PickUp";
        pcInventory.playerInventory.RemoveAt(cycleIndex); //check if this is working
        Debug.Log("pc inventory: "+pcInventory.playerInventory.Count);

        tubeBtns[cycleIndex].SetActive(false);

        UpdateCycleIndex();
        //updatePCList();
        soundMeter.CheckTestTubes();
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

        for(int i = 0; i < allowedIndices.Count; i++) {
            if(cycleIndex==allowedIndices[i])
            {
                return;
            }else if(cycleIndex!=allowedIndices[i] && i==(allowedIndices.Count-1))
            {
                compare(direction);
            }
        }

        initialhighlight();
    }

    public void UpdateCycleIndex()
    {
        int index =0;

        foreach(GameObject btn in tubeBtns)
        {
            if(btn.activeSelf)
            {
                allowedIndices.Add(index);
                index++;
            }
        }
    }

    public void compare(String direction){
        if(direction=="left")
        {
            for(int i =0; i<allowedIndices.Count; i++)
            {
                if((cycleIndex-i)>0)
                {
                    cycleIndex=i;
                    break;
                }else if(i==(allowedIndices.Count-1)){
                    cycleIndex=allowedIndices[i];
                }
            }
        }else{
            for(int i =0; i>allowedIndices.Count; i++)
            {
                if((cycleIndex-i)>0)
                {
                    cycleIndex=i;
                    break;
                }else if(i==(allowedIndices.Count-1)){
                    cycleIndex = allowedIndices[i];
                }
            }
        }
    }

    public void updatePCList() 
    {
        for(int i = 0; i < 9; i++) {
            if(i<pcInventory.playerInventory.Count) {
                tubeBtns[i].SetActive(true);
            } else{
                tubeBtns[i].SetActive(false);
            }
        }


        UpdateCycleIndex();
    }
}

  