using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
   [SerializeField] private PaddleController _paddleController;
   [SerializeField] private GameObject _loseWindow;
   [SerializeField] private GameObject _victoryWindow;
   [SerializeField] private GameObject _pauseWindow;
   
   public void RestartGame()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
      SceneManager.LoadScene(currentSceneIndex);
        
      Time.timeScale = 1f;
   }
   
   public void Pause()
   {
      _pauseWindow.SetActive(true);
        
      Time.timeScale = 0f;
        
      _paddleController.Joystick.gameObject.SetActive(false);
   }
   
   public void Resume()
   {
      _pauseWindow.SetActive(false);
        
      Time.timeScale = 1f;
        
      _paddleController.Joystick.gameObject.SetActive(true);
   }
   public void LoadStartWindow()
   {
      Time.timeScale = 1f;
        
      SceneManager.LoadScene("StartWindow");
   }
}
