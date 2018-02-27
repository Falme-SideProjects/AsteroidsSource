using UnityEngine;

public class UFOBehaviour : MonoBehaviour {

    //Velocity o movement
    [SerializeField]
    private float velocity;

    //Direction it's going (1 Right, -1 Left)
    public sbyte direction = 1;

    //After some time, it goes up
    private bool goingUp = false;


	
	void Update () {

        //Goes up in half velocity
        if (goingUp)
        {
            transform.position += Vector3.up * Time.deltaTime * (velocity/2);
        }

        //Define Direction it's going
        if(direction == 1)
            transform.position += transform.right * Time.deltaTime * velocity;
        else
            transform.position -= transform.right * Time.deltaTime * velocity;

        //If reached other side, disable
        if (transform.position.x > Boundaries.halfX || transform.position.x < -Boundaries.halfX)
        {
            gameObject.SetActive(false);
        }
    }

    private void activateGoUp()
    {
        goingUp = true;
    }

    private void OnEnable()
    {
        Invoke("activateGoUp", 3f); // After 3 seconds go Up
    }

    private void OnDisable()
    {
        goingUp = false;
        CancelInvoke();
    }
}
