using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadStartWindow()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("StartWindow");
    }

    public void LoadLevelWindow()
    {
        SceneManager.LoadScene("LevelWindow");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    } 
    
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    } 
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void LoadLevel6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void LoadLevel7()
    {
        SceneManager.LoadScene("Level7");
    }
    public void LoadLevel8()
    {
        SceneManager.LoadScene("Level8");
    }
    public void LoadLevel9()
    {
        SceneManager.LoadScene("Level9");
    }

   
}
