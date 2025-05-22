using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;
    public float delayBeforeAction = 0.5f;

    public void PlayGame()
    {
        StartCoroutine(PlayGameWithDelay());
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameWithDelay());
    }

    private IEnumerator PlayGameWithDelay()
    {
        if (clickSound != null) clickSound.Play();
        yield return new WaitForSeconds(delayBeforeAction);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator QuitGameWithDelay()
    {
        if (clickSound != null) clickSound.Play();
        yield return new WaitForSeconds(delayBeforeAction);
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
