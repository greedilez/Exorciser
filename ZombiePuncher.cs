using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombiePuncher : MonoBehaviour
{
    private Animator _animator;

    private bool _playerHasBeenKilled = false;

    public bool PlayerHasBeenKilled{ get => _playerHasBeenKilled; }

    private void Awake() {
        _animator = GetComponent<Animator>();        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!_playerHasBeenKilled) {
                other.GetComponentInChildren<PlayerDead>().GameOver();
                VisualizePunching();
                _playerHasBeenKilled = true;
            }
        }
    }

    private void VisualizePunching() => _animator.SetTrigger("Attack");
}
