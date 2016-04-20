using UnityEngine;
using System.Collections;


public class ChamberCode : MonoBehaviour {

    public int chamberSize;
    public float[] percentage = { 1.0F, 0.5F, 0.33F, 0.25F, 0.15F, 0.10F };
    public bool shotsFired;
    public float shotPercentage;

    // Use this for initialization
    void Start() {
        chamberSize = 6;
        shotsFired = false;
      
    }

	// Update is called once per frame
	void Update () {

        shotPercentage = percentage[chamberSize - 1];
        if (shotsFired == true) {
            shotPercentage = 0;
        }
	}

    public bool Shoot(int chamberSize)
    {

        //If the chamber is empty and you try to shoot, you'll reload automatically
        if (chamberSize == 0)
        {
            reload(chamberSize);
            shotsFired = false;
            return shotsFired;
        }

        //If the chamber isn't empty and you try to shoot, you'll shoot or shoot a blank depending on shotPercentage
        if (chamberSize > 0)
        {
            chamberSize = chamberSize - 1;
           
            float number = Random.value;
            if (number > shotPercentage)
            {
                //Blank shot
                shotsFired = false;
                return shotsFired;
            }
            else if (number <= shotPercentage)
            {
                //FIRE!
                shotsFired = true;
                return shotsFired;
            }
        }
        return shotsFired;
    }

    public void reload(int playerChamber)
    {
        playerChamber = 6;
        shotsFired = false;

    }


}
