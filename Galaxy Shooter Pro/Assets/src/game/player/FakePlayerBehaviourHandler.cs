using UnityEngine;

namespace vio.spaceshooter.game.player
{
  public class FakePlayerBehaviourHandler
  {
    private const float TRANSITION_LEFT_X = -8.25f;
    private const float TRANSITION_RIGHT_X = 8.25f;

    private readonly Transform fakePlayerTransformComponent;
    
    private Vector3 realPlayerPosition;
    public FakePlayerBehaviourHandler(Transform fakePlayerTransformComponent)
    {
      this.fakePlayerTransformComponent = fakePlayerTransformComponent;
      this.HideFakePlayer();
    }

    public void HideFakePlayer()
    {
      this.fakePlayerTransformComponent.transform.position = new Vector3(0, 0, -20);
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
      fakePlayerTransformComponent.transform.position = new Vector3(
        this.calculateFakePlayerXPositionToTheRight(),
        this.realPlayerPosition.y,
        0
      );
    }

    private float calculateFakePlayerXPositionToTheRight()
    {
      return this.realPlayerPosition.x + 19.7f;
    }

    private bool isPlayerMovingOverRightWrappingThreshold()
    {
      return this.realPlayerPosition.x >= TRANSITION_RIGHT_X;
    }

    private void showFakePlayerToLeft()
    {
      fakePlayerTransformComponent.transform.position = new Vector3(
        calculateFakePlayerXPositionToTheLeft(),
        this.realPlayerPosition.y,
        0
      );
    }

    private float calculateFakePlayerXPositionToTheLeft()
    {
      return this.realPlayerPosition.x - 19.6f;
    }
  }

}