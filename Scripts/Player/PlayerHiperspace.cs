using UnityEngine;

[RequireComponent(typeof(AudioClip))]
public class PlayerHiperspace : MonoBehaviour {

    //Can do hyperspace ?
    private bool cantHiperspace = false;

    //SoundHyperspace
    [SerializeField]
    private AudioClip audioSource;

    //Enable it
    public void enableHyperspace()
    {
        disableHyperspace(); //Confirm if was not set
        InputManager.inputHyperspaceDelegate += hyperspace; //Add to delegate
    }

    public void disableHyperspace()
    {
        InputManager.inputHyperspaceDelegate -= hyperspace; //Remove from delegate
    }

    // Update is called once per frame
    void Update () {

        //If paused or unpaused
        if (cantHiperspace != GameStates.paused)
        {
            cantHiperspace = GameStates.paused;

            if (!cantHiperspace) enableHyperspace();
            else disableHyperspace();
        }
        
    }

    private void OnEnable()
    {
        enableHyperspace();
    }

    private void OnDisable()
    {
        disableHyperspace();
    }

    private void hyperspace()
    {
        playSound();
        gameObject.SetActive(false);
        Invoke("spawn", 1f); // Reappear on screen after 1 second
    }
    

    private void spawn()
    {
        //set at random place X Y
        float y = Random.Range(-Boundaries.halfY, Boundaries.halfY);
        float x = Random.Range(-Boundaries.halfX, Boundaries.halfX);

        transform.position = new Vector3(x, y);

        gameObject.SetActive(true); //Appeaar player
    }

    //Play Sound of Hyperspace
    public void playSound()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioClip>();

        //Play sound on X Y of Scene
        AudioSource.PlayClipAtPoint(audioSource, transform.position);
    }
}
