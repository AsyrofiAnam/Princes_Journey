using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image potraitImage;
    [SerializeField] private string[] speaker;
    [SerializeField][TextArea] private string[] dialogueWords;
    [SerializeField] private Sprite[] potrait;
    private bool dialogueActivated;
    private int step;
    private bool isTyping; // Tambahkan variabel untuk mengecek apakah proses penulisan teks sedang berlangsung
    private Rigidbody2D playerRigidbody; // Referensi ke Rigidbody2D karakter

    private void Start()
    {
        // Dapatkan komponen Rigidbody2D dari karakter pada awal permainan
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Menampilkan dialog jika tombol "Fire1" ditekan dan dialog diaktifkan
        if (Input.GetKeyDown(KeyCode.Return) && dialogueActivated && !isTyping)
        {
            ShowDialog();
        }

        // Melewatkan proses penulisan teks dengan menekan tombol "Space"
        if (isTyping && Input.GetKeyDown(KeyCode.W))
        {
            StopAllCoroutines(); // Hentikan coroutine yang sedang berjalan
            dialogueText.text = dialogueWords[step - 1]; // Tampilkan seluruh teks sekaligus
            isTyping = false; // Selesai proses penulisan teks
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueActivated = true;
            dialogueCanvas.SetActive(true);

            // Bekukan karakter ketika dialog aktif
            if (playerRigidbody != null)
            {
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            // Memastikan langkah pertama dialog ditampilkan saat memasuki area
            step = 0;
            ShowDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Menonaktifkan dialog saat keluar dari area
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);

        // Kembalikan karakter ke keadaan semula setelah dialog ditutup
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.None;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Menampilkan dialog pada langkah tertentu
    private void ShowDialog()
    {
        // Menampilkan dialog selama langkah belum mencapai batas maksimum
        if (step < speaker.Length)
        {
            speakerText.text = speaker[step];
            StartCoroutine(ShowDialogueCoroutine());
            potraitImage.sprite = potrait[step];
            step++;
        }
        // Menutup dialog jika langkah mencapai batas maksimum
        else
        {
            dialogueCanvas.SetActive(false);

            // Kembalikan karakter ke keadaan semula setelah dialog ditutup
            if (playerRigidbody != null)
            {
                playerRigidbody.constraints = RigidbodyConstraints2D.None;
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    IEnumerator ShowDialogueCoroutine()
    {
        isTyping = true; // Mulai proses penulisan teks

        dialogueText.text = "";
        foreach (char letter in dialogueWords[step])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.035f);
        }

        isTyping = false; // Selesai proses penulisan teks
    }
}
