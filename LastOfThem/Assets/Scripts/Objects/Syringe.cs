using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{

    private Color syringeColor;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        syringeColor = Color.white;
        Disappear();
    }


    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Zombie"))
        {
            InjectZombie();
        }

    }

    public Color GetSyringeColor()
    {
        return syringeColor;
    }

    public void SetSyringeColor(Color newColor)
    {
        this.syringeColor = newColor;
        var syringeRenderer = this.gameObject.GetComponent<MeshRenderer>();
         syringeRenderer.materials[0].color = newColor;
    }

    private void InjectZombie()
    {
        Debug.Log("Zombie Injected");
        if (gameManager.HasCure())
        {
            CureInjection();
        }
        else
        {
            NoCureInjection();
        }
    }

    private void CureInjection()
    {
        Debug.Log("Zombie Cured");
    }

    private void NoCureInjection()
    {
        Debug.Log("Zombie Not Cured");
        gameManager.ClearMixture();
        Disappear();
        
    }

    private void Disappear()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Syringe has now disappeared");
    }


}
