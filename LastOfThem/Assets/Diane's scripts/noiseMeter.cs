using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class noiseMeter : MonoBehaviour
{
    // [SerializeField] private float noiseDetectionRange = 10.0f;
    // [SerializeField] private float noiseIncreasePerSecond = 0.1f;
    // [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private float noiseOmitted = 0; //value out of 10

    private bool isWalking;
    private bool isRunning;
    private bool isJumping;
    private bool isCrouching;

    private bool fullTestTubes;

    [SerializeField]
    int crouchingNoise = 1;
    [SerializeField]
    int walkingNoise = 3;
    [SerializeField]
    int runningNoise = 5;
    [SerializeField]
    int jumpingNoise = 7;
    [SerializeField]
    int maxNoise = 10;

    int waitTime = 2;

    [SerializeField] 
    private Slider noiseMeterSlider; // assign this in the Inspector

    FirstPersonController FPS;

    bool radioOn;

    GameObject[] zombies;

    Radio radio;

    PCInventory PCInventory;
    GameObject player;

    public void Start()
    {
        noiseMeterSlider.maxValue = 10;
        player = GameObject.FindGameObjectWithTag("Player");
        FPS = player.GetComponent<FirstPersonController>();

        PCInventory = GameObject.FindGameObjectWithTag("ParentPickUp").GetComponent<PCInventory>();

        radio = GameObject.Find("Radio").GetComponent<Radio>();
        zombies = GameObject.FindGameObjectsWithTag("Zombie");
    }

    // public void IncreaseSoundMeter()
    // {
    //     noiseOmitted += noiseIncreasePerSecond * Time.deltaTime;
    //     noiseMeterSlider.value = noiseOmitted;
    // }
    // Update is called once per frame
    void Update()
    {
        // Check if player is walking, running or jumping
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            isWalking = true;
            //Debug.Log("ey i am walking in the noiseOmitted script");
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                 Debug.Log("should be running");
                isRunning = true;
                isWalking = false;
                isCrouching = false;
            }else if(isCrouching)
            {
                isRunning = false;
                isWalking = false;
            }else{
                isWalking = true;
                isRunning = false;
                isCrouching = false;
            }
        }    
        else{
            isRunning = false;
            isWalking = false;
        }

        if(Input.GetKeyDown(KeyCode.C)){
            isCrouching = !isCrouching;
            Debug.Log("player is crouching: "+isCrouching);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            isJumping = true;
        }else if(Input.GetKeyUp(KeyCode.Space)){
            StartCoroutine("Wait");
            isJumping=false;
        }

        if(noiseOmitted>=10)
        {
            noiseOmitted=10;
        }else if(noiseOmitted<=0)
        {
            noiseOmitted=0;
        }

        if(!radioOn)
        {
            if(CheckTestTubes()==false)
            {
                UpdateNoiseMeter();
            }
        }

        // Check if any zombie is within the noise detection range
        // bool isZombieInRange = false;
        //CheckZombieRange();
  

        // Increase noise meter gradually if player is making noise and a zombie is in range
        // if (isWalking || isRunning || isJumping)
        // {
        //     noiseOmitted += noiseIncreasePerSecond * Time.deltaTime;
        // }
        // else
        // {
        //     noiseOmitted = Mathf.Max(0.0f, noiseOmitted - noiseIncreasePerSecond * Time.deltaTime);
        // }

        // Do something with the noise meter, such as displaying it on a UI element
        // Debug.Log("Noise meter: " + noiseOmitted);
        
        noiseMeterSlider.value = noiseOmitted;
    }

    //  void CheckZombieRange()
    // {
    //     foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie"))
    //     {
    //         float distanceToZombie = Vector3.Distance(transform.position, zombie.transform.position);
    //         if (distanceToZombie < noiseDetectionRange)
    //         {
    //             isZombieInRange = true;
    //             break;
    //         }else{
    //             isZombieInRange = false;
    //         }
    //     }
    // }

    public void RadioOn() //radio off -> code path 
    {
        noiseOmitted = maxNoise;
        radioOn = true;
        UpdateZombieDistance(1.0f);
    }

    public void RadioOff()
    {
        radioOn = false;
        UpdateNoiseMeter();
    }

    public bool CheckTestTubes()
    {
        if(PCInventory.playerInventory.Count>=3)
        {
            noiseOmitted = maxNoise;
            UpdateZombieDistance(1.0f);
            return true;
        }else{
            return false;

        }
    }

    void UpdateNoiseMeter()
    {
        if(isJumping)
        {
            noiseOmitted = jumpingNoise;
            Debug.Log("jumping");

            UpdateZombieDistance(0.9f);
        } 
        else if(isCrouching) 
        {
            noiseOmitted = crouchingNoise;
            Debug.Log("crouching");
            UpdateZombieDistance(0.60f);
        }
        else if(isWalking)
        {
            noiseOmitted = walkingNoise;
            Debug.Log("walking");
            UpdateZombieDistance(0.7f);
        }else if(isRunning)
        {
            noiseOmitted = runningNoise;
            Debug.Log("running");
            UpdateZombieDistance(0.8f);
        }else if((isJumping==false)  && (isWalking==false) && (isRunning==false) && (isCrouching==false)){ 
            noiseOmitted = crouchingNoise;
            UpdateZombieDistance(0.6f);
            //Debug.Log("still");
            
        }


    }

    void UpdateZombieDistance(float offsetFactor) //5 different noiseMeter levels, multiplying each level by total distance
    {
        foreach(GameObject zombie in zombies)
        {
            Zombie script = zombie.GetComponent<Zombie>();
            script.detectionDistance = script.maxDetectionDistance*offsetFactor; 
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
    }

}
