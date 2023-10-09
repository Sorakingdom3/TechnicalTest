using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private float _progress = 0f;

    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _progressText;

    private void Awake()
    {
        ChangeScene(FlowManager.instance.GetCurrentScene());
    }

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);

        op.allowSceneActivation = false;

        while (_progress < 1f)
        {
            _progress = Mathf.Clamp01(op.progress * 100f);
            _progressBar.value = _progress;

            _progressText.text = $"Loading... {_progress * 100f}%";

            yield return null;
        }

        op.allowSceneActivation = true;

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }
}
