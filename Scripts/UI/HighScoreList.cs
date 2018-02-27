using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreList : MonoBehaviour {

    //Items and the list of highscores in Gameover
    [SerializeField]
    private GameObject itemList, list;

    private RectTransform rect;

    //Items to show
    private List<HighScoreItemData> itemsData;

    //Load File just once
    private bool loaded = false;

    public void refreshScores()
    {

        if (!loaded)
            loadInfo();

        //Write on UI
        for(int a=0; a<10; a++)
        {
            itemsData[a].name.text = Score.highscores[a].name;
            itemsData[a].value.text = "" + Score.highscores[a].value;
        }
    }

    private void loadInfo()
    {
        itemsData = new List<HighScoreItemData>();
        rect = GetComponent<RectTransform>();
        float h = rect.rect.height;

        //Divide by height of parent the items (each score)
        for (int a = 0; a < 10; a++)
        {
            GameObject item = (GameObject)Instantiate(itemList, list.transform);
            item.GetComponent<LayoutElement>().preferredHeight = h / 10;
            itemsData.Add(item.GetComponent<HighScoreItemData>());
        }

        loaded = true;

    }
    

}
