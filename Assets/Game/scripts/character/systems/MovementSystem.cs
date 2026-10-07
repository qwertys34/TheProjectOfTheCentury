using Game.scripts;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementSystem : MonoBehaviour
{
    public bool IsMoving => Inputs.InputReader.Direction.magnitude != 0;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 1.5f;

    private CharacterController _controller;
    private Vector3 _velocity; // только вертикальная скорость
    private Transform _cameraTransform;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _cameraTransform = Camera.main!.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 input = Inputs.InputReader.Direction;

        // Движение относительно камеры (чтобы W шло "вперёд" по взгляду)
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * input.z + right * input.x;

        bool isGrounded = _controller.isGrounded;

        if (isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        if (isGrounded && Inputs.InputReader.ConsumeAction(InputActionType.Jump))
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        _velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = moveDir * (speed * Time.deltaTime) + Vector3.up * (_velocity.y * Time.deltaTime);
        _controller.Move(finalMove);
    }
}