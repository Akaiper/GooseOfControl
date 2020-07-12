using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class Mov : MonoBehaviour
{

    public Inputschema input;
    public float vel;
    private Rigidbody2D rb;
    private Vector2 moveVec;

    private float moveLimiter = 0.7f;

    public bool peraPato;
    public Pato pato;
    public Mov aaa;

    public bool buum;

    private bool peraColision;

    private Animator animator;

    private float actual_vel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        peraPato = false;

        buum = false;

        peraColision = true;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!buum)
        {
            if (!peraPato)
            {
                moveVec.x = Input.GetAxis(input.horizontal_axis);
                moveVec.y = Input.GetAxis(input.vertical_axis);

            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        actual_vel = (float) Math.Sqrt(rb.velocity.x * rb.velocity.x) + (float)Math.Sqrt(rb.velocity.y * rb.velocity.y);

        animator.SetBool("Controle", peraPato);
        animator.SetFloat("Vel", actual_vel - 0.1f);

    }

    private void FixedUpdate()
    {

        if(!buum)
        {
            if (!peraPato)
            {
                if (moveVec.x != 0 && moveVec.y != 0)
                {

                    moveVec.x *= moveLimiter;
                    moveVec.y *= moveLimiter;
                }

                rb.velocity = moveVec * vel  ;

            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
       



    }


    public void VoarPraLonge(Vector2 vec)
    {
        rb.freezeRotation = false;
        buum = true;
        GetComponent<BoxCollider2D>().enabled = false;
        rb.AddForce(vec * 300);
        rb.AddTorque(100);
    }

    public IEnumerator test()
    {
        yield return new WaitForSeconds(0.5f);
        peraColision = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(peraColision)
        {
            if (peraPato)
            {
                if (collision.gameObject.tag == "AAA")
                {

                    peraColision = false;

                    aaa = collision.gameObject.GetComponent<Mov>();

                    aaa.peraPato = true;
                    pato.input = aaa.input;
                    pato.aaa = aaa;
                    aaa.pato = pato;
                    aaa.peraColision = false;

                    peraPato = false;

                    StartCoroutine(aaa.test());

                    Debug.Log(input.horizontal_axis);
                }
            }
        }
        
    }
}
