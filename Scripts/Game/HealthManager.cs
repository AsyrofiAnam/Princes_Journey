using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import namespace TMPro
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;

    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        health = 3;
    }

    void Update()
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if (i < health)
            {
                heart[i].sprite = fullHeart;
            }
            else
            {
                heart[i].sprite = emptyHeart;
            }
        }
    }

    // Fungsi untuk memperbarui tampilan UI health
    public void UpdateHealthUI()
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if (i < health)
            {
                heart[i].sprite = fullHeart;
            }
            else
            {
                heart[i].sprite = emptyHeart;
            }
        }
    }
}
