using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    public TextFade logo, endText;
    public GameObject creditText;
    AudioSource audio;
    public AudioClip music;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(IntroScene());
    }

    IEnumerator IntroScene()
    {
        endText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        endText.Fade(true);
        yield return new WaitForSeconds(1f);
        audio.clip = music;
        audio.loop = true;
        audio.Play();
        logo.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        creditText.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        logo.Fade(true);
        yield return new WaitForSeconds(35f);
        LoadGame();
    }

    public void LoadGame()
    {
        foreach (var item in FindObjectsOfType<GameObject>())
        {
            if (item == gameObject) continue;
            if (item.name == "EventSystem") continue;
            Destroy(item);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
