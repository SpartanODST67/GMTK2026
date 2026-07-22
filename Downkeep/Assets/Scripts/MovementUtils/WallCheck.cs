using System;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] PlayerPuppet player;
    [SerializeField] bool isRight;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(isRight) 
            player.canMoveRight = false;
        else
            player.canMoveLeft = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(isRight) 
            player.canMoveRight = true;
        else
            player.canMoveLeft = true;
    }
}
