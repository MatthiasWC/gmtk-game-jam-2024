using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public bool FadeInToThisScene = true;
    public bool FadeOutOfThisScene = true;
    public CanvasGroup fadeImage;

    public float fadeTime = 1f;

    private float maxVolume;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxVolume = audioSource.volume;
        if (FadeInToThisScene) StartCoroutine(FadeInCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToScene(string sceneName)
    {
        if (FadeOutOfThisScene)
        {
            StartCoroutine(FadeOutCo(sceneName));
        } else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator FadeInCo()
    {
        fadeImage.alpha = 1;
        audioSource.volume = 0;
        while (fadeImage.alpha > 0)
        {
            fadeImage.alpha = fadeImage.alpha - Time.deltaTime / fadeTime;
            audioSource.volume = audioSource.volume + Time.deltaTime * maxVolume / fadeTime;
            yield return null;
        }
    }

    IEnumerator FadeOutCo(string sceneName)
    {
        fadeImage.alpha = 0;
        audioSource.volume = 1;
        while (fadeImage.alpha < 1)
        {
            fadeImage.alpha = fadeImage.alpha + Time.deltaTime / fadeTime;
            audioSource.volume = audioSource.volume - Time.deltaTime * maxVolume / fadeTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}
