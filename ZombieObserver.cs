using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieMotor), typeof(Animator))]
public class ZombieObserver : MonoBehaviour
{
    private bool _isRunningToPlayer = false;

    public bool IsRunningToPlayer{ get => _isRunningToPlayer; }

    [SerializeField] private float _rayMaxDistance = 10;

    [SerializeField] private float _maxHearDistance = 10;

    private GameObject _targetPlayer = null;

    public GameObject TargetPlayer{ get => _targetPlayer; }

    private ZombieMotor _zombieMotor;

    private Animator _animator;

    private void Awake() {
        _zombieMotor = GetComponent<ZombieMotor>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        WatchForPlayer();
        ReturnToNormalStateOnPlayerRunAway();
        AdjustAnimatorForZombieState();
    }

    private void WatchForPlayer() {
        RaycastHit hit;
        Debug.DrawRay(transform.position + (transform.up * 2), transform.forward * _rayMaxDistance, Color.red);
        if(Physics.Raycast(transform.position + (transform.up * 2), transform.forward, out hit, _rayMaxDistance)) {
            if(hit.collider.tag == "Player") {
                if (!_isRunningToPlayer) {
                    Debug.Log($"{gameObject.name} has seen player!");

                    _targetPlayer = hit.collider.gameObject;
                    _isRunningToPlayer = true;
                }
            }
        }
    }

    private void ReturnToNormalStateOnPlayerRunAway() {
        if(_targetPlayer != null) {
            if(Vector3.Distance(transform.position, _targetPlayer.transform.position) >= _maxHearDistance) {
                if (_isRunningToPlayer) {
                    Debug.Log("Player runned away!");
                    _zombieMotor.SendZombieToRandomPosition();
                    _targetPlayer = null;
                    _isRunningToPlayer = false;
                }
            }
        }
    }

    private void AdjustAnimatorForZombieState() => _animator.SetBool("IsRunning", _isRunningToPlayer);
}
