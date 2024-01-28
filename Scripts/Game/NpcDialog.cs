using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialog : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;
    public bool readyToSpeak;
    public bool startDialogue;

    private Animator npcAnimator; // Tambahkan animator untuk NPC

    void Start()
    {
        dialoguePanel.SetActive(false);
        npcAnimator = GetComponent<Animator>(); // Dapatkan komponen animator pada NPC
    }

    // Jika tag NPC bersentuhan dengan pemain, aktifkan animasi isNpc dan mulai dialog
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = true;

            // Aktifkan animator bool isNpc
            if (npcAnimator != null)
            {
                npcAnimator.SetBool("isNpc", true);
            }

            // Mulai dialog secara otomatis
            if (!startDialogue)
            {
                Rigidbody2D playerRb = FindObjectOfType<Karakter>().GetComponent<Rigidbody2D>();
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
                StartDialogue();
            }
        }
    }

    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";
        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = false;

            // Nonaktifkan animator bool isNpc ketika pemain keluar dari area NPC
            if (npcAnimator != null)
            {
                npcAnimator.SetBool("isNpc", false);
            }
        }
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
            // Kembalikan keterbatasan Rigidbody2D ke kondisi normal
            Rigidbody2D playerRb = FindObjectOfType<Karakter>().GetComponent<Rigidbody2D>();
            playerRb.constraints = RigidbodyConstraints2D.None;
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void StartDialogue()
    {
        nameNpc.text = "Paman";
        imageNpc.sprite = spriteNpc;
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        // Memeriksa tombol untuk melanjutkan dialog
        if (Input.GetButtonDown("Fire1") && readyToSpeak && startDialogue)
        {
            NextDialogue();
        }
    }
}
