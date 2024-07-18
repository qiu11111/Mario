using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        
    }
    private void Update()
    {
        player = PlayerManager.instance.player;
        transform.position = new Vector3(player.transform.position.x, 5.48f, -10);
    }
}
