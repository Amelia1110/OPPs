using UnityEngine;
using Alteruna;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Alteruna.Avatar player;
    [SerializeField] private float movementSpeed;
    // CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2DSynchronizable rb;
    private bool isColliding = false;
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
        // if (Input.GetKey(KeyCode.W))
        // {
        //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.up);
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.down);
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.left);
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.right);
        // }
    }
    void FixedUpdate()
    {
        if (!player.IsMe)
            return;
        // if (Input.GetKey(KeyCode.W))
        // {
        //     rb.MovePosition(rb.position + Time.fixedDeltaTime * new Vector2(0, movementSpeed));
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     rb.MovePosition(rb.position + Time.fixedDeltaTime * new Vector2(0, -1 * movementSpeed));
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     rb.MovePosition(rb.position + Time.fixedDeltaTime * new Vector2(-1 * movementSpeed, 0));
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     rb.MovePosition(rb.position + Time.fixedDeltaTime * new Vector2(movementSpeed, 0));

        // }
        rb.MovePosition(rb.position + new Vector2(Input.GetAxis("Horizontal") * movementSpeed / 10, Input.GetAxis("Vertical") * movementSpeed / 10));
    }
}


