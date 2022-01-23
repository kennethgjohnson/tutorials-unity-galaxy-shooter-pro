using UnityEngine;
using UnityEngine.SceneManagement;
using vio.spaceshooter.game.player;
using vio.spaceshooter.game.spawnmanager;
using vio.spaceshooter;
namespace vio.spaceshooter.game
{
  public class Game : MonoBehaviour
  {
    private int score = 0;
    private int lives = 0;
    private bool isGameOver = false;

    SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
      this.spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
      this.ResetLives();
      this.ResetScore();
      this.isGameOver = false;
      this.GetComponentInChildren<UI>().HideGameOver();
    }

    public void Update()
    {
      if (Input.GetKey(KeyCode.R) && this.isGameOver)
      {
        this.restartGame();
      }

      if (Input.GetKey(KeyCode.Escape) && this.isGameOver)
      {
        this.returnToMainMenu();
      }
    }

    private void restartGame()
    {
      SceneManager.LoadScene(Constants.GAME_SCENE_INDEX);
      //Code below replaced with complete scene reload
      /*
      this.ResetLives();
      this.ResetScore();
      this.isGameOver = false;
      this.GetComponentInChildren<UI>().HideGameOver();
      this.GetComponentInChildren<Player>(true).ResetPlayer();
      this.spawnManager.Reset();*/

    }

    public void returnToMainMenu()
    {
      SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_INDEX);
    }

    public void ResetLives()
    {
      this.lives = 3;
      this.GetComponentInChildren<UI>().UpdateLives(this.lives);
    }
    public void ResetScore()
    {
      this.score = 0;
      this.GetComponentInChildren<UI>().UpdateScore(this.score);
      this.spawnManager.SetDifficulty(0);
    }
    public void IncreaseScore(int points)
    {
      this.score += points;
      this.GetComponentInChildren<UI>().UpdateScore(this.score);
      if (this.score == 200) {
        this.spawnManager.SetDifficulty(1);
      }
      if (this.score == 400)
      {
        this.spawnManager.SetDifficulty(2);
      }
      if (this.score == 600)
      {
        this.spawnManager.SetDifficulty(3);
      }
    }

    public int GetScore()
    {
      return this.score;
    }

    public void LoseLife()
    {
      this.lives--;
      if (this.lives <= 0)
      {
        this.gameOver();
      }
      this.GetComponentInChildren<UI>().UpdateLives(this.lives);
    }

    private void gameOver()
    {
      this.isGameOver = true;
      this.spawnManager.stopSpawning();
      this.GetComponentInChildren<Player>().KillPlayer();
      this.GetComponentInChildren<UI>().ShowGameOver();
    }
  }
}
