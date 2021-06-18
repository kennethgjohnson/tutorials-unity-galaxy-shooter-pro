using UnityEngine;

namespace vio.spaceshooter.game.projectiles
{
  public class ProjectileGroup : MonoBehaviour
  {
    void Update()
    {
      if (this.transform.childCount == 0)
      {
        Destroy(this.gameObject);
      }
    }
  }
}
