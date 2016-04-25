using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class Shooting : MonoBehaviour
    {
        GameObject playerRef; //for later stage with multiplayer

        public int _score;
        public ChamberCode _chamber;
        public Camera mainCamera;
        private bool emptyChamber;
        private Rigidbody2D body;
        private float knockbackFactor = 10;
        private float lastCast = 0, coolDown = 1;
        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            _chamber = gameObject.AddComponent<ChamberCode>();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
       void Update()
        {
            Debug.DrawRay(transform.position + (mainCamera.ScreenToWorldPoint(Input.mousePosition).normalized),
                                                            mainCamera.ScreenToWorldPoint(Input.mousePosition), Color.red);
        }
        public bool Shoot(Player source)
        {
            if (lastCast + coolDown <= Time.time)
            {

                lastCast = Time.time;
                
                if (!_chamber.Shoot(source))//chamber does not contain bullet 
                {
                   
                    return false;
                }
                //Consinder circle collider
                RaycastHit2D hit = Physics2D.Raycast(transform.position + (mainCamera.ScreenToWorldPoint(Input.mousePosition).normalized),
                                                            mainCamera.ScreenToWorldPoint(Input.mousePosition)); //needs update, basically completly fucked up
                body.AddForce(-(mainCamera.ScreenToWorldPoint(Input.mousePosition).normalized)*knockbackFactor, ForceMode2D.Impulse); //Addforce in opposite direction of the gun shot
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {               
                    hit.collider.gameObject.GetComponent<Player>().Death(gameObject);
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
