using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; // MonoBehaviour, Vector3, Time, Input

namespace vio.spaceshooter
{
  public class Player : MonoBehaviour
  {

    private const float DEFAULT_PLAYER_SPEED = 15f;

    private const float MAX_Y_POS = 8f;
    private const float MIN_Y_POS = -0.8f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;

    private bool applyRestrictionVector = false;
    private float newX;
    private float newY;

    private const float TRANSITION_LEFT_X = -8.25f;
    private const float TRANSITION_RIGHT_X = 8.25f;

    private GameObject fakePlayer;
    // Start is called before the first frame update
    void Start()
    {
      this.transform.position = new Vector3(0, 0, 0);
      fakePlayer = GameObject.Find("Player_Fake");
      this.hideFakePlayer();
    }
    private void hideFakePlayer()
    {
      fakePlayer.transform.position = new Vector3(0, 0, -20);
    }

    // Update is called once per frame
    void Update()
    {
      this.movePlayer();
    }

    private void movePlayer()
    {
      this.applyPlayerInput();
      this.applyPlayerPositionRestrictions();
    }

    private void applyPlayerInput()
    {
      this.transform.Translate(
                  this.getDirectionInputVector()
                  * this.getPosibleDistanceMovedSinceLastFrame()
              );
    }

    private Vector3 getDirectionInputVector()
    {
      return Vector3.ClampMagnitude(
        new Vector3(
          Input.GetAxis("Horizontal"),
          Input.GetAxis("Vertical")
          ),
        1f
      );
    }

    private float getPosibleDistanceMovedSinceLastFrame()
    {
      return DEFAULT_PLAYER_SPEED * Time.deltaTime;
    }



    private void applyPlayerPositionRestrictions()
    {
      this.resetBoundingAreaVectorDetails();
      this.processYBoundingBoxRestrictions();
      this.processXWrappingBoundingBoxRestrictions();

      if (applyRestrictionVector)
      {
        this.transform.position = new Vector3(newX, newY);
      }

      this.transformFakePlayerForWrapping();
    }

    private void resetBoundingAreaVectorDetails()
    {
      this.applyRestrictionVector = false;
      this.newX = this.transform.position.x;
      this.newY = this.transform.position.y;
    }

    private void processYBoundingBoxRestrictions()
    {
      if (this.isPlayerAtYLowerBoundry())
      {
        this.applyRestrictionVector = true;
        this.newY = MIN_Y_POS;
      }
      else if (this.isPlayerAtYUpperBoundry())
      {
        this.applyRestrictionVector = true;
        this.newY = MAX_Y_POS;
      }
    }

    private bool isPlayerAtYUpperBoundry()
    {
      return this.transform.position.y > MAX_Y_POS;
    }

    private bool isPlayerAtYLowerBoundry()
    {
      return this.transform.position.y < MIN_Y_POS;
    }

    private void processXWrappingBoundingBoxRestrictions()
    {
      if (this.isPlayerFarEnoughToTheLeftToTeleportRight())
      {
        this.teleportPlayerToRight();
      }
      else if (this.isPlayerFarEnoughToTheRightToTeleportLeft())
      {
        this.teleportPlayerLeft();
      }
    }
    private bool isPlayerFarEnoughToTheLeftToTeleportRight()
    {
      return this.transform.position.x < MIN_X_POS;
    }
    private void teleportPlayerToRight()
    {
      this.applyRestrictionVector = true;
      this.newX = MAX_X_POS;
    }

    private bool isPlayerFarEnoughToTheRightToTeleportLeft()
    {
      return this.transform.position.x > MAX_X_POS;
    }

    private void teleportPlayerLeft()
    {
      this.applyRestrictionVector = true;
      this.newX = MIN_X_POS;
    }

    private void transformFakePlayerForWrapping()
    {
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
        this.hideFakePlayer();
      }
    }

    private bool isPlayerMovingOverLeftWrappingThreshold()
    {
      return this.transform.position.x <= TRANSITION_LEFT_X;
    }

    private void showFakePlayerToRight()
    {
      fakePlayer.transform.position = new Vector3(
        this.calculateFakePlayerXPositionToTheRight(),
        transform.position.y,
        0
      );
    }

    private float calculateFakePlayerXPositionToTheRight()
    {
      return this.transform.position.x + 19.7f;
    }

    private bool isPlayerMovingOverRightWrappingThreshold()
    {
      return this.transform.position.x >= TRANSITION_RIGHT_X;
    }

    private void showFakePlayerToLeft()
    {
      fakePlayer.transform.position = new Vector3(
        calculateFakePlayerXPositionToTheLeft(),
        transform.position.y,
        0
      );
    }

    private float calculateFakePlayerXPositionToTheLeft()
    {
      return this.transform.position.x - 19.6f;
    }
  }

}