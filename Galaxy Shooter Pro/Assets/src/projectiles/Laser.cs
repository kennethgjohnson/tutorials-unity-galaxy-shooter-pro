using UnityEngine;

namespace vio.spaceshooter.projectiles
{
  public class Laser : MonoBehaviour
  {
    private const float DEFAULT_SPEED = 20f;
    private const float MAX_Y_POS = 10f;

    void Update()
    {
      this.applyLaserMovement();
      if (this.isLaserOutOfBoundry())
      {
        this.removeLaser();
      }
    }

    private void applyLaserMovement()
    {
      this.transform.Translate(
                  Vector3.up
                  * this.getPosibleDistanceMovedSinceLastFrame()
              );
    }

    private float getPosibleDistanceMovedSinceLastFrame()
    {
      return DEFAULT_SPEED * Time.deltaTime;
    }

    private bool isLaserOutOfBoundry()
    {
      return (this.transform.position.y > MAX_Y_POS);
    }

    private void removeLaser()
    {
      Destroy(this.gameObject);
    }
  }
}