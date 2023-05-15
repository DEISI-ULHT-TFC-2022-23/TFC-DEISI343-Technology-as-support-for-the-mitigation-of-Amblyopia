using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip levelComplete;

    
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;

    AudioSource audiosource;

    bool isTransitioning = false;

    void Start() {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if (isTransitioning) return;

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("This is friendly. Nothing happens!");
                break;
            case "Finish":
                StartLandingSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence(){
        isTransitioning = true;
        crashParticles.Play();
        audiosource.Stop();
        audiosource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDelay);
    }

    void StartLandingSequence(){
        isTransitioning = true;
        audiosource.Stop();
        levelCompleteParticles.Play();
        GetComponent<Movement>().enabled = false;
        audiosource.PlayOneShot(levelComplete);
        Invoke("NextLevel", levelDelay);
    }

    public void NextLevel(){
        int nextLevel = ( SceneManager.GetActiveScene().buildIndex + 1 ) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevel);
    }
    public void ReloadLevel(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
