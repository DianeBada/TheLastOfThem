using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Test Tube", menuName ="Playable Objects/Test Tubes")]

public class TestTube : ScriptableObject
{
    [SerializeField] private string chemical;
    [SerializeField] private bool collected = false;
    [SerializeField] private GameObject asset;

    public void PrintChemical()
    {
        Debug.Log(chemical);
    }
    
    public void PickUp()
    {
        this.collected = true;
    }

    public bool Picked()
    {
        return collected;
    }
}
