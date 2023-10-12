using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneService : MonoBehaviour
{

    public static SceneService Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }


    public void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
       

        if(_loaderCanvas == null)
        {
            scene.allowSceneActivation = true;
            return;
        }

        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);

        do
        {
            _progressBar.fillAmount = scene.progress;
        } while (!scene.isDone);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);


    }
}
