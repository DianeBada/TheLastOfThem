using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private TestTube testTube;
    
    // Start is called before the first frame update
    void Start()
    {
        testTube.PrintChemical();
    }

   
}
