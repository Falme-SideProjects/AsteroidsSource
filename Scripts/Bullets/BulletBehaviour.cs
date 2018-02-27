using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    //Velocity of Bullet
    [SerializeField]
    private float velocity;

    private AudioSource audioSource;

    //Bullet will live 1.5 seconds before destroy itself
    private float lifeBullet = 1.5f;

    // Update is called once per frame
    void Update () {
        
        //Movementation
        transform.position += transform.up * Time.deltaTime * velocity;
    }


    private void OnEnable()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        //Play sound on X Y of Scene
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        Invoke("Destroy", lifeBullet); //After X seconds, Self Destroy
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(); //Cancel self destruct
    }
}
