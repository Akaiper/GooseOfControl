using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Animator animator;
    public bool podeExplodi;

    private GameObject aaa_fudido;

    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controle = false;
        tempo = maxtempo;
        animator = GetComponent<Animator>();
        podeExplodi = false;

        fill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(controle)
        {
            moveVec.x = Input.GetAxis(input.horizontal_axis);
            moveVec.y = Input.GetAxis(input.vertical_axis);

            tempo -= Time.deltaTime;
            fill.fillAmount = tempo / maxtempo;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(tempo <= 0 )
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
        else
        {
            rb.velocity = Vector2.zero;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(podeExplodi)
        {
            if(collision.gameObject.tag == "AAA")
            {
                fill.fillAmount = 0;
                animator.SetBool("BUUM", true);

                aaa_fudido = collision.gameObject;

                podeExplodi = false;

                controle = false;
            }
        }
    }


    public void BUUM()
    {
        

        Vector2 dir = Vector2.zero;

        dir = aaa_fudido.transform.position - transform.position;

        dir = dir.normalized;

        aaa_fudido.GetComponent<Mov>().VoarPraLonge(dir);
        Destroy(this.gameObject);
    }
}
