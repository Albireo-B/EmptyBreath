using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinScreen : MonoBehaviour
{

    public PlayerMovement player;
    public void Setup(int score)
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
