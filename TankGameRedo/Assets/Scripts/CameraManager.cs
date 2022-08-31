using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public new Camera camera;
    public Pawn pawn;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.multiPlayer)
        {
            Debug.Log("test1");
            if (pawn.controller.gameObject.name == "Player1Controller(Clone)")
            {
                camera.rect = new Rect(0, 0.5f, 1, 0.5f);
            }
            else
            {
                camera.rect = new Rect(0, 0, 1, 0.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}