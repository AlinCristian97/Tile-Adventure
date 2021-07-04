using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip _coinPickUpSFX;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // add to currency
        AudioSource.PlayClipAtPoint(_coinPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
