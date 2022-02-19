using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private float GROWTH_RATE = 2.5f;
    // Update is called once per frame
    void Update()
    {
    float scale = this.transform.localScale.x + GROWTH_RATE * Time.deltaTime;
    if (scale > 4)
    {
      Destroy(this.gameObject);
    }
    else { 
      transform.localScale = new Vector3(scale, scale, 1);
    }

  }
}
