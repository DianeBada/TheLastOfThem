using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{

    private Color syringeColor;
    private bool containsCure;
    // Start is called before the first frame update
    void Start()
    {
        syringeColor = Color.white;
        containsCure = false;
    }

    public Color GetSyringeColor()
    {
        return syringeColor;
    }

    public void SetSyringColor(Color newColor)
    {
        this.syringeColor = newColor;
    }

    private void InjectZombie()
    {
        Debug.Log("Zombie Injected");
    }

    private void Appear()
    {
        //set active
        Debug.Log("Syringe has now appeared");
    }

    private void Disppear()
    {
        //set inactive
        Debug.Log("Syringe has now disappeared");
    }

    private bool ContainsCure()
    {
        return containsCure;
    }


}
