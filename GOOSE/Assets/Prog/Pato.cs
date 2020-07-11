using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pato : MonoBehaviour
{
    public Inputschema input;
    public float vel;
    private Rigidbody2D rb;
    private Vector2 moveVec;

    private float moveLimiter = 0.7f;

    public bool controle;
    private float tempo;
    public float maxtempo;

    public Mov aaa;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controle = false;
        tempo = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(controle)
        {
            moveVec.x = Input.GetAxis(input.horizontal_axis);
            moveVec.y = Input.GetAxis(input.vertical_axis);

            tempo += Time.deltaTime;
        }

        if(tempo > maxtempo)
        {
            controle = false;

            aaa.peraPato = false;
        }
        


    }

    private void FixedUpdate()
    {
        if (controle)
        {
            if (moveVec.x != 0 && moveVec.y != 0)
            {

                moveVec.x *= moveLimiter;
                moveVec.y *= moveLimiter;
            }

            rb.velocity = moveVec * vel;
        }
       

        
    }
}
