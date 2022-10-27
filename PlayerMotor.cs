using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    public Rigidbody _rigidbody{ get; private set; }

    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private float _rotationSpeed = 5f;

    [SerializeField] private PlayerDead _playerDead;

    private void Awake() => Init();

    private void Init() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() => MovePlayer();

    private void Update() => RotatePlayer();

    private void MovePlayer() {
        if (!_playerDead.IsPlayerDead) {
            _rigidbody.AddForce(transform.forward * _moveSpeed * Input.GetAxis("Vertical"), ForceMode.Impulse);
            _rigidbody.AddForce(transform.right * _moveSpeed * Input.GetAxis("Horizontal"), ForceMode.Impulse);
        }
    }

    private void RotatePlayer() {
        if (!_playerDead.IsPlayerDead) {
            transform.Rotate(new Vector3(0, _rotationSpeed * Input.GetAxis("Mouse X"), 0), Space.Self);
        }
    }
}
