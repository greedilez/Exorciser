using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;

    private bool _isPlayerDead = false;

    public bool IsPlayerDead{ get => _isPlayerDead; }

    [SerializeField] private AudioSource _hitSource;

    [SerializeField] private float _hitSoundDelay = 1f;

    public UnityEvent OnPlayerDead;

    public void GameOver() {
        if (!_isPlayerDead) {
            _cameraAnimator.SetTrigger("Falling");
            StartCoroutine(HitSoundDelay());
            if (OnPlayerDead != null) OnPlayerDead.Invoke();
            _isPlayerDead = true;
        }
    }

    private IEnumerator HitSoundDelay() {
        yield return new WaitForSeconds(_hitSoundDelay); _hitSource.Play();
    }

    public void RestartGameAfter(float time) => StartCoroutine(RestartDelay(time));

    public IEnumerator RestartDelay(float time) {
        yield return new WaitForSeconds(time); SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
