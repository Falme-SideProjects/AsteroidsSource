using UnityEngine;

enum LethalFor { Player, UFO }

public class BulletCollision : MonoBehaviour {

    //Who the bullet can kill (UFO or Player)
    [SerializeField]
    private LethalFor lethalFor;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //If big or medium asteroid
        if (collision.tag == "BigAsteroid" || collision.tag == "MedAsteroid")
        {
            gameObject.SetActive(false); //Deactivate this bullet
            collision.GetComponent<AsteroidCollision>().collided(); //Call Collided
            collision.GetComponent<AsteroidCollision>().playSound(); //Play a sound
            collision.gameObject.SetActive(false); //Asteroid disable
        }

        //If it's small asteroid
        if (collision.tag == "SmallAsteroid")
        {
            gameObject.SetActive(false); //Deactivate this bullet
            collision.GetComponent<AsteroidCollision>().playSound(); //Play a sound
            collision.gameObject.SetActive(false); //Asteroid disable
        }

        //If can kill player
        if (lethalFor == LethalFor.Player)
        {
            if (collision.tag == "Player")
            {
                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);

                //If component exist
                if (collision.GetComponent<PlayerCollision>())
                {
                    collision.GetComponent<PlayerCollision>().dead(); //Call player dead
                }

            }
        }
        else
        {
            //If collided with a UFO
            if (collision.tag == "BigUFO" || collision.tag == "SmallUFO")
            {
                gameObject.SetActive(false); //Deactivate this bullet
                collision.GetComponent<UFOCollision>().playSound(); //Play a sound
                collision.gameObject.SetActive(false); //UFO disable
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
    }
}
