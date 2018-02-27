using UnityEngine;
using UnityEngine.U2D;

public enum SpriteType {
                    None,
                    Black,
                    Player,
                    UfoBig,
                    UfoSmall,
                    laserUFO,
                    laserPlayer,
                    MeteorBrownBig1,
                    MeteorBrownMed1,
                    MeteorBrownSmall1,
                    FireThrust
                }

public class AtlasManager : MonoBehaviour {

    //Sprite showing at the moment
    [SerializeField]
    private SpriteType actualSprite;

    //Last sprite it was showing
    private SpriteType lastSprite;

    //Atlas
    [SerializeField]
    private SpriteAtlas spriteAtlas;

    //SpriteRenderer of Gameobject
    private SpriteRenderer spriteRenderer;

    
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //set Last sprite to None (Had nothing before)
        lastSprite = SpriteType.None;
    }

	void Update () {
        //Verify if Sprite Changed
        verifyActualSprite();
	}

    private void verifyActualSprite()
    {
        //Sprite Changed?
        if(actualSprite != lastSprite)
        {
            //Set Sprite
            spriteRenderer.sprite = spriteAtlas.GetSprite(actualSprite.ToString());
            lastSprite = actualSprite;
        }
    }

    public void changeSprite(SpriteType spriteType)
    {
        this.actualSprite = spriteType;
    }
}
