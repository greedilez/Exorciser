using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{
    public UnityEvent OnTimeUp;

    [SerializeField] private int _minutesLeft, _secondsLeft;

    public const float SecondDuration = 1;

    [SerializeField] private TMP_Text _timeText;

    [SerializeField] private ZombiePuncher _zombiePuncher;

    private void Start() => StartCoroutine(TimeCount());

    private IEnumerator TimeCount() {
        yield return new WaitForSeconds(SecondDuration);
        {
            if (!_zombiePuncher.PlayerHasBeenKilled) {
                if (_secondsLeft <= 0) {
                    _minutesLeft--;
                    _secondsLeft = 59;
                }
                else _secondsLeft--;
                StartCoroutine(TimeCount());

                if (_minutesLeft < 0) {
                    _minutesLeft = 0;
                    _secondsLeft = 0;
                    StopAllCoroutines();
                    if (OnTimeUp != null) OnTimeUp.Invoke();
                }
                UpdateText();
            }
        }
    }

    public void UpdateText() {
        if(_secondsLeft >= 10) _timeText.text = $"Time Left - {_minutesLeft}:{_secondsLeft}";
        else _timeText.text = $"Time Left - {_minutesLeft}:0{_secondsLeft}";
    }

    public void LoadWinningScene() => StartCoroutine(SceneLoadDelay());

    public IEnumerator SceneLoadDelay() {
        float loadingDelay = 2f;
        yield return new WaitForSeconds(loadingDelay); SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
