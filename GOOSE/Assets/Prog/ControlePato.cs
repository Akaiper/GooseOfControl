using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePato : MonoBehaviour
{
    private Pato pato;
    private Mov aaa;
    private bool pode;

    private void Start()
    {
        pode = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pode)
        {
            if (collision.gameObject.tag == "AAA")
            {
                aaa = collision.gameObject.GetComponent<Mov>();
                pato = GameObject.FindGameObjectWithTag("Pato").GetComponent<Pato>();

                aaa.peraPato = true;
                pato.input = aaa.input;
                pato.aaa = aaa;
                pato.controle = true;
                pato.podeExplodi = true;

                pode = false;

                pato.fill.fillAmount = 1;

                Destroy(this.gameObject);
            }
        }
       
    }
}
