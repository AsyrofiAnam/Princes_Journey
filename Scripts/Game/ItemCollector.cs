using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int coin = 5; // Set the initial value to 5
    [SerializeField] private TextMeshProUGUI coinText; // Use TextMeshProUGUI

    private AudioManager audioManager; // Reference to AudioManager

    private void Start()
    {
        // Find AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Initialize coinText with the starting value
        UpdateCoinText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coin++;
            UpdateCoinText();

            // Play collectibles sound effect from AudioManager
            audioManager.PlaySFX(audioManager.collectibles);
        }
    }

    public void SubtractCoins(int amount)
    {
        coin -= amount;
        UpdateCoinText();
    }

    public int GetCoins()
    {
        return coin;
    }

    private void UpdateCoinText()
    {
        coinText.text = ":" + coin;
    }
}
