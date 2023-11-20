using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Transform player;
    [SerializeField]
    float movementSpeed = 0.01f;
    private void Update()
    {
        if (gameManager.GetState() == GameManager.State.InDungeon)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.position += new Vector3(0, 0, 1 * movementSpeed + Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.position += new Vector3(-1 * movementSpeed + Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.position += new Vector3(0, 0, -1 * movementSpeed + Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.position += new Vector3(1 * movementSpeed + Time.deltaTime, 0, 0);
            }
        }
    }
}
