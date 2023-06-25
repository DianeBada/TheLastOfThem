using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class noiseMeter : MonoBehaviour
{
    // [SerializeField] private float noiseDetectionRange = 10.0f;
    // [SerializeField] private float noiseIncreasePerSecond = 0.1f;
    // [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private float noiseOmitted = 0; //value out of 10


    private bool isWalking = false;
    private bool isRunning = false;
    private bool isJumping = false;

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

    [SerializeField] private Slider noiseMeterSlider; // assign this in the Inspector

    FirstPersonController FPS;

    bool radioOn;

    // bool isZombieInRange = false;

    Radio radio;

    public void Start()
    {
        noiseMeterSlider.maxValue = 10;
        FPS = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        radio = GameObject.Find("Radio").GetComponent<Radio>();
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
        // if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        // {
        //     isWalking = true;
        //     //Debug.Log("ey i am walking in the noiseOmitted script");
        // }

        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        // {
        //     isWalking = true;
        // }
        // else
        // {
        //     isWalking = false;
        // }
        // if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        // {
        //     isWalking = false;
        // }
        // if (Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     isRunning = true;
        //     // noise increase per second when running
        //     noiseIncreasePerSecond *= speedIncrease;
        // }

        // if (Input.GetKeyUp(KeyCode.LeftShift))
        // {
        //     isRunning = false;
        //     //  to its original value
        //     noiseIncreasePerSecond /= speedIncrease;
        // }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     isJumping = true;
        // }
        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     isJumping = false;
        // }

        if(noiseOmitted>=10)
        {
            noiseOmitted=10;
        }else if(noiseOmitted<=0)
        {
            noiseOmitted=0;
        }

        if(!radioOn)
        {
            UpdateNoiseMeter();
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
        Debug.Log("max");
    }

    public void RadioOff()
    {
        radioOn = false;
        UpdateNoiseMeter();
    }


    void UpdateNoiseMeter()
    {
        if(FPS.m_Jumping)
        {
            noiseOmitted = jumpingNoise;
            Debug.Log("jumping");
        } else if(FPS.getIsCrouching())
        {
            noiseOmitted = crouchingNoise;
            Debug.Log("crouching");
        }else if(FPS.isWalking)
        {
            noiseOmitted = walkingNoise;
            Debug.Log("walking");
        }else if(FPS.isRunning)
        {
            noiseOmitted = runningNoise;
            Debug.Log("running");
        }else if(!FPS.m_Jumping && !FPS.getIsCrouching() && !FPS.isWalking && !FPS.isRunning){
            noiseOmitted = crouchingNoise;
            Debug.Log("still");
        }
    }

}
