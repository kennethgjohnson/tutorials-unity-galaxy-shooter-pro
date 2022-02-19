using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vio.spaceshooter.game
{
  public class Background : MonoBehaviour
  {
    private const float DRIFT_RADIUS = 0.25f;

    // Update is called once per frame
    void Update()
    {
      float x = DRIFT_RADIUS * Mathf.Cos((float)Time.frameCount / 500f);
      float y = 4f - DRIFT_RADIUS  * Mathf.Sin((float)Time.frameCount / 500f);
      this.transform.position = new Vector2(x, y);
    }
  }
}