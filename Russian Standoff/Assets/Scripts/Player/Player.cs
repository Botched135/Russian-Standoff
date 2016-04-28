using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace RussianStandOff
{
    public class Player : MonoBehaviour
    {
        public int playerNum;
        public PlayerIndex playerIndex;
        //rayPoints
        [SerializeField]
        private GameObject rayPointLeft, rayPointRight, bottomRayPoint, middleRayPoint, topRayPoint;
        Camera mainCamera;

        private Vector3 velocity;
        public float jumpSpeed;
        public float speed = 1000;
        public float maxSpeed = 10;
        public float maxAirSpeed = 10;
        public float maxJumpSpeed;

        private bool isTurnedRight;
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
            playerIndex = (PlayerIndex)playerNum;
            fireGun = GetComponent<Shooting>();
            scaleFactorX = transform.localScale.x;
            scaleFactorY = transform.localScale.y;
            jumpSpeed = 12500;
            speed = 400;
            maxSpeed = 12f;
            maxAirSpeed = 10f;
            maxJumpSpeed = 28f;
            body = this.GetComponent<Rigidbody2D>();
            SetRayPoints();

           

        }
        // Update is called once per frame
        void Update()
        {
            velocity = Vector3.zero;
            if (!fireGun._chamber.isReloading)
            {


                if (Mathf.Abs(Input.GetAxis("Xbox"+playerIndex+"_X_Axis_Left"))>0.25f && !MidAirCollision())
                {
                    velocity.x = Input.GetAxis("Xbox"+playerIndex+"_X_Axis_Left")* speed;
                }
                if (onGround() && Input.GetButtonDown("Xbox"+playerIndex+"_AButton"))
                { //consider jump timer
                    velocity.y = Vector3.up.y * jumpSpeed;
                }
                Move();

                Vector3 v = body.velocity;

                if (body.velocity.x < -0.01)
                {
                    Vector3 temp = transform.localScale;
                    temp.x = Mathf.Abs(temp.x) * -1;
                    transform.localScale = temp;
                    isTurnedRight = false;
                }
                else if (body.velocity.x > 0.01)
                {
                    Vector3 temp = transform.localScale;
                    temp.x = Mathf.Abs(temp.x);
                    transform.localScale = temp;
                    isTurnedRight = true;
                }
                if (Mathf.Abs(v.x) > maxSpeed && onGround())
                {
                    Vector2 temp = v;
                    temp.x = Mathf.Sign(temp.x) * maxSpeed;
                    v = temp;
                    body.velocity = v;
                }

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


                if (Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Xbox" + playerIndex + "_RightTrigger") == 1)
                {
                    fireGun.Shoot(this);
                }
                if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Xbox"+playerIndex+"_XButton"))
                {
                    StartCoroutine(fireGun._chamber.reload(this, 2.5f));
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
        private void Move()
        {
            body.AddForce(velocity * Time.deltaTime, ForceMode2D.Force);
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
