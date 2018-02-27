using UnityEngine;
using UnityEngine.UI;

public class ItemPauseMenuBehaviour : MonoBehaviour {

    [SerializeField]
    private Text text;

    //Pause item is active
    public bool active = false;

    //If changed item state (select or mouse over)
    private bool lastState = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(lastState != active)
        {
            lastState = active;

            if (active) moreVisible();
            else lessVisible();
        }

        if (active) moreVisible();

	}

    //Text more visible
    public void moreVisible()
    {
        text.color = new Color(1f, 1f, 1f, 1f);
    }

    //Text less visible
    public void lessVisible()
    {
        text.color = new Color(1f, 1f, 1f, 0.6f);
    }
}
