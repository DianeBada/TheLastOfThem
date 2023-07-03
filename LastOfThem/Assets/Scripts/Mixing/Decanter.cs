using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Decanter : MonoBehaviour
{
    [SerializeField] private int testTubesDropped;

    private GameManager gameManager;
    private Syringe syringe;
    // Start is called before the first frame update
    void Start()
    {
        //testTubesDropped = 0;
        gameManager = FindObjectOfType<GameManager>();
        syringe = FindObjectOfType<Syringe>();
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
            StartCoroutine(ThreeTestTubesDropped());

        }
    }

    private IEnumerator ThreeTestTubesDropped()
    {
        yield return new WaitForSecondsRealtime(1f);
        testTubesDropped = 0;
        gameManager.ExitMixingRoom();
        gameManager.AppearSyringe();
        syringe.SetSyringeColor(GetDecanterCurrentColor());
        gameManager.SetInstructionPanelText("Inject zombies with chemical mixture to cure curable zombies.", "Location: Lab facility", "Move syringe onto zombies.");
        yield return new WaitForSecondsRealtime(3f);
        gameManager.DeactivateInstructionPanel();
    }

    private void MoveTestTubeDown(GameObject testTube)
    {
        testTube.transform.DOMoveX(this.transform.position.x + 0.2f, 0.5f);
        testTube.transform.DOMoveY(this.transform.position.y - 3, 3f);
    }


    private Color GetDecanterCurrentColor()
    {
        var cubeRenderer = this.gameObject.GetComponent<MeshRenderer>();
        return cubeRenderer.materials[0].color;
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
        cubeRenderer.materials[0].DOColor(newColor, 2f);
    }

}

