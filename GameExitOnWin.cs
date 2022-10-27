using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitOnWin : MonoBehaviour
{
    [SerializeField] private float _gameExitDelay = 20f;

    private void Awake() => StartCoroutine(GameExitDelay());

    private IEnumerator GameExitDelay() {
        yield return new WaitForSeconds(_gameExitDelay); Application.Quit();
    }
}
