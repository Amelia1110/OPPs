using UnityEngine;
// using Alteruna;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // private Alteruna.Avatar player;
    [SerializeField] private float movementSpeed;
    private Vector2 movementInput = Vector2.zero;
    // CharacterController characterController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D rb;

    private GameLogic logic;

    void Start()
    {
        // player = GetComponent<Alteruna.Avatar>();
        logic = FindFirstObjectByType<Grid>().GetComponent<GameLogic>();
        rb = GetComponent<Rigidbody2D>();
        // Don't run rest of function if player is not me
        // if (!player.IsMe)
        //     return;

        // characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputValue value){
        movementInput = value.Get<Vector2>();
    }

    public void OnAttack(){
        Debug.Log("attack");
        logic.Reveal(rb.position.x, rb.position.y);
    }

    public void OnInteract(){
        Debug.Log("poo");
        logic.ToggleFlag(rb.position.x, rb.position.y);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // Don't run rest of function if player is not me
    //     // if (!player.IsMe)
    //     //     return;

    //     // player.Move = new Vector2(Input.GetAxis("Horizontal") * speed, player.velocity.y);

    //     // if (Input.GetKey(KeyCode.Space)){
    //     //     player.velocity = new Vector2(player.velocity.x, speed);
    //     // }
    //     // if (Input.GetKey(KeyCode.W))
    //     // {
    //     //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.up);
    //     // }
    //     // if (Input.GetKey(KeyCode.S))
    //     // {
    //     //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.down);
    //     // }
    //     // if (Input.GetKey(KeyCode.A))
    //     // {
    //     //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.left);
    //     // }
    //     // if (Input.GetKey(KeyCode.D))
    //     // {
    //     //     transform.position += 2.5f * movementSpeed * Time.deltaTime * transform.TransformDirection(Vector2.right);
    //     // }
    //     if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         logic.Reveal(rb.position.x, rb.position.y);
    //     }
    //     if (Input.GetKeyUp(KeyCode.F))
    //     {
    //         logic.ToggleFlag(rb.position.x, rb.position.y);
    //     }
    // }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(movementInput.x * movementSpeed / 10, movementInput.y * movementSpeed / 10));
    }
}


