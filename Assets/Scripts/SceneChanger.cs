using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using LitMotion;
using Unity.Collections;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    [SerializeField] string SceneName;
    [SerializeField] float fadetime;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Fade().Forget();
    }

    async UniTask Fade()
    {
        await LMotion.Create(0f, 1f, fadetime)
        .Bind(x =>
        {
            FadeImage.color = new Color(0f, 0f, 0f, x);
        })
        .ToUniTask();
        SceneManager.LoadScene(SceneName);
        await LMotion.Create(1f, 0f, fadetime)
        .Bind(x =>
        {
            FadeImage.color = new Color(0f, 0f, 0f, x);
        })
        .ToUniTask();
        Destroy(this.gameObject);
    }
}
