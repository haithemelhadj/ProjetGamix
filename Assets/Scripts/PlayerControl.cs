using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform; // Reference to the main camera
    public float mouseSensitivity = 100f; // Mouse sensitivity for camera rotation
    public float cameraDistance = 5f; // Distance from player to camera
    public float cameraHeight = 2f; // Height offset of camera above player
    public float minPitch = -30f; // Minimum camera pitch angle
    public float maxPitch = 60f; // Maximum camera pitch angle

    private Vector2 direction;
    private Animator animator;
    private float cameraYaw = 0f;
    private float cameraPitch = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        // Ensure camera is not a child of the player
        if (cameraTransform.parent == transform)
        {
            cameraTransform.SetParent(null);
        }
        // Initialize camera rotation
        cameraYaw = transform.eulerAngles.y;
        cameraPitch = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player movement
        Vector3 movement = new Vector3(direction.x, 0f, direction.y);
        if (movement != Vector3.zero)
        {
            // Align player rotation with camera's forward direction (projected on XZ plane)
            Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
            transform.rotation = Quaternion.LookRotation(cameraForward);
            // Move player in the direction relative to camera
            movement = cameraTransform.TransformDirection(movement);
            movement.y = 0f; // Keep movement on XZ plane
            movement = movement.normalized;
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Update animator with movement speed
        float moveSpeed = movement.magnitude;
        animator.SetFloat("speed", moveSpeed);

        // Update camera position and rotation
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        // Get mouse input
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;
        cameraYaw += mouseInput.x;
        cameraPitch -= mouseInput.y;
        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);

        // Apply rotation to camera
        cameraTransform.rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);

        // Position camera behind player
        Vector3 offset = -cameraTransform.forward * cameraDistance + Vector3.up * cameraHeight;
        cameraTransform.position = transform.position + offset;
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnJump()
    {
        animator.SetTrigger("isJumping");
    }
}