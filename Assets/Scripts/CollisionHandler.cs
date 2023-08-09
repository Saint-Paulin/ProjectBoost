using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Variables :
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip rocketCrash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem rocketCrashParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audiosrc;
    ParticleSystem particleSystem;
    // CollisionHandler collisionHandler;

    bool isTransitioning = false;
    bool collisionDisabled = false;
    
    // Movement Movement;

    void Start() {
        audiosrc = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update() {
        CheatsCode();
    }

    void CheatsCode()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FinishLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            // 1 ere methode 
            collisionDisabled = !collisionDisabled;

            // 2 eme methode 
            if (!isTransitioning)
            {
                isTransitioning = true;
                //Debug.Log("True");
            }
            else
            {
                isTransitioning = false;
                //Debug.Log("False");
            }
        }
    }

    void OnCollisionEnter(Collision other) {

        if (isTransitioning || collisionDisabled) { return; }

        // if (other.gameObject.tag == "Player") {
           // GetComponent<MeshRenderer>().material.color = Color.red;
            //gameObject.tag = "Hit";
            /*
            switch (variable to compare)
                {
                    case ValueA:
                        ActionToTake();
                        break;
                    case ValueB:
                        OtherAction();
                        break;
                    default:
                        YetAnotherAction();
                        break;
                }
            */
        //}

        switch (other.gameObject.tag)
                {
                    case "Friendly":
                        Debug.Log("Friendly");
                        break;
                    case "Finish":
                        // Debug.Log("Finish");
                        // FinishLevel();
                        //if (isTransitioning = false)
                        //{
                        StartSuccessSequence();
                        //}
                        break;
                    case "Fuel":
                        Debug.Log("fuel");
                        break;
                    default:
                        // Debug.Log("Untagged");

                        StartCrashSequence();
                        // Invoke("StartCrashSequence", delay);
                        break;
                }
    }

    void StartSuccessSequence()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            audiosrc.Stop();
            audiosrc.PlayOneShot(success);
            particleSystem.Play(successParticle);
            //collisionHandler.Play(successParticle);
        // movement = GetComponent<Movement>();
        // movement.enabled = false;
            GetComponent<Movement>().enabled = false;
        // GameObject.Find("Player").GetComponent("rocketCrash").enabled = false;
        // GetComponent<CollisionHandler>().enabled = false;
        // gameObject.movement = false;
            Invoke ("FinishLevel", levelLoadDelay);
        }
    }

    void StartCrashSequence()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            audiosrc.Stop();
            audiosrc.PlayOneShot(rocketCrash);
            particleSystem.Play(rocketCrashParticle);
            
        // movement = GetComponent<Movement>();
        // movement.enabled = false;
            GetComponent<Movement>().enabled = false;
        // GetComponent<CollisionHandler>().enabled = false;
        // gameObject.movement = false;
            Invoke ("ReloadLevel", levelLoadDelay);
        }
    }

    void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene(CurrentSceneIndex);
        // SceneManager.LoadScene(0);
    }

    void FinishLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            // NextSceneIndex = 0;
        }
        else
        {
            SceneManager.LoadScene(CurrentSceneIndex + 1);
        }
        // SceneManager.LoadScene(NextSceneIndex);        
    }
}
