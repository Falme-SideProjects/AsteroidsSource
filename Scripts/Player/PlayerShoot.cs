using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    //Where it shoots
    [SerializeField]
    private Transform aim;

    //List with 4 bullets allowed
    [SerializeField]
    private List<GameObject> bullets;

    //Pooling system
    [SerializeField]
    private PoolManager poolManager;

    //Can shoot? it's paused?
    private bool cantShoot = true;

    #region delegates
    private void enableShooting()
    {
        disableShooting();
        InputManager.inputFireDelegate += shoot;
    }


    private void disableShooting()
    {
        InputManager.inputFireDelegate -= shoot;
    }
    #endregion

    // Update is called once per frame
    void Update ()
    {
        //Verify pause
        if(cantShoot != GameStates.paused)
        {
            cantShoot = GameStates.paused;

            if (!cantShoot) enableShooting();
            else disableShooting();
        }
        
	}

    private void OnEnable()
    {
        enableShooting();
    }

    private void OnDisable()
    {
        disableShooting();
    }

    private void shoot()
    {
        //If bullet exist, shoot
        if (poolManager.haveItem())
        {
            GameObject bullet = poolManager.getItem();
            bullet.transform.position = aim.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
    }
}
