using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInventory : MonoBehaviour
{
    public List<GameObject> playerInventory = new List<GameObject>();
    public List<GameObject> handInventory = new List<GameObject>();

    InventoryUI inventoryUI;
    GameObject canvas;

    private int maxCapacity = 10;
    private int requiredTestTubes = 2; // Number of test tubes required for the win condition

    bool addToBag;
    bool gameCompleted = false; // Tracks if the win condition has been achieved

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        inventoryUI = canvas.GetComponent<InventoryUI>();
    }

    private void Update()
    {
        if (playerInventory.Count >= maxCapacity)
        {
            addToBag = false;
            Debug.Log("Bag full");
        }
        else
        {
            addToBag = true;
        }

        // Unequip
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (handInventory.Count > 0)
            {
                handInventory.RemoveAt(0);
                // Place object on the ground
            }
            else
            {
                Debug.Log("There is nothing in your hands to drop");
            }
        }

        // Check win condition
        if (!gameCompleted && playerInventory.Count >= requiredTestTubes)
        {
            gameCompleted = true;
            CompleteGame();
        }
    }

    public void moveObjToBag(GameObject obj)
    {
        if (addToBag)
        {
            // Play pick up animation
            playerInventory.Add(handInventory[0]);
            // Change position of child to be visible on screen
            handInventory[0].transform.SetParent(transform);
            handInventory.Clear();

            if (obj.name == "TestTube")
            {
                inventoryUI.updateTestTubeList();
            }

            Debug.Log("Player inventory size: " + playerInventory.Count);
        }
        else
        {
            handInventory.Clear();
            Debug.Log("The bag is full, please remove an item"); // Leaving object in hand so player can drop it
        }
    }

    public void AddObjectToInventory(GameObject obj)
    {
        playerInventory.Add(obj);

        if (handInventory.Count > 0)
        {
            for (int i = 0; i < handInventory.Count - 1; i++)
            {
                handInventory.RemoveAt(i);
                handInventory[i].SetActive(false);
            }
        }
        handInventory.Add(obj);
        if (obj.name.Contains("TestTube"))
        {
            inventoryUI.updateTestTubeList();
        }
    }

    public void RemoveFromHand(GameObject obj)
    {
        handInventory.Clear();
        obj.SetActive(false);
    }

    void CompleteGame()
    {
        // Game completion logic
        Debug.Log("Congratulations! You have completed the game.");
        // Perform any other required actions for the win condition
    }
}
