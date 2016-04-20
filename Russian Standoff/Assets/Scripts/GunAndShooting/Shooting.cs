using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
    GameObject playerRef; //for later stage with multiplayer
    private bool emptyChamber;
    private float lastCast = 0, coolDown = 1;

    public bool Shoot()
    {
        if(lastCast + coolDown <= Time.time)
        {
            lastCast = Time.time;
            //check chamber
            if (emptyChamber)//chamber does not contain bullet 
            {
                //increment the chambers
                return false;
            }
            //Cast ray, for shot, increment chamber 
            return true;
        }
        return false;
    }
	
}
