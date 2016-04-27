using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class ChamberCode : MonoBehaviour
    {
        
        private int chamberSize;
        private int shotsLeft;
        private bool[] shotChamber;

        public bool isReloading;

        void Start()
        {
            this.chamberSize = 6;
            this.shotsLeft = this.chamberSize;
            this.shotChamber = FeelingLuckyPunk();
            this.isReloading = false;

        }
    
            
        
       public ChamberCode(int _chamberSize, float _reloadTime)
        {
            this.chamberSize = _chamberSize;
            this.shotsLeft = this.chamberSize;
            this.shotChamber = FeelingLuckyPunk();
            this.isReloading = false;
        
        }

        public bool Shoot(Player source)
        {
            Debug.Log(shotsLeft);
            //If the chamber isn't empty and you try to shoot, you'll shoot or shoot a blank depending on shotPercentage
            if (shotsLeft > 0)
            {

                if (shotChamber[this.chamberSize - shotsLeft])
                {
                    //FIRE!
                    shotsLeft--;
                    int bangbang = Random.Range(1, 2);
                    if (bangbang == 1)
                    {
                        AudioSource.PlayClipAtPoint(source.GunShot01, source.transform.position);
                        return true;
                    }
                    else if (bangbang == 2)
                    {
                        AudioSource.PlayClipAtPoint(source.GunShot02, source.transform.position);
                        return true;
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
                Debug.Log(source.name);
                float v = 2.5f;
                StartCoroutine(reload(source, v));
                return false;
                }
            
        }
        public void reload()
        {
            this.shotsLeft = chamberSize;
            this.shotChamber = FeelingLuckyPunk();
        }
        public IEnumerator reload(Player source, float reloadTime)
        {
            this.isReloading = true;
          
            AudioSource.PlayClipAtPoint(source.Reload01, source.transform.position);
            yield return new WaitForSeconds(reloadTime);
            this.shotsLeft = chamberSize;
            this.shotChamber = FeelingLuckyPunk();
            string printing = "";
            foreach (bool chamber in this.shotChamber)
            {
                printing += " " + chamber;
            }
            Debug.Log(printing);
            this.isReloading = false;

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
