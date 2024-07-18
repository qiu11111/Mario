using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obj1Controller : MonoBehaviour
{
    public GameObject Tile;
    private Animator anim;
    private bool isUsed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("used", false);
    }
    public void use()
    {
        if (!isUsed)
        {
            isUsed = true;
            anim.SetBool("used", true);
            GameObject.Instantiate(Tile, new Vector2(transform.position.x,transform.position.y+1.0f), Quaternion.identity);
        }
    }
    

}
