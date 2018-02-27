using UnityEngine;

public class AsteroidRandomizeDirection : MonoBehaviour {

    //Pivot to rotate and Define Direction
    [SerializeField]
    private Transform pivot;

	// Use this for initialization
	void Start () {
        //Start with random Direction
        randomizeRotation();
	}

    public void randomizeRotation()
    {
        float rnd = Random.Range(0f, 359f); // Angles between 0º - 359º
        pivot.Rotate(Vector3.forward * rnd); // Rotate Pivot
    }
}
