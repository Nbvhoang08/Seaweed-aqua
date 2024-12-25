using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fail : CanvasUI
{
    public void RetryBtn()
    {
        Time.timeScale = 1;
        StartCoroutine(ReLoad());
        SoundManager.Instance.PlayClickSound();
    }
    IEnumerator ReLoad()
    {
        yield return new WaitForSeconds(0.3f);
        ReloadCurrentScene();
    }
    public void ReloadCurrentScene()
    {
        // Lấy tên của scene hiện tại 
        string currentSceneName = SceneManager.GetActiveScene().name;
        //Tải lại scene hiện tại
        SceneManager.LoadScene(currentSceneName);
        UIManager.Instance.CloseUIDirectly<Fail>();
    }
}
