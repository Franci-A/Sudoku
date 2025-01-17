using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void StartSudoku()
    {
        SudokuHandler.Instance.StartSudoku();
        SceneManager.UnloadSceneAsync(1);
    }
}
