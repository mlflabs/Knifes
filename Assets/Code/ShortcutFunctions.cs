using UnityEngine;

public class ShortcutFunctions : MonoBehaviour
{
    public static ShortcutFunctions Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }



    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }

    public void BackToMain()
    {
        GameManager.Instance.BackToMainMenu();
    }
}
