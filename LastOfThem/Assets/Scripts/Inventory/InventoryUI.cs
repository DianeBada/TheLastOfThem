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

    public List<GameObject> pcTubes = new List<GameObject>(){}; //testTubes in the personal inventory
    private List<GameObject> pcRadio = new List<GameObject>(){}; //radio in the personal inventory
    private List<GameObject> pcRocks = new List<GameObject>(){}; //rocks in the personal inventory

    TextMeshProUGUI radioText;
    TextMeshProUGUI rockText;
    TextMeshProUGUI tubeText;

    GameObject rockIcon;
    GameObject radioIcon;

    private bool keyPressed;

    private bool rightPressed;
    private bool leftPressed;

    int cycleIndex = 0;

    private List<GameObject> cycleInventory = new List<GameObject>(){}; //for UI items
    private List<GameObject> cycleObjInventory = new List<GameObject>(){}; //for the corresponding gameObj

    void Start()
    {
        InventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        parentPickUp = GameObject.FindGameObjectWithTag("ParentPickUp");
        pcInventory = parentPickUp.GetComponent<PCInventory>();

        tubeImg = GameObject.FindGameObjectsWithTag("TubeImg");
        tubeBtns = GameObject.FindGameObjectsWithTag("TubeBtn");

        //find radio image

        radioText = GameObject.Find("RadioText").GetComponent<TextMeshProUGUI>();
        rockText = GameObject.Find("RockText").GetComponent<TextMeshProUGUI>();
        tubeText = GameObject.Find("TubeText").GetComponent<TextMeshProUGUI>();

        rockIcon = GameObject.Find("RockIcon");
        radioIcon = GameObject.Find("RadioIcon");

        //sort arrays

        updatePCList();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(panelOpen)
            {
                InventoryPanel.SetActive(false);
                cycleIndex = 0;
                panelOpen = false;
                //player should not be able to walk with arrow keys
            } else{
                InventoryPanel.SetActive(true);
                panelOpen = true;
            }
        } 

        radioText.text = pcRadio.Count.ToString();
        rockText.text = pcRocks.Count.ToString();
        tubeText.text = pcTubes.Count.ToString();


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

        updateCycleInventory();
    }

    public void updatePCList()
    {

        pcTubes.Clear();
        pcRocks.Clear();
        pcRadio.Clear();

        for(int i = 0; i < pcInventory.playerInventory.Count; i++) { 
            
            if(pcInventory.playerInventory[i].name.Contains("TestTube"))
            {
                pcTubes.Add(pcInventory.playerInventory[i]);
                Debug.Log("Test tubes: "+pcTubes.Count);
            }

              if(pcInventory.playerInventory[i].name.Contains("Rocks"))
            {
                pcRocks.Add(pcInventory.playerInventory[i]);
                Debug.Log("Rocks: "+pcRocks.Count);
            }

            if(pcInventory.playerInventory[i].name.Contains("Radio"))
            {
                pcRadio.Add(pcInventory.playerInventory[i]);
                Debug.Log("Radio: "+pcRadio.Count);
            }
        }

        //update UI
          for(int i = 0; i < pcTubes.Count; i++) {
                tubeBtns[i].SetActive(true);
            }

            for(int i = pcTubes.Count; i < tubeBtns.Length; i++) {
                tubeBtns[i].SetActive(false);
            }

        initialhighlight();
    }

    public void initialhighlight() //selects where the player begins cycling from
    {

        updateCycleInventory();

        if(cycleInventory.Count!=cycleIndex)
        {
            Debug.Log(cycleInventory[cycleIndex]);
            cycleInventory[cycleIndex].GetComponent<Image>().color = Color.yellow;
        }
    }

//  UI BUTTONS
    public void dropObj()
    {
        //needs to be updated to work with cycle
        //cycleInventory[cycleIndex].GetComponent<TestTube>().Drop();
        //updateCycleInventory();
        // Debug.Log("status of current object "+cycleInventory[cycleIndex].GetComponent<TestTube>().Refresh()); //try keep scriptable objects in array and refresh
        // cycleInventory[cycleIndex].GetComponent<Tube>().drop=true;

        Debug.Log(cycleObjInventory[cycleIndex]+" was dropped");
        pcInventory.playerInventory.Remove(cycleObjInventory[cycleIndex]); //check if this is working
        Debug.Log("pc inventory: "+pcInventory.playerInventory.Count);
        pcTubes[0].transform.SetParent(null);
        pcTubes[0].tag = "PickUp";
        updatePCList();
        Debug.Log("dropped obj");
    }

    public void cycle(String direction)
    {
        if(direction=="right")
        {
            cycleIndex++;
        }else{
            cycleIndex--;
        }

        if(cycleIndex>=cycleInventory.Count)
        {
            cycleIndex=0;
        } else if(cycleIndex<0)
        {
            cycleIndex=(cycleInventory.Count)-1;
        }

        updateCycleInventory();

         if(cycleInventory.Count!=0)
        {
            for(int i = 0; i < cycleInventory.Count; i++) {
                if(i!=cycleIndex)
                {
                    Debug.Log(cycleInventory[i]);
                    cycleInventory[i].GetComponent<Image>().color = Color.white;
                } else{
                    cycleInventory[i].GetComponent<Image>().color = Color.yellow;
                }
            }
        }
    }

    public void updateCycleInventory()
    {
        cycleInventory.Clear();
        cycleObjInventory.Clear();

         for(int i = 0; i < pcTubes.Count; i++) {
                cycleInventory.Add(tubeBtns[i]);
                cycleObjInventory.Add(pcTubes[i]);
            }

            if(pcRocks.Count!=0)
            {
                cycleInventory.Add(rockIcon);

                for(int i = 0; i < pcRocks.Count; i++) {
                cycleObjInventory.Add(pcRocks[i]);
                }

            }

            if(pcRadio.Count!=0)
            {
                cycleInventory.Add(radioIcon);
                
                 for(int i = 0; i < pcRadio.Count; i++) {
                cycleObjInventory.Add(pcRadio[i]);
                }

            }
    }



}
