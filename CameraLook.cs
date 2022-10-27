using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraLook : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;

    private float _xRotation = 0;

    [SerializeField] private PlayerDead _playerDead;

    private void Update() => RotateCameraAlongX();

    private void RotateCameraAlongX() {
        if (!_playerDead.IsPlayerDead) {
            _xRotation += Input.GetAxis("Mouse Y") * _rotationSpeed;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(_xRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
