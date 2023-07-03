using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ZombieManager : MonoBehaviour
{
    GameObject[] totalZombies;
    float totalNum;
    GameObject[] startingZombies;
    float startingNum;
    float zombieCount; //current zombie count
    float zombiePercentage;
    TextMeshProUGUI zombiesLeftTxt;

    GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        startingZombies = GameObject.FindGameObjectsWithTag("Zombie"); //total zombies at the begining of the scene
        startingNum = startingZombies.Length;
        zombiesLeftTxt = GameObject.Find("ZombiePercentage").GetComponent<TextMeshProUGUI>();

        GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcZombies();
    }

    public void LoseState()
    {
        SceneManager.LoadScene("Lose_Screen");
    }

    public void WinState()
    {
        SceneManager.LoadScene("Win_Screen"); 
    }

    public void CalcZombies()
    {
        totalZombies = GameObject.FindGameObjectsWithTag("Zombie"); //zombies in scene at current time
        totalNum = totalZombies.Length;
        zombiePercentage = ((totalNum/startingNum)*100);
        zombiesLeftTxt.text =  Mathf.Round(zombiePercentage)+"% of zombies left";

        if(GameManager.HasCure())
        {
            if(zombiePercentage==0)
            {
                WinState();
            }
        }else{
            if(zombiePercentage<=80)
            {
                LoseState();
            }else if(zombiePercentage<90)
            {
                zombiesLeftTxt.color = Color.red; //warning sign for users
            }
        }

    }
}
