using UnityEngine;

namespace vio.spaceshooter.game.player
{
  public class FakePlayerBehaviourHandler
  {
    private const float TRANSITION_LEFT_X = -10f;
    private const float TRANSITION_RIGHT_X = 10f;

    private readonly GameObject fakePlayerGameObject;
    private readonly GameObject fakeShieldGameObject;
    private Animator fakePlayerAnimator;
    private Vector3 realPlayerPosition;
    public FakePlayerBehaviourHandler(GameObject fakePlayerGameObject, GameObject fakeShieldGameObject)
    {
      this.fakePlayerGameObject = fakePlayerGameObject;
      this.fakeShieldGameObject = fakeShieldGameObject;
      this.HideFakePlayer();
      this.fakePlayerAnimator = this.fakePlayerGameObject.GetComponent<Animator>();
    }

    public void HideFakePlayer()
    {
      this.fakePlayerGameObject.transform.position = new Vector3(0, 0, -50);
      this.fakeShieldGameObject.transform.position = new Vector3(0, 0, -50);
    }

    public void Update(Vector3 realPlayerPosition)
    {
      this.realPlayerPosition = realPlayerPosition;
      if (this.isPlayerMovingOverLeftWrappingThreshold())
      {
        this.showFakePlayerToRight();
      }
      else if (isPlayerMovingOverRightWrappingThreshold())
      {
        this.showFakePlayerToLeft();
      }
      else
      {
        this.HideFakePlayer();
      }
    }

    private bool isPlayerMovingOverLeftWrappingThreshold()
    {
      return this.realPlayerPosition.x <= TRANSITION_LEFT_X;
    }

    private void showFakePlayerToRight()
    {
      fakePlayerGameObject.transform.position = new Vector3(
        this.calculateFakePlayerXPositionToTheRight(),
        this.realPlayerPosition.y,
        0
      );
    }

    private float calculateFakePlayerXPositionToTheRight()
    {
      return this.realPlayerPosition.x + 21.5f;
    }

    private bool isPlayerMovingOverRightWrappingThreshold()
    {
      return this.realPlayerPosition.x >= TRANSITION_RIGHT_X;
    }

    private void showFakePlayerToLeft()
    {
      fakePlayerGameObject.transform.position = new Vector3(
        calculateFakePlayerXPositionToTheLeft(),
        this.realPlayerPosition.y,
        0
      );
    }

    private float calculateFakePlayerXPositionToTheLeft()
    {
      return this.realPlayerPosition.x - 21.5f;
    }

    public void animateNone()
    {
      fakePlayerAnimator.ResetTrigger("OnTurnRight");
      fakePlayerAnimator.ResetTrigger("OnTurnLeft");
      fakePlayerAnimator.ResetTrigger("OnNoTurning");
      fakePlayerAnimator.SetTrigger("OnNoTurning");
    }

    public void animateLeft()
    {
      fakePlayerAnimator.ResetTrigger("OnTurnRight");
      fakePlayerAnimator.ResetTrigger("OnTurnLeft");
      fakePlayerAnimator.ResetTrigger("OnNoTurning");
      fakePlayerAnimator.StopPlayback();
      fakePlayerAnimator.SetTrigger("OnTurnLeft");
    }

    public void animateRight()
    {
      fakePlayerAnimator.ResetTrigger("OnTurnRight");
      fakePlayerAnimator.ResetTrigger("OnTurnLeft");
      fakePlayerAnimator.ResetTrigger("OnNoTurning");
      fakePlayerAnimator.StopPlayback();
      fakePlayerAnimator.SetTrigger("OnTurnRight");
    }
  }

}