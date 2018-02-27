using System.Collections.Generic;
using UnityEngine;

public class SafeZoneCollider : MonoBehaviour {

    //It's safe to respawn?
    public bool isSafe = true;

    //Hashes to save which item is on the safezone
    public List<string> hashs;

	// Use this for initialization
	void Start () {
        hashs = new List<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
        //Verify if it's safe
        if(!(hashs.Count > 0))
        {
            isSafe = true;
        } else
        {
            isSafe = false;
        }


	}

    //Collisions Add a new item inside the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hashs.Contains(collision.GetHashCode().ToString()))
        {
            hashs.Add(collision.GetHashCode().ToString());
        }
    }

    //Collisions Remove an item inside the list
    private void OnTriggerExit2D(Collider2D collision)
    {

        hashs.Remove(collision.GetHashCode().ToString());

    }
}
