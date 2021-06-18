using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace vio.spaceshooter.game
{
  public class UI : MonoBehaviour
  {
    private const string SCORE_LABEL = "Score: ";
    private bool isShowingGameOver = false;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Text gameoverLabel;

    [SerializeField]
    private Text restartGameInstructionsLabel;

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

    public void HideGameOver()
    {
      this.isShowingGameOver = false;
      this.gameoverLabel.enabled = false;
      this.restartGameInstructionsLabel.enabled = false;
    }

    public void ShowGameOver()
    {
      this.isShowingGameOver = true;
      this.gameoverLabel.enabled = true;
      this.restartGameInstructionsLabel.enabled = true;
      this.StartCoroutine(AnimateGameOver());
    }

    IEnumerator AnimateGameOver()
    {
      while (this.isShowingGameOver)
      {
        yield return new WaitForSeconds(0.5f);
        this.toggleShowHideGameOver();
      }
      this.gameoverLabel.enabled = false;
    }

    private void toggleShowHideGameOver()
    {
      this.gameoverLabel.enabled = !this.gameoverLabel.enabled;
    }
  }
}
