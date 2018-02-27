using UnityEngine;

public class PlayerMovementation : MonoBehaviour {

    [SerializeField]
    private float velocityRotation;

    //private float forceInertia = 0f;

    private Rigidbody2D rb2d;
    
    //Desaceleration force
    private float DSpeed = 0.995f;

    [SerializeField]
    private float limitAccel = 0f;

    // Fire Behind Player
    [SerializeField]
    private GameObject fireThrust;

    //You cant move? paused?
    private bool cantMove = false;

    private Vector2 lastVelocity;

    // Use this for initialization
    void Start () {

        rb2d = GetComponent<Rigidbody2D>();           

    }

    #region Delegates
    private void enableMovement()
    {
        disableMovement();
        InputManager.inputTurnRightDelegate += rotateShipRight;
        InputManager.inputTurnLeftDelegate += rotateShipLeft;
        InputManager.inputThrustDelegate += thrustShip;
    }


    private void disableMovement()
    {
        InputManager.inputTurnRightDelegate -= rotateShipRight;
        InputManager.inputTurnLeftDelegate -= rotateShipLeft;
        InputManager.inputThrustDelegate -= thrustShip;
    }
    #endregion

    // Update is called once per frame
    void Update () {

        if (cantMove != GameStates.paused)
        {
            cantMove = GameStates.paused;

            if (!cantMove) enableMovement();
            else disableMovement();
        }


        //Verify Fire Thrust
        if (Mathf.Abs(rb2d.velocity.x) > Mathf.Abs(lastVelocity.x) ||
            Mathf.Abs(rb2d.velocity.y) > Mathf.Abs(lastVelocity.y))
        {
            if (!fireThrust.activeInHierarchy)
                fireThrust.SetActive(true);
        }
        else
        {

            if (fireThrust.activeInHierarchy)
                fireThrust.SetActive(false);
        }

        //Deaccelerate a little
        shipInertia();

        //Save last data
        lastVelocity = rb2d.velocity;
    }

    private void OnEnable()
    {
        enableMovement();
    }

    private void OnDisable()
    {
        disableMovement();
    }

    private void rotateShipLeft()
    {
        transform.Rotate(0f, 0f, velocityRotation);
    }

    private void rotateShipRight()
    {
        transform.Rotate(0f, 0f, -velocityRotation);
    }

    private void thrustShip()
    {
        rb2d.AddForce(transform.up * 5f);



        //Limits of velocity
        if (Mathf.Abs(rb2d.velocity.y) > limitAccel)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, limitAccel * (rb2d.velocity.y < 0 ? -1 : 1));
        }
        
        if (Mathf.Abs(rb2d.velocity.x) > limitAccel)
        {
            rb2d.velocity = new Vector2(limitAccel * (rb2d.velocity.x < 0 ? -1:1), rb2d.velocity.y);
        }

    }

    // Deacelleration of the ship Player
    private void shipInertia()
    {
        rb2d.velocity *= DSpeed;
    }
}
