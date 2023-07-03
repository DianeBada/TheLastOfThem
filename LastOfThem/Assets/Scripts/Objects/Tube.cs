using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private TestTube testTube;
    [SerializeField] private GameObject indicator;
    [SerializeField] private float deactivationTime = 0.2f;

   

    private PCInventory pcInventory;
    private GameObject player;
    private GameManager gameManager;

    private Vector3 mousePosition;

    public bool drop;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        testTube.Refresh();
        indicator.SetActive(false);
        pcInventory = FindObjectOfType<PCInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        originalPosition = this.gameObject.transform.position;
    }

    private void Update() {
        if(drop)
        {
             testTube.Refresh();
             Debug.Log("refreshed chemical");
             drop = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            indicator.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !testTube.Picked())
            {
                StartCoroutine(PickUpObject());

            }

            
        }

    
    }

    public void ResetTestTubeInTray()
    {
        this.gameObject.transform.position =originalPosition;
    }

    public TestTube getTestTube()
    {
        return testTube;
    }

    private IEnumerator PickUpObject()
    {
        indicator.SetActive(false);
        pcInventory.AddObjectToInventory(this.gameObject);
        this.transform.SetParent(player.transform);
        testTube.PickUp();
        gameManager.ActivateTestTubeInMixingRoom(testTube.GetChemical());
        yield return new WaitForSecondsRealtime(2.5f);
        pcInventory.RemoveFromHand(this.gameObject);
        
    }

    private Vector3 GetMousePositionOfTube()
    {
        return Camera.main.WorldToScreenPoint(this.transform.position);
    }

   private void OnMouseOver()
   {
        if (gameManager.IsInMixingRoom())
        {
            gameManager.SetInstructionPanelText(testTube.GetChemical(), "Location: Mixing Room", "Drag and Drop after pressing Esc, X - Exit");
        }
        
   }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePositionOfTube();
    }

    private void OnMouseDrag()
    {
        if (gameManager.IsInMixingRoom() && gameManager.CanMix())
        {
            
            this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }
        

    }

    public void DropTestTube()
    {
        gameManager.SetInstructionPanelText("Drag and Drop test tubes into white mould to make cure.", "Location: Mixing Room", "Drag and Drop after pressing Esc, X - Exit");
        
        if (testTube.IsInCureFormula())
        {
            gameManager.AddToKeepPanel(testTube.GetChemical());
            gameManager.IncrementCorrectChemicals();
            
          
        }
        StartCoroutine(DeactivateTestTube());
        pcInventory.RemoveTestTubeFromInventory(testTube);
        
    }

   

    private IEnumerator DeactivateTestTube()
    {
        yield return new WaitForSecondsRealtime(deactivationTime);
        if(testTube.Picked())
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
           
        }
    }

    public TestTube GetTestTube()
    {
        return testTube;
    }


}





