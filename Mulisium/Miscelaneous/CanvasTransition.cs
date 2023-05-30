using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasTransition : MonoBehaviour
{
    public CanvasGroup transitionImage;
    public float transitionTime = 1f;

    private void Awake()
    {
        StartFadeIn();
    }
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    //To transparent
    IEnumerator FadeIn()
    {
        float timeElapsed = 0f;

        Debug.Log(transitionImage.alpha);
        while (timeElapsed < transitionTime)
        {
            //transitionImage.alpha += Mathf.Min(transitionImage.alpha + transitionTime * Time.deltaTime, 1f);
            transitionImage.alpha = Mathf.Lerp(transitionImage.alpha, 0f, timeElapsed);
            timeElapsed += Time.deltaTime;
            Debug.Log(transitionImage.alpha);

            yield return null;
        }

    }

    //To black
    IEnumerator FadeOut()
    {
        float timeElapsed = 0f;

        while (timeElapsed < transitionTime)
        {
            //transitionImage.alpha += Mathf.Min(transitionImage.alpha + transitionTime * Time.deltaTime, 1f);
            transitionImage.alpha = Mathf.Lerp(transitionImage.alpha, 1f, timeElapsed);
            timeElapsed += Time.deltaTime;


            yield return null;
        }
        int scene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(scene==0? 1 : 0);
    }
}
