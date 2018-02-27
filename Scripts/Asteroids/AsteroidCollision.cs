using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AsteroidCollision : MonoBehaviour {
    
    //Manager of Pooling System of Asteroids
    public PoolManager asteroidsPool;

    private AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //If THIS is a Big Asteroid 
        if (gameObject.tag == "BigAsteroid")
        {
            //Get Pooling system of Medium Asteroids
            asteroidsPool = GameObject.FindGameObjectWithTag("MedAsteroidsPool").GetComponent<PoolManager>();

        //If THIS is a Medium Asteroid
        } else if(gameObject.tag == "MedAsteroid")
        {
            //Get Pooling System of small asteroids
            asteroidsPool = GameObject.FindGameObjectWithTag("SmallAsteroidsPool").GetComponent<PoolManager>();

        }
    }

    //On Collision to the Asteroid Happen (Like a Shoot or crash)
    public void collided()
    {
            //Create 2 Asteroids from especific Pool
            for (var a = 0; a < 2; a++)
            {
                //If is avalaible
                if (asteroidsPool.haveItem())
                {
                    GameObject asteroid = asteroidsPool.getItem(); //Get Asteroid
                    asteroid.transform.position = transform.position; //Position where Collided
                    asteroid.SetActive(true); //Active it
                    asteroid.GetComponent<AsteroidRandomizeDirection>().randomizeRotation(); //Go to a random Direction
                }
            }
        
    }

    //Play Sound of Crash
    public void playSound()
    {
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();

        //Play sound on X Y of Scene
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
    }
    

}
