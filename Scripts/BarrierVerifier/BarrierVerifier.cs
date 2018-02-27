using UnityEngine;

public class BarrierVerifier : MonoBehaviour {

    void Awake()
    {

        //Boundaries.halfX = Camera.main.orthographicSize + 1.5f;
        //Boundaries.halfY = Screen.height / 2;
    }

    void Update () {
        
        verifyPosition();
        //Debug.Log(Screen.width);
        //Debug.Log(Camera.main.orthographicSize);
	}

    //Verify if reached bounds
    private void verifyPosition()
    {

        //Passed the bounds on X angle
        if(transform.position.x > Boundaries.halfX)
        {
            transform.position = new Vector3(-Boundaries.halfX, transform.position.y); //Go to the other side
        } else if(transform.position.x < -Boundaries.halfX)
        {
            transform.position = new Vector3(Boundaries.halfX, transform.position.y); //Go to the other side
        }

        //Passed the bounds on Y angle
        if (transform.position.y > Boundaries.halfY)
        {
            transform.position = new Vector3(transform.position.x, -Boundaries.halfY); //Go to the other side
        }
        else if (transform.position.y < -Boundaries.halfY)
        {
            transform.position = new Vector3(transform.position.x, Boundaries.halfY); //Go to the other side
        }
    }
}
