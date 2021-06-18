using UnityEngine;
using UnityEngine.SceneManagement;

namespace vio.spaceshooter.mainmenu
{
  public class MainMenu : MonoBehaviour
  {
    public void NewGame()
    {
      SceneManager.LoadScene(Constants.GAME_SCENE_INDEX);
    }
    public void QuitGame()
    {
      Debug.Log("Application quiting...");
      Application.Quit();
    }
  }
}

