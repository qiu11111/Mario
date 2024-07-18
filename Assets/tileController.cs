using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileController : MonoBehaviour
{
    private SpriteRenderer sr;
    private float timer;
    private float x;
    private float currenty;
    private float y;
    public float a;
    private bool canBeEaten;


    private void Awake()
    {
        x = transform.position.x;
        y = transform.position.y;
        currenty = y - a;

    }
    private void Update()
    {
        if (currenty < y)
        {
            currenty += 0.02f;
            transform.position = new Vector2(x, currenty);
        }
        else
        {
            canBeEaten = true;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeEaten)
        {
            GameObject.Destroy(this.gameObject);
            collision.GetComponent<Player>().grow();
        }
            
    }


}
