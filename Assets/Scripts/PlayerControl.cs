using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = speed * Time.deltaTime * new Vector3(direction.x, 0, direction.y);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
}
