using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Decanter : MonoBehaviour
{
    [SerializeField] private int testTubesDropped;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

        testTubesDropped = 0;
        gameManager = FindObjectOfType<GameManager>();
    }

 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("TestTube") )
        {
            IncrementTestTubesDropped();
           
            Tube currentTestTubeHolder = collision.gameObject.GetComponent<Tube>();
            TestTube currentTestTube = currentTestTubeHolder.GetTestTube();

            Color newColor = GetNewDecanterColor(currentTestTube);

            SetNewDecanterColor(newColor);

            MoveTestTubeDown(collision.gameObject);

            currentTestTubeHolder.DropTestTube();

        }
    }

    private void IncrementTestTubesDropped()
    {
        ++testTubesDropped;

        if (testTubesDropped == 3)
        {
            gameManager.SetCanMix(false);

        }
    }

    private void MoveTestTubeDown(GameObject testTube)
    {
        testTube.transform.DOMoveX(this.transform.position.x + 0.2f, 0.5f);
        testTube.transform.DOMoveY(this.transform.position.y - 3, 3f);
        
    }

  

    private Color GetNewDecanterColor( TestTube testTube)
    {
        var cubeRenderer = this.gameObject.GetComponent<MeshRenderer>();
        Color newColor;
        if (cubeRenderer.materials[0].color == Color.white)
        {
            newColor = testTube.GetColor();
        }
        else
        {
            newColor = Color.Lerp(testTube.GetColor(), cubeRenderer.materials[0].color, 0.5f);
        }
        
        return newColor;
    }

    private void SetNewDecanterColor(Color newColor)
    {
        var cubeRenderer = this.gameObject.GetComponent<MeshRenderer>();
       // cubeRenderer.materials[0].SetColor("_BaseColor", newColor);
        cubeRenderer.materials[0].DOColor(newColor, 2f);
    }

}

    //Color.Lerp(color1, color2, 0.5f)  startColor = Color.Lerp(Color.blue, Color.red, 0.5);}
