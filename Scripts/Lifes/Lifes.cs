using UnityEngine;
using UnityEngine.UI;

public class Lifes : MonoBehaviour {

    //Number for Player
    public static int lives;

    //UIText
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }
   
	// Update is called once per frame
	void Update () {

        //Refresh
        if (text.text != ""+lives)
        {
            text.text = "" + lives;
        }
	
    }
}
