using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Test Tube", menuName ="Playable Objects/Test Tubes")]

public class TestTube : ScriptableObject
{
    [SerializeField] private string chemical;
    [SerializeField] private bool collected = false;
    [SerializeField] private GameObject asset;
    [SerializeField] private Color color;
    [SerializeField] private bool isInCure = false;
    [SerializeField] private Vector3 originalPositionInTray;

    public void PrintChemical()
    {
        Debug.Log(chemical);
    }
    
    public void PickUp()
    {
        this.collected = true;

    }

    public void Drop()
    {
        this.collected = false;
    }

    public Vector3 GetOriginalPositionInTray()
    {
        return originalPositionInTray;
    }

    public bool Picked()
    {
        return collected;
    }

    public void Refresh()
    {
        this.collected = false;
        //PrintChemical();
    }

    public Color GetColor()
    {
        return color;
    }

    public bool IsInCureFormula()
    {
        return isInCure;
    }

    public string GetChemical()
    {
        return chemical;
    }

    
}
