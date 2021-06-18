using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vio.spaceshooter.player;

public class Game : MonoBehaviour
{
  
  private int score = 0;
  private int lives = 0;
  
  
  // Start is called before the first frame update
  void Start()
    {
    this.ResetLives();
    this.ResetScore();
    }

  public void ResetLives()
  {
    this.lives = 3;
  }

  // Update is called once per frame
  void Update()
    {
    }

  public void ResetScore()
  {
    this.score = 0;
    this.GetComponentInChildren<UI>().UpdateScore(this.score);
  }
  public void IncreaseScore(int points)
  {
    this.score += points;
    this.GetComponentInChildren<UI>().UpdateScore(this.score);
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
      this.GetComponentInChildren<Player>().KillPlayer();
    }
    this.GetComponentInChildren<UI>().UpdateLives(this.lives);
  }
}
