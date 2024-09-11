using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float ySpeed;
    Vector3 isometricDirection;
    float magnitude;

    private Vector3 _input;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GatherInput();
        Look(); // Smoothly rotate towards movement direction (visual)
        magnitude = _input.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

    }

    private void FixedUpdate()
    {
        Move(); // Direct movement


    }

    // This function gathers player input and stores it in _input
    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    // This handles the visual rotation towards movement direction
    private void Look()
    {
        if (_input == Vector3.zero) return; // No input, no need to rotate

        var targetRotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }

    // This function handles movement
    private void Move()
    {
        // Convert the input direction to isometric direction using the helper
        isometricDirection = _input.ToIso().normalized;

        // Move the character using the isometric direction
        Vector3 moveDirection = isometricDirection * _speed * Time.deltaTime;

        // Apply the movement
        _rb.MovePosition(transform.position + moveDirection);

    }




}

public static class Helpers
{
    // Matrix to convert movement to isometric perspective
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    // Converts a standard Vector3 into isometric perspective
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}

