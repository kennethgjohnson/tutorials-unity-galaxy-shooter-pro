using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vio.spaceshooter.projectiles
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
