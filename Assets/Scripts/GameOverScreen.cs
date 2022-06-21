using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
        public PlayerMovement player;

    public void Setup()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //player immobile
        player.isGravity = false;
        player.IsClimbing = false;
        player.IsHanging= false;
        player.IsWallRunning = false;
        player.IsImmobile = true;
    }

  
}
