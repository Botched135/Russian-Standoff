using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace RussianStandOff
{
    public class Shooting : MonoBehaviour
    {
        //PlayerIndex playerRef; //for later stage with multiplayer
        private Player player;
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
            player = GetComponent<Player>();
            _chamber = gameObject.AddComponent<ChamberCode>();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
       void Update()
        {
            Debug.DrawRay(transform.position+ new Vector3((float)Input.GetAxis("Xbox" +player.playerIndex  + "_X_Axis_Right"),
                                                        -(float)Input.GetAxis("Xbox" + player.playerIndex + "_Y_Axis_Right")).normalized/1.75f, 
                                              new Vector2((float)Input.GetAxis("Xbox" + player.playerIndex + "_X_Axis_Right"), 
                                                        -(float)Input.GetAxis("Xbox" + player.playerIndex + "_Y_Axis_Right")), Color.red);
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3((float)Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"),
                                                                                     -(float)Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")).normalized/1.75f, 
                                                                          new Vector2((float)Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"), 
                                                                                     -(float)Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")));

                body.AddForce(-new Vector2((float)Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"), 
                                          -(float)Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")).normalized*knockbackFactor, 
                                          ForceMode2D.Impulse);
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
