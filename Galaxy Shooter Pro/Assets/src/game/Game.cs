﻿using UnityEngine;
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
    private bool isPaused = false;
    private int difficulty = 0;
    private bool isEscapeDown = false;
    SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
      this.spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
      this.ResetLives();
      this.ResetScore();
      this.isGameOver = false;
      this.isPaused = false;
      this.GetComponentInChildren<UI>().HideGameOver();
      this.GetComponentInChildren<UI>().HidePauseMenu();
    }

    public void Update()
    {
      if (Input.GetKey(KeyCode.R) && this.isGameOver)
      {
        this.restartGame();
      }

      if (Input.GetKey(KeyCode.Escape) && this.isGameOver)
      {
        this.ReturnToMainMenu();
      }

      if (Input.GetKey(KeyCode.Escape) && !this.isGameOver && !this.isPaused && !this.isEscapeDown)
      {
        this.pauseGameAndShowMenu();
      } else if (Input.GetKey(KeyCode.Escape) && !this.isGameOver && this.isPaused && !this.isEscapeDown)
      {
        this.ResumeGame();
      }
      this.isEscapeDown = (Input.GetKey(KeyCode.Escape));
    }

    private void restartGame()
    {
      SceneManager.LoadScene(Constants.GAME_SCENE_INDEX);
    }

    public void ReturnToMainMenu()
    {
      SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_INDEX);
    }

    private void pauseGameAndShowMenu()
    {
      this.isPaused = true;
      Time.timeScale = 0f;
      this.GetComponentInChildren<UI>().ShowPauseMenu();
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
      this.setDifficulty(0);
    }
    public void IncreaseScore(int points)
    {
      this.score += points;
      this.GetComponentInChildren<UI>().UpdateScore(this.score);
      if ((this.score >= 200) && (this.difficulty == 0)) {
        this.setDifficulty(1);
      }
      if ((this.score >= 400) && (this.difficulty == 1))
      {
        this.setDifficulty(2);
      }
      if ((this.score >= 600) && (this.difficulty == 2))
      {
        this.setDifficulty(3);
      }
      if ((this.score >= 2000) && (this.difficulty == 3))
      {
        this.setDifficulty(4);
      }
    }

    private void setDifficulty(int difficulty)
    {
      this.difficulty = difficulty;
      this.spawnManager.SetDifficulty(difficulty);
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
        this.lives = 0;
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

    public int GetLives()
    {
      return this.lives;
    }

    public void ExitToTitleScreen()
    {
      Time.timeScale = 1f;
      this.ReturnToMainMenu();
    }

    public void ResumeGame()
    {
      this.isPaused = false;
      this.GetComponentInChildren<UI>().HidePauseMenu();
      Time.timeScale = 1f;
    }
  }
}
