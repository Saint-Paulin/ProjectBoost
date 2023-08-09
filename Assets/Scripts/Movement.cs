using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audiosrc;
    // CollisionHandler collisionHandler;
    // CollisionHandler chScripts;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoost;
    [SerializeField] ParticleSystem sideBoostL;
    [SerializeField] ParticleSystem sideBoostR;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosrc = GetComponent<AudioSource>();
        // collisionHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        // CheatsCode();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }

    }

    private void StartThrust()
    {
        //float yValue = mainThrust * Time.deltaTime;
        // Debug.Log(yValue);
        //rb.AddRelativeForce(0, yValue, 0);
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // if (!audiosrc.isPlaying)
        if (audiosrc.isPlaying != true)
        {
            // audiosrc.Play();
            audiosrc.PlayOneShot(mainEngine);
            // mainBoost.Play();
            // sideBoostL.Play();
            // sideBoostR.Play();
        }
        if (!mainBoost.isPlaying)
        {
            // audiosrc.Play();
            mainBoost.Play();
            // sideBoostL.Play();
            // sideBoostR.Play();
        }
        //else if (audiosrc.isPlaying == true && Input.GetKey(KeyCode.Space) != true)
    }

    private void StopThrust()
    {
        audiosrc.Stop();
        mainBoost.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // Debug.Log("q");
            // transform.Rotate(0, 0, rotationThrust * Time.deltaTime);
            ApplyRotation(rotationThrust);
            if (!sideBoostR.isPlaying)
            {
                sideBoostR.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Debug.Log("d");
            // transform.Rotate(0, 0, -rotationThrust * Time.deltaTime);
            ApplyRotation(-rotationThrust);
            if (!sideBoostL.isPlaying)
            {
                sideBoostL.Play();
            }
        }
        else
        {
            sideBoostL.Stop();
            sideBoostR.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    
}
