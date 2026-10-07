using Game.scripts;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private Transform playerBody;

    private float _pitch = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerBody == null)
            playerBody = transform.parent;
    }

    private void Update()
    {
        Vector2 look = Inputs.InputReader.LookDelta;

        // Горизонталь — вращаем тело игрока
        playerBody.Rotate(Vector3.up * (look.x * sensitivity));

        // Вертикаль — вращаем только камеру
        _pitch -= look.y * sensitivity;
        _pitch = Mathf.Clamp(_pitch, -maxPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}