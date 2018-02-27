using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    
    //Item to be added to Pooling system
    [SerializeField]
    private GameObject poolItem;

    //Quantity to add
    [SerializeField]
    private short InitialQuantity;

    //Itens saved to a list
    private List<GameObject> items;


    void Awake() {

        items = new List<GameObject>();
        addToList(InitialQuantity);

    }
    
    // Verify if have available item to be called
    public bool haveItem()
    {
        foreach (GameObject item in items)
        {
            if (!item.activeInHierarchy)
                return true;
        }
        return false;
    }

    //Get an item to be used
    public GameObject getItem()
    {
        foreach(GameObject item in items)
        {
            if(!item.activeInHierarchy)
                return item;
        }
        return items[0];
    }

    //Return list of itens in pool
    public List<GameObject> getList()
    {
        return items;
    }

    //How many items are here?
    public int getLength()
    {
        return items.Count;
    }

    //inactive all, clear
    public void inactiveAll()
    {
        if (items != null)
        {
            foreach (GameObject item in items)
            {
                if (item.activeInHierarchy)
                    item.SetActive(false);
            }
        }
    }

    //active all, show everything
    public void activeAll()
    {
        if(items != null)
        {
            foreach (GameObject item in items)
            {
                if (!item.activeInHierarchy)
                    item.SetActive(true);
            }

        }
    }


    //Active a especific number of items
    public void activeNumber(int number)
    {
        if (items != null)
        {
            for(int a=0; a<number; a++)
            {
                if (!items[a].activeInHierarchy)
                    items[a].SetActive(true);
            }

        }
    }

    //Add a new item to the list
    public void addToList(int number)
    {
        for (int a = 0; a < number; a++)
        {
            GameObject item = (GameObject)Instantiate(poolItem);
            item.transform.parent = transform;
            item.SetActive(false);
            items.Add(item);
        }
    }

    //If have any of the items active on screen
    public bool haveActives()
    {
        if (items != null)
        {
            foreach (GameObject item in items)
            {
                if (item.activeInHierarchy)
                    return true;
            }

        }

        return false;
    }

}
