using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private int _gameSceneIndex = 1;

    public void StartGame() => SceneManager.LoadScene(_gameSceneIndex);

    public void ExitFromGame() => Application.Quit();
}
