using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    [SerializeField] private GameObject riddlePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        if (riddlePanel!=null)
        {
            riddlePanel.SetActive(false);
        }
        
    }

    public void ActivatePanel()
    {
        if (riddlePanel != null)
        {
            riddlePanel.SetActive(true);
        }
    }

    public void DeactivatePanel()
    {
        if (riddlePanel != null)
        {
            riddlePanel.SetActive(false);
        }
    }

   /* private void OnMouseOver()
    {
        if (riddlePanel != null)
        {
            riddlePanel.SetActive(true);
        }
        
    }*/

   /* private void OnMouseExit()
    {
        if (riddlePanel != null)
        {
            riddlePanel.SetActive(false);
        }
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("test");
    }*/


}
