using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BrickCreate : MonoBehaviour
{
    public GameObject Brick;
    public int countx;
    public int county;
    private float length;
    private float height;
    private float x;
    private float y;

    private void OnValidate()
    {
        length = Brick.GetComponent<SpriteRenderer>().bounds.size.x;
        height = Brick.GetComponent<SpriteRenderer>().bounds.size.y;
    }
#if UNITY_EDITOR
    public void tileInEditor()
    {
        for(int i = 0; i < county; i++)
        {
            for(int j = 0; j < countx; j++)
            {
                Vector2 position = new Vector2(x + j * length, y);
                GameObject brick = (GameObject)PrefabUtility.InstantiatePrefab(Brick, transform);
                brick.transform.position = position;
            }
            
        }
    }
#endif
}
