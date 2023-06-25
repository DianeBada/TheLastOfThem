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

    private List<GameObject> pcTubes = new List<GameObject>(); //testTubes in the personal inventory
    private List<GameObject> pcRadio = new List<GameObject>(); //radio in the personal inventory
    private List<GameObject> pcRocks = new List<GameObject>(); //rocks in the personal inventory

    TextMeshProUGUI radioText;
    TextMeshProUGUI rockText;
    TextMeshProUGUI tubeText;

    private bool hasRadio = false;
    public noiseMeter soundMeter;


    void Start()
    {
        InventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        parentPickUp = GameObject.FindGameObjectWithTag("ParentPickUp");
        pcInventory = parentPickUp.GetComponent<PCInventory>();

        tubeImg = GameObject.FindGameObjectsWithTag("TubeImg");
        tubeBtns = GameObject.FindGameObjectsWithTag("TubeBtn");

        radioText = GameObject.Find("RadioText").GetComponent<TextMeshProUGUI>();
        rockText = GameObject.Find("RockText").GetComponent<TextMeshProUGUI>();
        tubeText = GameObject.Find("TubeText").GetComponent<TextMeshProUGUI>();

        //sort arrays

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

        radioText.text = pcRadio.Count.ToString();
        rockText.text = pcRocks.Count.ToString();
        tubeText.text = pcTubes.Count.ToString();

        hasRadio = pcRadio.Count > 0;

        // Update sound meter based on radio possession
        // if (hasRadio)
        // {
        //     // Increase sound meter level
        //     soundMeter.IncreaseSoundMeter();
        // }
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

              if(pcInventory.playerInventory[i].name=="Rocks")
            {
                pcRocks.Add(pcInventory.playerInventory[i]);
                Debug.Log("Rocks: "+pcRocks.Count);
            }

            if (pcInventory.playerInventory[i].name.Contains("TestTube"))
            {
                pcTubes.Add(pcInventory.playerInventory[i]);
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
        pcTubes[0].transform.SetParent(null);
        pcTubes[0].tag = "PickUp";
        updateTestTubeList();
    }


}
