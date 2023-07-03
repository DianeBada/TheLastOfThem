using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private TextMeshProUGUI locationText;
    [SerializeField] private TextMeshProUGUI controlText;
    [SerializeField] private GameObject instructionPanel;

    private List<GameObject> testTubesToKeepInPanel = new();

    [SerializeField] private GameObject argon;



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
        DeactivateInstructionPanel();
    }

    private void Update()
    {
        if (inMixingRoom && Input.GetKeyDown(KeyCode.X))
        {
            ExitMixingRoom();
        }
    }

    public void AddToKeepPanel(string testTubeChemical)
    {
        foreach (GameObject testTubeObject in testTubesInMixingRoom)
        {
            if (testTubeObject.name.Contains(testTubeChemical))
            {
                testTubesToKeepInPanel.Add(testTubeObject);
            }
        }
    }

    public void SetInstructionPanelText(string instruction, string location, string control)
    {
        instructionText.text = instruction;
        locationText.text = location;
        controlText.text = control;
        ActivateInstructionPanel();
    }

    private void ActivateInstructionPanel()
    {
        instructionPanel.SetActive(true);
    }

    public void DeactivateInstructionPanel()
    {
        ClearInstructionPanelText();
        instructionPanel.SetActive(false);
    }

    private void ClearInstructionPanelText()
    {
        instructionText.text = "";
        locationText.text = "";
        controlText.text = "";
    }

    public bool IsInMixingRoom()
    {
        return inMixingRoom;
    }

    public bool CanMix()
    {
        return canMix;
    }

    public void SetCanMix(bool value)
    {
        canMix = value;
    }

    public void IncrementCorrectChemicals()
    {
        ++correctChemicalsInSyringe;
    }

    public bool HasCure()
    {
        if (correctChemicalsInSyringe >= 3)
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
        mixingRoomCamera.tag = "MainCamera";
        mixingRoomCamera.SetActive(true);
        SetInstructionPanelText("Drag and Drop test tubes into white mould to make cure.", "Location: Mixing Room", "Drag and Drop after pressing Esc, X - Exit");
    }

    public void ExitMixingRoom()
    {
        inMixingRoom = false;
        FirstPersonController.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
        argon.SetActive(false);
        FirstPersonController.SetActive(true);
        mixingRoomCamera.SetActive(false);
        DeactivateInstructionPanel();
        ActivateTestTubesToKeep();

    }

    private void ActivateTestTubesToKeep()
    {

        foreach (GameObject testTubeObject in testTubesToKeepInPanel)
        {
            testTubeObject.SetActive(true);
            testTubeObject.GetComponent<Tube>().ResetTestTubeInTray();
        }
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
