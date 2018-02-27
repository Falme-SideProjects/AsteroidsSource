using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour {

    //Which direction it's going
    [SerializeField]
    private Transform destiny;

    // Velocity
    [SerializeField]
    private float speed;

    void Update () {
        //Movementation
        transform.position = Vector3.MoveTowards(transform.position, destiny.position, speed * Time.deltaTime);
    }
    
}
