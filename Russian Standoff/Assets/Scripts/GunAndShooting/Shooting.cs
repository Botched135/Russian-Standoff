using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class Shooting : MonoBehaviour
    {
        GameObject playerRef; //for later stage with multiplayer
        public ChamberCode _chamber;
        public Camera mainCamera;
        private bool emptyChamber;
        private float lastCast = 0, coolDown = 1;
        void Awake()
        {
            _chamber = new ChamberCode();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
       
        public bool Shoot()
        {
            if (lastCast + coolDown <= Time.time)
            {

                lastCast = Time.time;
                
                if (!_chamber.Shoot())//chamber does not contain bullet 
                {
                    return false;
                }
                Debug.Log("SHOT");
                RaycastHit2D hit = Physics2D.Raycast(transform.position + (mainCamera.ScreenToWorldPoint(Input.mousePosition).normalized),
                                                            mainCamera.ScreenToWorldPoint(Input.mousePosition)); //needs update, basically completly fucked up
                if (hit.collider != null && hit.collider.CompareTag("Ground"))
                {
                    
                    Destroy(hit.collider.gameObject);
                }
                return true;
            }
            return false;
        }

        public bool CanShoot()
        {
            if (lastCast + coolDown <= Time.time)
                return true;
            return false;
        }

    }
}
