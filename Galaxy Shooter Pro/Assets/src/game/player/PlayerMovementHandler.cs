using UnityEngine; 

namespace vio.spaceshooter.game.player
{
  public class PlayerMovementHandler
  {

    private const float DEFAULT_PLAYER_SPEED = 15f;
    private float playerSpeed;
    private const float MAX_Y_POS = 8f;
    private const float MIN_Y_POS = -0.8f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;

    private bool applyRestrictionVector = false;
    private float newX;
    private float newY;
    // 0 - none
    // 1 - right
    // -1 - left
    private int turningDirection = 0;

    private readonly Player player;
    private readonly FakePlayerBehaviourHandler fakePlayerBehaviourHandler;
    public PlayerMovementHandler(Player player, FakePlayerBehaviourHandler fakePlayerBehaviourHandler)
    {
      this.player = player;
      this.fakePlayerBehaviourHandler = fakePlayerBehaviourHandler;
      player.transform.position = new Vector3(0, 0, 0);
      playerSpeed = DEFAULT_PLAYER_SPEED;
    }

    public void Update()
    {
      this.animateMovement();
      this.movePlayer();
    }

    private void animateMovement()
    {
      Animator animator = this.player.GetComponent<Animator>();
      if (Input.GetAxis("Horizontal") == 0 && (this.turningDirection != 0))
      {
        animator.ResetTrigger("OnTurnRight");
        animator.ResetTrigger("OnTurnLeft");
        animator.ResetTrigger("OnNoTurning");

        animator.SetTrigger("OnNoTurning");
        this.turningDirection = 0;
      }
      else if ((Input.GetAxis("Horizontal") > 0) && (this.turningDirection != 1))
      {
        animator.ResetTrigger("OnTurnRight");
        animator.ResetTrigger("OnTurnLeft");
        animator.ResetTrigger("OnNoTurning");

        animator.StopPlayback();
        animator.SetTrigger("OnTurnRight");
        this.turningDirection = 1;
      }
      else if ((Input.GetAxis("Horizontal") < 0) && (this.turningDirection != -1))
      {
        animator.ResetTrigger("OnTurnRight");
        animator.ResetTrigger("OnTurnLeft");
        animator.ResetTrigger("OnNoTurning");

        animator.StopPlayback();
        animator.SetTrigger("OnTurnLeft");
        this.turningDirection = -1;
      }
    }

    private void movePlayer()
    {
      this.applyPlayerInput();
      this.applyPlayerPositionRestrictions();
    }

    private void applyPlayerInput()
    { 
      this.player.transform.Translate(
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
      return playerSpeed * Time.deltaTime;
    }

    private void applyPlayerPositionRestrictions()
    {
      this.resetBoundingAreaVectorDetails();
      this.processYBoundingBoxRestrictions();
      this.processXWrappingBoundingBoxRestrictions();

      if (this.applyRestrictionVector)
      {
        this.player.transform.position = new Vector3(this.newX, this.newY);
      }

      this.fakePlayerBehaviourHandler.Update(this.player.transform.position);
    }

    private void resetBoundingAreaVectorDetails()
    {
      this.applyRestrictionVector = false;
      this.newX = this.player.transform.position.x;
      this.newY = this.player.transform.position.y;
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
      return this.player.transform.position.y > MAX_Y_POS;
    }

    private bool isPlayerAtYLowerBoundry()
    {
      return this.player.transform.position.y < MIN_Y_POS;
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
      return this.player.transform.position.x < MIN_X_POS;
    }
    private void teleportPlayerToRight()
    {
      this.applyRestrictionVector = true;
      this.newX = MAX_X_POS;
    }

    private bool isPlayerFarEnoughToTheRightToTeleportLeft()
    {
      return this.player.transform.position.x > MAX_X_POS;
    }

    private void teleportPlayerLeft()
    {
      this.applyRestrictionVector = true;
      this.newX = MIN_X_POS;
    }

    public void Reset()
    {
      this.fakePlayerBehaviourHandler.HideFakePlayer();
    }

    public void SetPlayerSpeed(float speed)
    {
      this.playerSpeed = speed;
    }

    public void ResetPlayerSpeed()
    {
      this.playerSpeed = DEFAULT_PLAYER_SPEED;
    }
    public void ResetPlayerAnimations()
    {
      this.player.GetComponent<Animator>().SetTrigger("OnNoTurning");
      this.turningDirection = 0;
    }
  }

}