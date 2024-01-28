using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;
    int backCount;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed = 0.05f;

    public Transform player; 
    public float playerMoveThreshold = -8.662107f; 

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        backCount = transform.childCount;

        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

            Vector3 initialPosition = backgrounds[i].transform.position;
            initialPosition.x = 2.402099f;
            backgrounds[i].transform.position = initialPosition;
        }

        BackSpeedCalculate();
    }

    void BackSpeedCalculate()
    {
        farthestBack = 0;

        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        // Perbarui parallax berdasarkan pergerakan pemain.
        float playerMove = player.position.x - playerMoveThreshold;
        for (int i = 0; i < backCount; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(playerMove, 0) * speed);
        }
    }
}