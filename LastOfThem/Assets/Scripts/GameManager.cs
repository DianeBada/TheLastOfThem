using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool inMixingRoom;
    [SerializeField] private bool canMix;
    [SerializeField] private int correctChemicalsInSyringe;
    [SerializeField] private GameObject FirstPersonController;
    [SerializeField] private GameObject mixingRoomCamera;
    [SerializeField] private GameObject syringe;
    [SerializeField] private List<GameObject> testTubesInMixingRoom;


    // Start is called before the first frame update
    void Start()
    {
        inMixingRoom = false;
        canMix = true;
        correctChemicalsInSyringe = 0;

        foreach (GameObject testTube in testTubesInMixingRoom)
        {
            testTube.SetActive(false);
        }
    }

    private void Update()
    {
        if(inMixingRoom && Input.GetKeyDown(KeyCode.X))
        {
            ExitMixingRoom();
        }
    }

    public bool IsInMixingRoom()
    {
        return inMixingRoom;
    }

    public bool CanMix()
    {
        return canMix;
    }
    
    public void SetCanMix( bool value)
    {
        canMix = value;
    }

    public void IncrementCorrectChemicals()
    {
        ++correctChemicalsInSyringe;
    }

    public bool HasCure()
    {
        if(correctChemicalsInSyringe == 3)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void EnterMixingRoom()
    {
        inMixingRoom = true;
        FirstPersonController.SetActive(false);
        mixingRoomCamera.SetActive(true);
    }

    public void ExitMixingRoom()
    {
        inMixingRoom = false;
        FirstPersonController.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        FirstPersonController.SetActive(true);
        mixingRoomCamera.SetActive(false);
    }

    public void AppearSyringe()
    {
        syringe.SetActive(true);
    }

    public void ClearMixture()
    {
        correctChemicalsInSyringe = 0;
    }

    public void ActivateTestTubeInMixingRoom(string testTubeChemical)
    {
        
        foreach (GameObject testTubeObject in testTubesInMixingRoom)
        {
            if (testTubeObject.name.Contains(testTubeChemical))
            {
                //Debug.Log(testTubeChemical);
                testTubeObject.SetActive(true);
            }
        }
    }
    
}
