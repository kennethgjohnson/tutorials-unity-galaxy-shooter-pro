using UnityEngine.UI;
using UnityEngine;
using System;

public class UI : MonoBehaviour
{
  private const string SCORE_LABEL = "Score: ";

  [SerializeField]
  private Text scoreLabel;

  [SerializeField]
  private Sprite[] livesSprites;

  [SerializeField]
  private Image livesImage;

  public void UpdateScore(int score)
  {
    this.scoreLabel.text = SCORE_LABEL
      + score.ToString();
  }

  public void UpdateLives(int lives)
  {
    livesImage.sprite = this.livesSprites[lives];
  }
}
