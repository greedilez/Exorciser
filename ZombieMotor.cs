using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator), typeof(ZombieObserver))]
public class ZombieMotor : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed, _runningSpeed, _changeRandomPositionDelay;

    private NavMeshAgent _agent;

    [SerializeField] private Vector3 _minimalRandomPositionPoint, _maximumRandomPositionPoint;

    [SerializeField] private bool _debug = false;

    private Vector3 _targetPosition;

    private ZombieObserver _zombieObserver;

    private ZombiePuncher _zombiePuncher;

    private void Awake() => Init();

    public void Init() {
        _agent = GetComponent<NavMeshAgent>();
        _zombieObserver = GetComponent<ZombieObserver>();
        _zombiePuncher = GetComponent<ZombiePuncher>();
        _agent.speed = _defaultSpeed;
        SendZombieToRandomPosition();
        StartCoroutine(ChangeTargetRandomPlace());
    }

    private IEnumerator ChangeTargetRandomPlace() {
        yield return new WaitForSeconds(_changeRandomPositionDelay);
        {
            if (!_zombieObserver.IsRunningToPlayer && !_zombiePuncher.PlayerHasBeenKilled) {
                SendZombieToRandomPosition();
            }
            StartCoroutine(ChangeTargetRandomPlace());
        }
    }

    public Vector3 RandomTargetPosition() {
        float xPosition = Random.Range(_minimalRandomPositionPoint.x, _maximumRandomPositionPoint.x);
        float zPosition = Random.Range(_minimalRandomPositionPoint.z, _maximumRandomPositionPoint.z);
        Vector3 targetPosition = new Vector3(xPosition, 0, zPosition);
        if (_debug) Debug.Log(targetPosition);
        return targetPosition;
    }

    public void SendZombieToRandomPosition() {
        _targetPosition = RandomTargetPosition();
        _agent.SetDestination(_targetPosition);
    }

    private void FixedUpdate() => RunToPlayerOnDetect();

    private void RunToPlayerOnDetect() {
        if (_zombieObserver.IsRunningToPlayer) {
            _agent.SetDestination(_zombieObserver.TargetPlayer.transform.position);
            if (_agent.speed != _runningSpeed) _agent.speed = _runningSpeed;
        }
        else if (_agent.speed == _runningSpeed) _agent.speed = _defaultSpeed;
    }
}