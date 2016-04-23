using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class ChamberCode : MonoBehaviour
    {

        private int chamberSize;
        private int shotsLeft;
        private bool[] shotChamber;
        /*
                //Sound effects
                public AudioClip click01;
                public AudioClip click02;
                public AudioClip click03;
                public AudioClip GunShot01;
                public AudioClip GunShot02;
                public AudioClip Reload01;
        */

        
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

        public bool Shoot(Player source)
        {
            Debug.Log(shotsLeft);
            //If the chamber isn't empty and you try to shoot, you'll shoot or shoot a blank depending on shotPercentage
            if (shotsLeft > 0)
            {

                float number = Random.value;
                if (shotChamber[this.chamberSize - shotsLeft] == true)
                {
                    //FIRE!
                    shotsLeft--;
                    int bangbang = Random.Range(1, 2);
                    if (bangbang == 1)
                    {

                        AudioSource.PlayClipAtPoint(source.GunShot01, source.transform.position);
                    }
                    else if (bangbang == 2)
                    {
                        AudioSource.PlayClipAtPoint(source.GunShot02, source.transform.position);
                    }
                        return true;
                    }
                    else
                    {
                        //*CLICK!*
                        shotsLeft--;
                        int clickclick = Random.Range(1, 3);
                        if (clickclick == 1)
                        {
                            AudioSource.PlayClipAtPoint(source.click01, source.transform.position);
                        }
                        else if (clickclick == 2)
                        {
                            AudioSource.PlayClipAtPoint(source.click02, source.transform.position);
                        }
                        else if (clickclick == 3)
                        {
                            AudioSource.PlayClipAtPoint(source.click03, source.transform.position);
                        }
                        return false;

                    }

                }
                else
                {
                    reload(source);
                    return false;
                }
            
        }

        public void reload(Player source)
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

            AudioSource.PlayClipAtPoint(source.Reload01, source.transform.position);

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
