using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


public class Mov : MonoBehaviour
{

    public Inputschema input;
    public float vel;
    private Rigidbody2D rb;
    private Vector2 moveVec;

    private float moveLimiter = 0.7f;

    public bool peraPato;

    public bool buum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        peraPato = false;

        buum = false;
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
}
