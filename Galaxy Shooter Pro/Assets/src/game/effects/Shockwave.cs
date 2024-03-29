﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private float GROWTH_RATE = 2.5f;

  private void Start()
  {
    AudioSource audioSource = this.GetComponent<AudioSource>();
    audioSource.PlayOneShot(audioSource.clip);
  }

  void Update()
    {
    float scale = this.transform.localScale.x + GROWTH_RATE * Time.deltaTime;
    if (scale > 16)
    {
      Destroy(this.gameObject);
    }
    else { 
      transform.localScale = new Vector3(scale, scale, 1);
    }

  }
}
