using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.playerSpawnPoints.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
