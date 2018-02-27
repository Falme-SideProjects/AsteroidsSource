using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UFOCollision : MonoBehaviour {

    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //If collided with asteroids, Break Asteroids and destroy itself 
        if (collision.tag == "BigAsteroid" || collision.tag == "MedAsteroid")
        {
            playSound();
            gameObject.SetActive(false);
            collision.GetComponent<AsteroidCollision>().collided();
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "SmallAsteroid")
        {
            playSound();
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }


    }

    public void playSound()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

}
