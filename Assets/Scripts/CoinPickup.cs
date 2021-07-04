using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int _scorePoints = 100;
    [SerializeField] private AudioClip _coinPickUpSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameSession>().AddToScore(_scorePoints);
        AudioSource.PlayClipAtPoint(_coinPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
