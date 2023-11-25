using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float movespeed;
    public float JumpPower;
    public bool isLongJump = false; //낮은 점프, 높은 점프 제어
    public bool isJumping = false;
    public float jumpCount;

    Rigidbody2D rb;

    int health = 3;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 1;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * movespeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * movespeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isJumping == false) 
            { 
                isJumping = true;
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * 450f);
                isLongJump = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isLongJump = false;
        }
    }

    public void Jump()
    {
        rb.velocity = Vector2.up * JumpPower;
    }

    private void FixedUpdate()
    {
        if (isLongJump && rb.velocity.y > 0) 
        {
            rb.gravityScale = 2.0f;
        }
        else 
        {
            rb.gravityScale = 5.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Box")
        {
            Debug.Log("박스");
            if(health > 0)
            {
                health -= 1;
                HpManager.hp -= 1;

            }else if (HpManager.hp == 0)
            {
                Destroy(gameObject);
            }
        }

        if(col.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }
}
