using UnityEngine;
using System.Collections;

public class Chamber : MonoBehaviour {

	// Use this for initialization
	void Start () {

        int chamberSize = 6;
      //  bool chamberEmpty = false;
        int[] percentage = { 100, 50, 33, 25, 20, 16};
        

	}
	
	// Update is called once per frame
	void Update () {

        int shotPercentage = percentage[chamberSize];
	
    bool shoot (int chamberSize){
            

            //If the chamber is empty and you try to shoot, you'll reload automatically
            if (chamberSize == 0 ){
                reload(chamberSize);
                return ;
            }

            //If the chamber isn't empty and you try to shoot, you'll shoot or shoot a blank depending on shotPercentage
            if (chamberSize > 0){
                chamberSize = chamberSize - 1;
                Random rnd = new Random();
                int number = rnd.Next(1, 100);
                if(number > shotPercentage) {
                    //Blank shot
                    return false;
                }
                else if (number <= shotPercentage){
                    //FIRE!
                    return true;
                }

            }

        }

     void reload (int playerChamber)
        chamberSize = 6;
	}
}
