using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaySpawn : MonoBehaviour
{
    [SerializeField] private GameObject hallway;

    public bool canSpawnNew;
    // Start is called before the first frame update
    void Start()
    {
        canSpawnNew = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnNew == true)
        {
            Instantiate(hallway);
        }
    }
}
