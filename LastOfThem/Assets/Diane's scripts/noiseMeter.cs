using UnityEngine;
using UnityEngine.UI;

public class noiseMeter : MonoBehaviour
{
    public float noiseDetectionRange = 10.0f;
    public float noiseIncreasePerSecond = 0.1f;

    private bool isWalking = false;
    private bool isRunning = false;
    private bool isJumping = false;
    public float noisemeter = 0;
    public Slider noiseMeterSlider; // assign this in the Inspector
    // Update is called once per frame
    void Update()
    {
        // Check if player is walking, running or jumping
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            isWalking = true;
            Debug.Log("ey i am walking in the noisemeter script");
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            isWalking = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Check if any zombie is within the noise detection range
        bool isZombieInRange = false;
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            float distanceToZombie = Vector3.Distance(transform.position, zombie.transform.position);
            if (distanceToZombie < noiseDetectionRange)
            {
                isZombieInRange = true;
                break;
            }
        }

        // Increase noise meter gradually if player is making noise and a zombie is in range
        if ((isWalking || isRunning || isJumping) && isZombieInRange)
        {
            noisemeter += noiseIncreasePerSecond * Time.deltaTime;
        }
        else
        {
            noisemeter = Mathf.Max(0.0f, noisemeter - noiseIncreasePerSecond * Time.deltaTime);
        }

        // Do something with the noise meter, such as displaying it on a UI element
        Debug.Log("Noise meter: " + noisemeter);

        noiseMeterSlider.value = noisemeter;
    }

}
