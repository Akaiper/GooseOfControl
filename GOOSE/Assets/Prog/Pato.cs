using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.Mathematics;
using System;

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

    private GameObject spawn;

    private float actual_vel;

    private AudioSource quack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controle = false;
        tempo = maxtempo;
        animator = GetComponent<Animator>();
        podeExplodi = false;
        spawn = GameObject.FindGameObjectWithTag("SPAWN");

        fill.fillAmount = 0;

        quack = GetComponent<AudioSource>();
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
            if (tempo > 0)
            {
                rb.velocity = Vector2.zero;
            }
               
        }

        if(tempo <= 0 )
        {
            controle = false;

            aaa.peraPato = false;

            Follow();
        }

        actual_vel = (float)Math.Sqrt(rb.velocity.x * rb.velocity.x) + (float)Math.Sqrt(rb.velocity.y * rb.velocity.y);

       
        animator.SetFloat("Vel", actual_vel - 0.1f);

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
            if (tempo > 0)
            {
                rb.velocity = Vector2.zero;
            }
        }



    }

    private Vector2 Closer()
    {
        Vector2 closer = Vector2.zero;
        float dist_min = 100000000000;
        float aux = 0;

        GameObject[] aaas;

        aaas = GameObject.FindGameObjectsWithTag("AAA");

        foreach (GameObject player in aaas)
        {
            aux = Vector2.Distance(transform.position, player.transform.position);

            if(aux < dist_min)
            {
                dist_min = aux;
                closer = player.transform.position;
            }
        }

        return closer;
    }

    private void Follow()
    {

        Vector2 dir = Closer();

        dir = dir - (Vector2)transform.position;

        dir = dir.normalized;


        rb.velocity = dir * vel * 1.03f;
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

       

        aaa.peraPato = false;

        Vector2 dir = Vector2.zero;

        dir = aaa_fudido.transform.position - transform.position;

        dir = dir.normalized;

        aaa_fudido.GetComponent<Mov>().VoarPraLonge(dir);

        spawn.GetComponent<Spawn>().SpawnControlePato();

       

        Destroy(this.gameObject);
    }

    public void PlayBuum()
    {
        
        quack.Play();

    }
}
