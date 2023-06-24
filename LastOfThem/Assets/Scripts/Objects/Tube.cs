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

    // Start is called before the first frame update
    void Start()
    {
        testTube.Refresh();
        indicator.SetActive(false);
        pcInventory = FindObjectOfType<PCInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
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
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !testTube.Picked())
        {
            StartCoroutine(PickUpObject());

        }

        /*if (other.CompareTag("Player") && !testTube.Picked() )
        {
            if (this.gameObject.activeInHierarchy)
            {
                Debug.Log("bugggy");
                StartCoroutine(PickUpObject());
            }
            
        }*/
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
        StartCoroutine(DeactivateTestTube());

        if (testTube.IsInCureFormula())
        {
            gameManager.IncrementCorrectChemicals();
            //make the test tube reappear in leave function
        }
        
    }

    private IEnumerator DeactivateTestTube()
    {
        yield return new WaitForSecondsRealtime(deactivationTime);
        this.gameObject.SetActive(false);
    }

    public TestTube GetTestTube()
    {
        return testTube;
    }


}
