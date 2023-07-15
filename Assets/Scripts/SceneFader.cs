using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image FadeImage;
    [Range(0.1f, float.MaxValue)]
    public float Seconds = 1f;
    public AnimationCurve AnimationCurve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    void Update()
    {

    }

    public void FadeTo(int buildIndex) => FadeTo(SceneManager.GetSceneAt(buildIndex).name);

    public void FadeTo(string scene)
    {
        Time.timeScale = 1;
        StartCoroutine(FadeOut(scene));
    }


    IEnumerator FadeIn()
    {
        float t = 1f;
        if (Seconds == 0f)
            Seconds = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime / Seconds;
            float a = AnimationCurve.Evaluate(t);
            FadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        if (Seconds == 0f)
            Seconds = 1f;

        while (t < 1f)
        {
            t += Time.deltaTime / Seconds;
            float a = AnimationCurve.Evaluate(t);
            FadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

}
