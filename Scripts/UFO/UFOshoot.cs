using UnityEngine;

enum TypeUFO { Big, Small }

public class UFOshoot : MonoBehaviour {

    // If it's small or big
    [SerializeField]
    private TypeUFO typeUFO;

    [SerializeField]
    private Transform pivot, aim, player;

    [SerializeField]
    private PoolManager poolManager;
    
    // Frequency of shooting 
    private float fireTime = 1f;

    private void shoot()
    {
        if(typeUFO == TypeUFO.Big)
        {
            float rnd = Random.Range(0f, 359f); //Shoot randomly, everywhere
            pivot.Rotate(Vector3.forward * rnd);
            if (poolManager.haveItem())
            {
                GameObject bullet = poolManager.getItem();
                bullet.SetActive(true);
                bullet.transform.position = aim.position;
                bullet.transform.rotation = pivot.rotation;
                bullet.transform.Rotate(0f, 0f, -90f);
            }
        } else
        {
            pivot.right = player.position - pivot.position; //Shoot at the layer position...
            float rnd = Random.Range(-25f, 25f);
            pivot.Rotate(Vector3.forward * rnd); // ... But not perfect
            if (poolManager.haveItem())
            {
                GameObject bullet = poolManager.getItem();
                bullet.SetActive(true);
                bullet.transform.position = aim.position;
                bullet.transform.rotation = pivot.rotation;
                bullet.transform.Rotate(0f, 0f, -90f);
            }
        }

        Invoke("shoot", fireTime); //Shoot again
    }


    private void OnEnable()
    {
        Invoke("shoot", fireTime); //Start shooting
    }

    private void OnDisable()
    {
        CancelInvoke(); //Stop shooting
    }
}
