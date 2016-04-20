using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    //rayPoints
    [SerializeField]
    private GameObject rayPointLeft, rayPointRight, bottomRayPoint, middleRayPoint, topRayPoint;


    private Vector3 velocity;
    public float jumpSpeed;
    public float speed= 1000;
    public float maxSpeed = 10;
    public float maxAirSpeed = 10;
    public float maxJumpSpeed;

    private bool isTurnedRight;
    private float scaleFactorX, scaleFactorY;
    private Rigidbody2D body;
	// Use this for initialization
	void Awake () {
        scaleFactorX = transform.localScale.x;
        scaleFactorY = transform.localScale.y;
        jumpSpeed = 10000;
        speed = 500;
        maxSpeed = 12f;
        maxAirSpeed = 10f;
        maxJumpSpeed = 28f;
        body = this.GetComponent<Rigidbody2D>();
        SetRayPoints();
	}
	
	// Update is called once per frame
	void Update () {
        velocity = Vector3.zero;
        
        if (Input.GetKey(KeyCode.LeftArrow) && !MidAirCollision())
        {
            velocity.x += Vector3.left.x * speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !MidAirCollision())
        {
            velocity.x += Vector3.right.x * speed;
        }
        if (onGround() && Input.GetKeyDown(KeyCode.Space))
        { //consider jump timer
            velocity = Vector3.up * jumpSpeed;
        }
        Move();

        Vector3 v = body.velocity;

        if(body.velocity.x < -0.01)
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
        if (Mathf.Abs(v.x)> maxSpeed && onGround())
        {
            Vector2 temp = v;
            temp.x = Mathf.Sign(temp.x) * maxSpeed;
            v = temp;
            body.velocity = v;
        }

        if(Mathf.Abs(v.x)> maxAirSpeed && !onGround())
        {
            Vector2 temp = v;
            temp.x = Mathf.Sign(temp.x) * maxAirSpeed;
            v = temp;
            body.velocity = v;
        }
        if(Mathf.Abs(v.y)> maxJumpSpeed)
        {
            Vector2 temp = v;
            temp.y = Mathf.Sign(temp.y) * maxJumpSpeed;
            v = temp;
            body.velocity = v;
        }

        


	}

    private bool onGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPointLeft.transform.position, Vector2.down, 0.1f);// last parameter is distance until you are on the ground
        if(hit.collider != null && hit.collider.CompareTag("Ground"))
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
                if(hit1.collider != null && hit1.collider.CompareTag("Ground"))
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
        this.rayPointLeft.transform.position = new Vector2(transform.position.x-0.5f*scaleFactorX, transform.position.y-1f*scaleFactorY);//first in placement(Outermost left point), y is the bottom of the player 
        this.rayPointRight.transform.position = new Vector2(transform.position.x+0.5f*scaleFactorX, transform.position.y-1f*scaleFactorY);//x is outermost right point, y is the bottom of the player
    }
    private void Move()
    {
        body.AddForce(velocity * Time.deltaTime, ForceMode2D.Force);
    }
}
