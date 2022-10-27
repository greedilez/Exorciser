using UnityEngine;
using System.Collections;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _steps; //0 - first step, 1 - second

    [SerializeField] private AudioSource _source;

    private int _stepSoundIndex = 0;

    private bool _madeStep = false;

    [SerializeField] private PlayerDead _playerDead;

    private void Update() => StartCoroutineOnStep();

    private void StartCoroutineOnStep() {
        if (!_playerDead.IsPlayerDead) {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (vertical != 0 || horizontal != 0) {
                if (!_madeStep) {
                    _source.PlayOneShot(_steps[_stepSoundIndex]);
                    StartCoroutine(NextStepUnlockDelay());
                    _stepSoundIndex = (_stepSoundIndex >= 1) ? 0 : _stepSoundIndex + 1;
                    _madeStep = true;
                }
            }
        }
    }

    private IEnumerator NextStepUnlockDelay() {
        yield return new WaitForSeconds(0.6f); if (_madeStep) _madeStep = false;
    }
}
