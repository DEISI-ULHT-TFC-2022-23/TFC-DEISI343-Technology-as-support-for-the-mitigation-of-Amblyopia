using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrustForce = 10f;
    [SerializeField] float rocketRotationSpeed = 1f;
    [SerializeField] AudioClip mainEngineSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();

        CheckJoystickNames();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if (Input.GetAxis("Fire1") == 1 || Input.GetAxis("Jump") == 1) {
            StartThrusting();
        } else {
            StopThrusting();
        }

    }
    
    void ProcessRotation(){
        if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        } else if (Input.GetKey(KeyCode.A)) {
            RotateLeft();
        } else {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        if (!mainEngineParticles.isPlaying) mainEngineParticles.Play();
        if (!audiosource.isPlaying) audiosource.PlayOneShot(mainEngineSound);
        rb.AddRelativeForce(Vector3.up * rocketThrustForce * Time.deltaTime * Mathf.Max(Input.GetAxis("Fire1"), Input.GetAxis("Jump")));
    }
    
    void StopThrusting()
    {
        audiosource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateRight()
    {
        if (!rightThrusterParticles.isPlaying) rightThrusterParticles.Play();
        ApplyRotation(rocketRotationSpeed);
    }
    private void RotateLeft()
    {
        if (!leftThrusterParticles.isPlaying) leftThrusterParticles.Play();
        ApplyRotation(-rocketRotationSpeed);
    }

    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;    // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.back * rotationThisFrame * Time.deltaTime );
        rb.freezeRotation = false;   // unfreeze rotation so the physics system can take over
    }

    /* -----------------------------------------        */
    /* Not used anymore                                 */
    /* Haven't removed just for documentation tracking   */
    /* -----------------------------------------        */
    void CheckJoystickNames(){
    
        int numSticks;
        int i = 0;
 
        string sticks = "Joysticks\n";
 
        foreach (string joyName in Input.GetJoystickNames())
        {
            sticks += i.ToString() + ":" + joyName + "\n";
            i++;
        }
 
        Debug.Log("Sticks = " + i);
        Debug.Log(sticks);
 
        numSticks = i;
    }
}
