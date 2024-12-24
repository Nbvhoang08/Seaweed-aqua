using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Congrat : CanvasUI
{
     public void NextBtn()
    {
        Time.timeScale = 1;

        StartCoroutine(NextSence());
        SoundManager.Instance.PlayVFXSound(2);
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // Kiểm tra xem scene tiếp theo có tồn tại không
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }

    IEnumerator NextSence()
    {
        yield return new WaitForSeconds(0.3f);
        LoadNextScene();
        UIManager.Instance.CloseUIDirectly<Congrat>();

    }
}
