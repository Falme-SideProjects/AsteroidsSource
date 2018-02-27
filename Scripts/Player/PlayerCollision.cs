using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerCollision : MonoBehaviour {

    //Get GameState Class
    [SerializeField]
    private GameStates gameStates;

    //Safezone when respawn
    [SerializeField]
    private SafeZoneCollider safeZoneCollider;

    //Clip Death
    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "BigAsteroid" || collision.tag == "MedAsteroid")
        {
            gameObject.SetActive(false);
            collision.GetComponent<AsteroidCollision>().collided();
            collision.gameObject.SetActive(false);

            dead();
        }

        if (collision.tag == "SmallAsteroid" || collision.tag == "BigUFO" || collision.tag == "SmallUFO")
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);

            dead();
        }

        //Get Points
        switch (collision.tag)
        {
            case "BigAsteroid":
                Score.setScore(ScoreScale.BigAsteroid);
            break;
            case "MedAsteroid":
                Score.setScore(ScoreScale.MedAsteroid);
                break;
            case "SmallAsteroid":
                Score.setScore(ScoreScale.SmallAsteroid);
                break;
            case "BigUFO":
                Score.setScore(ScoreScale.BigUFO);
                break;
            case "SmallUFO":
                Score.setScore(ScoreScale.SmallUFO);
                break;
        }
    }

    //Death behaviour
    public void dead()
    {
        //Lost life
        Lifes.lives--;
        playSound(); //Play desth sound

        //If Respawn or Game over
        if(Lifes.lives > 0)
        {
            Invoke("respawn", 2f);
        } else
        {
            CancelInvoke();
            Invoke("gameover", 2f);
        }
    }

    private void playSound()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    public void cancelRespawn()
    {
        CancelInvoke();
    }

    public void respawn()
    {
        //If it's safe to respawn, respawn
        if (safeZoneCollider.isSafe)
        {
            transform.position = new Vector3(0f, 0f);
            gameObject.SetActive(true);
        } else
        {
            Invoke("respawn", 0.2f); //Try again in 0.2 seconds
        }
    }

    //Gameover behaviour
    public void gameover()
    {
        //You have a highscore?
        if(Score.score > Score.highscores[9].value)
            gameStates.changeState(GameState.SetHighScore); //Go set your name
        else
            gameStates.changeState(GameState.GameOver); //Gameover
    }


}
