using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
