using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool inMixingRoom;
    [SerializeField] private bool canMix;
    [SerializeField] private int correctChemicalsInSyringe;


    // Start is called before the first frame update
    void Start()
    {
        /*inMixingRoom = false;
        canMix = true;
        correctChemicalsInSyringe = 0;*/
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
}
