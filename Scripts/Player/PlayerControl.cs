using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    PhotonView view;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();    
    }


    public Vector2 moveVector;
    public float speed = 3;



    void Walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));

    }

    public bool faceRight = true;

    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    public float jumpForce = 7f;
    private bool jumpControl;
    private int jumpIteration = 0;
    public int jumpValueIteration = 60;


    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.AddForce(Vector2.up * jumpForce);
            if (onGround) { jumpControl = true; }
        }
        else { jumpControl = false; }

        if (jumpControl)
        {
            if(jumpIteration++ < jumpValueIteration)
            {
                rb.AddForce(Vector2.up * jumpForce / jumpIteration);
            }
        }
        else {jumpIteration = 0;}
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;

    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", onGround);
    }
}
