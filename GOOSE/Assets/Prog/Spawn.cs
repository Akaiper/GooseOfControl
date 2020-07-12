using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject pato;
    public GameObject controlePato;

    private Vector2 min_pos;
    private Vector2 max_pos;

    private void Start()
    {
        min_pos = transform.GetChild(0).position;
        max_pos = transform.GetChild(1).position;
    }


    IEnumerator SpawnPato()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 new_pos = Vector3.zero;

        new_pos.x = Random.Range(min_pos.x, max_pos.x); 
        new_pos.y = Random.Range(min_pos.y, max_pos.y);

        GameObject.Instantiate(controlePato, new_pos, Quaternion.identity);
    }

    public void SpawnControlePato()
    {
        Vector3 new_pos = Vector3.zero;

        new_pos.x = Random.Range(min_pos.x, max_pos.x);
        new_pos.y = Random.Range(min_pos.y, max_pos.y);

        GameObject.Instantiate(pato, new_pos, Quaternion.identity);

        StartCoroutine(SpawnPato());
    }

}
