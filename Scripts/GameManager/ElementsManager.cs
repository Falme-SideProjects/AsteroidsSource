using UnityEngine;

public class ElementsManager : MonoBehaviour {

    //Player - UFO Big - UFO Small - Commands/Keys Screen
    [SerializeField]
    private GameObject player, BigUFO, SmallUFO, helpScreen;

    //Asteroids pooling system
    [SerializeField]
    private PoolManager BigAsteroids, MedAsteroids, SmallAsteroids;

    //Get Gameplay manager
    [SerializeField]
    private GameplayManager gameplayManager;
    
    //If cleared asteroids, and wait to go to next Level
    private bool changingLevel;

    //Where the asteroids and UFO plays in Main Menu
    public void startDemo()
    {
        player.SetActive(false);
        BigUFO.SetActive(false);
        SmallUFO.SetActive(false);

        spawnAsteroids(); //Spawn 4 asteroids

        MedAsteroids.inactiveAll();
        SmallAsteroids.inactiveAll();
        
        Invoke("spawnUFO", 15f); //Spawn UFO each 15 seconds
    }

    //Gameplay screen
    public void startGameplay()
    {
        player.SetActive(true);
        player.transform.position = new Vector3(0f, 0f); //Set player to the center of the screen

        BigUFO.SetActive(false);
        SmallUFO.SetActive(false);

        spawnAsteroids(); //Spawn 4 asteroids at begining

        MedAsteroids.inactiveAll();
        SmallAsteroids.inactiveAll();

        Invoke("spawnUFO", 15f);
    }

    //Remove everything from screen (READY SCREEN)
    public void clearScreen()
    {

        helpScreen.SetActive(false);
        CancelInvoke(); //To not spawn UFO

        player.GetComponent<PlayerCollision>().cancelRespawn(); //Not respawn player accidentally
        player.SetActive(false);
        BigUFO.SetActive(false);
        SmallUFO.SetActive(false);
        BigAsteroids.inactiveAll();
        MedAsteroids.inactiveAll();
        SmallAsteroids.inactiveAll();

    }

    //Spawn a new UFO on Screen
    public void spawnUFO()
    {

        //IF it's not active on stage
        if (!SmallUFO.activeInHierarchy && !BigUFO.activeInHierarchy)
        {
            //random numbers
            sbyte rnd = 0;
            sbyte side = (sbyte)(Random.Range(-5, 5) < 0 ? 1 : -1); // Can be -1 or 1

            //If Score lower than 40,000 , show both UFO's, if score is Higher than 40,000 , show only the small ones
            if (Score.score < 40000)
            {
                rnd = (sbyte)Random.Range(-5, 5);
            }
            else
            {
                rnd = 5; // positive number to spawn only Small UFO's
            }

            
            if (rnd <= 0)
            {

                BigUFO.SetActive(true);

                float randY = Random.Range(-Boundaries.halfY, Boundaries.halfY); //Random Y position to spawn

                BigUFO.transform.position = new Vector3(Boundaries.halfX * side, randY); // From the left or the right ?

                BigUFO.GetComponent<UFOBehaviour>().direction = (sbyte)(side * -1); // where it spawns, define direction to go

            }
            else
            {

                SmallUFO.SetActive(true);

                float randY = Random.Range(-Boundaries.halfY, Boundaries.halfY); //Random Y position to spawn

                SmallUFO.transform.position = new Vector3(Boundaries.halfX * side, randY); // From the left or the right ?

                SmallUFO.GetComponent<UFOBehaviour>().direction = (sbyte)(side * -1); // where it spawns, define direction to go
            }
        
            Invoke("spawnUFO", 15f); // Respawn the next in 15seconds
        } else
        {

            Invoke("spawnUFO", 5f); //If have some active on Stage, try again in 5 seconds
        }
    }


    void FixedUpdate()
    {
        //If it's in gameplay
        if(GameStates.actualGameState == GameState.Gameplay)
        {
            //If do not have any asteroids
            if(!BigAsteroids.haveActives() &&
                !MedAsteroids.haveActives() &&
                !SmallAsteroids.haveActives() &&
                !changingLevel)
            {
                gameplayManager.stopAudio(); //Stop audio background
                changingLevel = true; //Changing level
                Invoke("nextLevel", 2f); //Change level in 2 seconds
            }
        }    
    }

    //Define next level
    void nextLevel()
    {
        GameStates.level++;

        gameplayManager.startAudioAgain(); //Play audio from beginning

        int lengthNecessary = 4 + (2 * GameStates.level); //How many Asteroids?

        //If it's necessary to add more Asteroids to Pooling System
        if(BigAsteroids.getLength() < lengthNecessary)
        {
            BigAsteroids.addToList(2); // 2 Big
            MedAsteroids.addToList(4); // 4 Medium
            SmallAsteroids.addToList(8); // 8 Small
        }

        spawnAsteroids(); // Spawn

        changingLevel = false; // Ok, changed level
    }

    //Spawn Asteroids on screen
    void spawnAsteroids()
    {
        int lengthSpawn = 4 + (2 * GameStates.level); //define quantity

        //call every Asteroid
        foreach(GameObject asteroid in BigAsteroids.getList())
        {
            //which side it will spawn?
            byte rndX = (byte)Random.Range(-1, 1);
            byte rndY = (byte)Random.Range(-1, 1);

            if (rndX == 0 && rndY == 0) { rndX = 1; } //Do not spawn on center

            asteroid.transform.position = new Vector3(Boundaries.halfX * rndX, Boundaries.halfY * rndY); //Asteroid new position
        }

        BigAsteroids.activeNumber(lengthSpawn); //Activate it
    }

}
