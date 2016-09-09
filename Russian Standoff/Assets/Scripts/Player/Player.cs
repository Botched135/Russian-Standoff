using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace RussianStandOff
{
    public class Player : MonoBehaviour
    {
        public int playerNum;
        public PlayerIndex playerIndex;
        //rayPoint
        [SerializeField]
        private GameObject rayPointLeft, rayPointRight, bottomRayPoint, middleRayPoint, topRayPoint;
        [SerializeField]
        private bool controllerActive;
        Camera mainCamera;

        public GameObject arm;

        private Vector3 velocity;
        public float jumpSpeed, speed, maxSpeed, maxAirSpeed, maxJumpSpeed;

        public bool isTurnedRight;
        private float scaleFactorX, scaleFactorY;
        private Rigidbody2D body;

        private Shooting fireGun;
        // Use this for initialization

        //Sound effects
        public AudioClip click01;
        public AudioClip click02;
        public AudioClip click03;
        public AudioClip GunShot01;
        public AudioClip GunShot02;
        public AudioClip Reload01;

        void Start()
        {
            controllerActive = true;
            playerIndex = (PlayerIndex)playerNum;
            fireGun = GetComponent<Shooting>();
            scaleFactorX = transform.localScale.x;
            scaleFactorY = transform.localScale.y;
            jumpSpeed = 300;
            speed = 2.5f;
            maxAirSpeed = 2.25f;
            maxJumpSpeed = 50f;
            body = this.GetComponent<Rigidbody2D>();
            SetRayPoints();

           

        }
        // Update is called once per frame
        void Update()   
        {
            if (onGround() && (Input.GetButtonDown("Xbox" + playerIndex + "_AButton") || Input.GetKeyDown(KeyCode.Space)))
            { //consider jump timer
                body.AddForce(new Vector2(0,Vector3.up.y * jumpSpeed)); //not predictable
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Xbox" + playerIndex + "_RightTrigger") == 1)
                {
                    fireGun.Shoot(this);
                }
                if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Xbox"+playerIndex+"_XButton"))
                {
                    StartCoroutine(fireGun._chamber.reload(this, 2.5f));
                }
        }
        void FixedUpdate()
        {
            if (onGround())
            body.velocity = new Vector2(0, body.velocity.y); //cannot be constantly as they will tend to ignore gravity, works fine atm but might need some tweeking

            if (!fireGun._chamber.isReloading)
            {

                if (controllerActive)
                {
                    if (Mathf.Abs(Input.GetAxis("Xbox" + playerIndex + "_X_Axis_Left")) > 0.26f && !MidAirCollision())
                    {
                        body.velocity = new Vector2(Input.GetAxis("Xbox" + playerIndex + "_X_Axis_Left") * speed, body.velocity.y);
                    }
                }
                else if (!controllerActive)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) && !MidAirCollision())
                    {
                        body.velocity= new Vector2(-speed, body.velocity.y);
                    }
                    if (Input.GetKey(KeyCode.RightArrow) && !MidAirCollision())
                    {
                        body.velocity = new Vector2(speed, body.velocity.y);
                    }
                }
               

                Vector3 v = body.velocity;

                if (v.x < -0.01)
                {
                    Vector3 temp = transform.localScale;
                    temp.x = Mathf.Abs(temp.x) * -1;
                    transform.localScale = temp;
                    isTurnedRight = false;
                }
                else if (v.x > 0.01)
                {
                    Vector3 temp = transform.localScale;
                    temp.x = Mathf.Abs(temp.x);
                    transform.localScale = temp;
                    isTurnedRight = true;
                }
              /*  if (Mathf.Abs(v.x) > maxSpeed && onGround())// try to modify
                {
                    Vector2 temp = v;
                    temp.x = Mathf.Sign(temp.x) * maxSpeed;
                    v = temp;
                    body.velocity = v;
                }*/

                if (Mathf.Abs(v.x) > maxAirSpeed && !onGround())
                {
                    Vector2 temp = v;
                    temp.x = Mathf.Sign(temp.x) * maxAirSpeed;
                    v = temp;
                    body.velocity = v;
                }
                if (Mathf.Abs(v.y) > maxJumpSpeed)
                {
                    Vector2 temp = v;
                    temp.y = Mathf.Sign(temp.y) * maxJumpSpeed;
                    v = temp;
                    body.velocity = v;
                }
            }


        }

        private bool onGround()
        {
            
            RaycastHit2D hit = Physics2D.Raycast(rayPointLeft.transform.position, Vector2.down, 0.1f);// last parameter is distance until you are on the ground
            if (hit.collider != null && hit.collider.CompareTag("Ground"))
                return true;

            RaycastHit2D hit2 = Physics2D.Raycast(rayPointRight.transform.position, Vector2.down, 0.1f);
            if (hit2.collider != null && hit2.collider.CompareTag("Ground"))
                return true;

            return false;
        }
        private bool MidAirCollision()
        {
            if (!onGround())
            {
                if (isTurnedRight)
                {
                    RaycastHit2D hit1 = Physics2D.Raycast(topRayPoint.transform.position, Vector2.right, 2f);
                    if (hit1.collider != null && hit1.collider.CompareTag("Ground"))
                        return true;

                    RaycastHit2D hit2 = Physics2D.Raycast(middleRayPoint.transform.position, Vector2.right, 2f);
                    if (hit2.collider != null && hit2.collider.CompareTag("Ground"))
                        return true;

                    RaycastHit2D hit3 = Physics2D.Raycast(bottomRayPoint.transform.position, Vector2.right, 2f);
                    if (hit3.collider != null && hit3.collider.CompareTag("Ground"))
                        return true;
                }
                if (!isTurnedRight)
                {
                    RaycastHit2D hit1 = Physics2D.Raycast(topRayPoint.transform.position, Vector2.left, 2f);
                    if (hit1.collider != null && hit1.collider.CompareTag("Ground"))
                        return true;

                    RaycastHit2D hit2 = Physics2D.Raycast(middleRayPoint.transform.position, Vector2.left, 2f);
                    if (hit2.collider != null && hit2.collider.CompareTag("Ground"))
                        return true;

                    RaycastHit2D hit3 = Physics2D.Raycast(bottomRayPoint.transform.position, Vector2.left, 2f);
                    if (hit3.collider != null && hit3.collider.CompareTag("Ground"))
                        return true;
                }
            }
            return false;
        }
        private void SetRayPoints()
        {
            this.rayPointLeft.transform.position = new Vector2(transform.position.x - 0.75f * scaleFactorX, transform.position.y - 2.25f * scaleFactorY);//first in placement(Outermost left point), y is the bottom of the player 
            this.rayPointRight.transform.position = new Vector2(transform.position.x + 0.75f * scaleFactorX, transform.position.y - 2.25f * scaleFactorY);//x is outermost right point, y is the bottom of the player
        }

        public void Death(GameObject killer)
        {
            //Add death animation
            killer.GetComponent<Shooting>()._score++;
            fireGun._chamber.reload();
            //Here the player should be KILLED rather than DESTROYED and moved to respawn location or smt
            GameManager.GM.RespawnPlayer(this, 2);
        }
    }
}
