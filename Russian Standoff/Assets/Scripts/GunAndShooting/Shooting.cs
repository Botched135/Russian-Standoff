using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace RussianStandOff
{
    public class Shooting : MonoBehaviour
    {
        private LineRenderer lrender;
        public GameObject arm;
        private Player player;
        public int _score;
        public ChamberCode _chamber;
        public Camera mainCamera;
        private bool emptyChamber;
        private Rigidbody2D body;
        [SerializeField]
        private float knockbackFactor = 10;
        private float lastCast = 0, coolDown = 1;
        private float angle;
        void Awake()
        {
            lrender = GetComponent<LineRenderer>();
            body = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
            _chamber = gameObject.AddComponent<ChamberCode>();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
       void Update()
        {


            Vector3 vectorToTarget = new Vector3((float)Input.GetAxis("Xbox" + player.playerIndex + "_X_Axis_Right"),
                                                        -(float)Input.GetAxis("Xbox" + player.playerIndex + "_Y_Axis_Right"));
            if (player.isTurnedRight)
                angle = Mathf.Atan2(vectorToTarget.x, -vectorToTarget.y) * Mathf.Rad2Deg;
            else
                angle = Mathf.Atan2(-vectorToTarget.x, -vectorToTarget.y) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            arm.transform.rotation = q; //works!
           /* Debug.DrawRay(transform.position+ new Vector3((float)Input.GetAxis("Xbox" +player.playerIndex  + "_X_Axis_Right"),
                                                        -(float)Input.GetAxis("Xbox" + player.playerIndex + "_Y_Axis_Right")).normalized/1.75f, 
                                              new Vector2((float)Input.GetAxis("Xbox" + player.playerIndex + "_X_Axis_Right"), 
                                                        -(float)Input.GetAxis("Xbox" + player.playerIndex + "_Y_Axis_Right")), Color.red);*/
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"),
                                                                                     -Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")).normalized/1.75f, 
                                                                          new Vector2(Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"), 
                                                                                     -Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")));

                body.AddForce(-new Vector2((float)Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"), 
                                          -(float)Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")).normalized*knockbackFactor, 
                                          ForceMode2D.Impulse);

                //hit.point
                //need start position as well
                CreateBulletTrail(transform.position + new Vector3(Input.GetAxis("Xbox" + source.playerIndex + "_X_Axis_Right"),
                                                                  -Input.GetAxis("Xbox" + source.playerIndex + "_Y_Axis_Right")).normalized / 1.75f,
                                  hit.point,1);//should be based on distance
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

       public IEnumerator CreateBulletTrail(Vector3 start, Vector3 end, int iterations)
        {
            lrender.SetPosition(0, start);
            lrender.SetPosition(1, end);

            yield return new WaitForSeconds(iterations);
            lrender.SetPosition(0, end);
        }

    }
}
