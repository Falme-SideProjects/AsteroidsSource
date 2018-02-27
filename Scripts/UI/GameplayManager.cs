using UnityEngine;

public class GameplayManager : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField]
    private GameObject pauseMenu;
    
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	

    private void openPause()
    {
        pauseMenu.SetActive(true);
    }

    private void OnEnable()
    {

        InputManager.inputSelectEscDelegate += openPause;

        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.pitch = 0.5f; //Set pitch to standard
        Invoke("accelerateSound", 1f); // Accelerate pitch ever second
    }

    private void OnDisable()
    {
        InputManager.inputSelectEscDelegate -= openPause;
        CancelInvoke();
    }

    private void accelerateSound()
    {
        audioSource.pitch *= 1.01f; //Increase velocity of sound
        Invoke("accelerateSound", 1f); //Do it again
    }

    public void stopAudio()
    {
        audioSource.Stop(); //Stop it 
    }

    public void resumeAudio()
    {
        audioSource.Play();
    }

    //Start again after play again, die or next level
    public void startAudioAgain()
    {
        audioSource.pitch = 0.5f;
        audioSource.Play();
    }
}
