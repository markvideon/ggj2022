using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public TextFade text1, text2, text3;
    bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroScene());
    }

    IEnumerator IntroScene()
    {
        text1.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        text3.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        text1.Fade(true);
        yield return new WaitForSeconds(1f);
        text2.Fade(true);
        yield return new WaitForSeconds(1f);
        text3.Fade(true);
        yield return new WaitForSeconds(3f);
        LoadGame();
    }

    public void LoadGame()
    {
        if (!loading)
        {
            SceneManager.LoadScene("PatTest00");
            loading = true;
        }
    }
}
