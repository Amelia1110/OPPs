using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Alteruna.Avatar player;
    [SerializeField] private float movementSpeed;
    CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Alteruna.Avatar>();
        
        // Don't run rest of function if player is not me
        if (!player.IsMe)
            return;

        // characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Don't run rest of function if player is not me
        if (!player.IsMe)
            return;

        // player.Move = new Vector2(Input.GetAxis("Horizontal") * speed, player.velocity.y);

        // if (Input.GetKey(KeyCode.Space)){
        //     player.velocity = new Vector2(player.velocity.x, speed);
        // }
                if (Input.GetKey(KeyCode.W))
        {
            transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.right);
        }
    }
}


