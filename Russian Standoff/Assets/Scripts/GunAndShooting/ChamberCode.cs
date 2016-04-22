using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class ChamberCode : MonoBehaviour
    {

        private int chamberSize;
        private int shotsLeft;
        private bool[] shotChamber;



        public float[] percentage = { 1.0F, 0.5F, 0.33F, 0.25F, 0.15F, 0.10F };
        public float shotPercentage;

        public ChamberCode()
        {
            this.chamberSize = 6;
            this.shotsLeft = this.chamberSize;
            this.shotChamber = FeelingLuckyPunk() ;
      


        }
       public ChamberCode(int _chamberSize)
        {
            this.chamberSize = _chamberSize;
            this.shotsLeft = this.chamberSize;
            this.shotChamber = FeelingLuckyPunk();
        }

        public bool Shoot()
        {
            Debug.Log(shotsLeft);
            //If the chamber isn't empty and you try to shoot, you'll shoot or shoot a blank depending on shotPercentage
            if (shotsLeft > 0)
            {
                
                float number = Random.value;
                if(shotChamber[this.chamberSize-shotsLeft] == true)
                {
                    //FIRE!
                    shotsLeft--;
                    return true;
                }
                else
                {
                    //*CLICK!*
                    shotsLeft--;
                    return false;
                    
                }
                
            }
            else
                {
                    reload();
                    return false;
                }
            
        }

        public void reload()
        {
            //timer obviously
            this.shotsLeft = chamberSize;
            this.shotChamber = FeelingLuckyPunk();
            string printing = "";
            foreach (bool chamber in this.shotChamber)
            {
                printing += " " + chamber;
            }
            Debug.Log(printing);

        }
        public bool[] FeelingLuckyPunk()
        {
            bool[] chambers = new bool[chamberSize];//  0.05 0.2 0.25 0.25 0.2 0.05
            for (int i = 0; i < chambers.Length; i++)
            {
                chambers[i] = false;
            }
            float RNGsus = Random.Range(0, 100);
            if (RNGsus <= 5)
            {
                chambers[0] = true;
            }
            else if (RNGsus <= 25)
            {
                chambers[1] = true;

            }
            else if (RNGsus <= 50)
            {
                chambers[2] = true;
            }
            else if (RNGsus <= 75)
            {
                chambers[3] = true;
            }
            else if (RNGsus <= 95)
            {
                chambers[4] = true;
            }
            else
            {
                chambers[5] = true;
            }
            
            return chambers;
            
        }


    }
}
