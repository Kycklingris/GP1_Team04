using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayScrolling : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;
    [SerializeField] private int test;
    

    private HallwaySpawn _hallwaySpawn;

    private void Awake()
    {
        _hallwaySpawn = gameManager.GetComponent<HallwaySpawn>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate( speed * Time.deltaTime * - player.transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("apa");
        if (other.CompareTag("Respawn"))
        {
            _hallwaySpawn.canSpawnNew = true;
            Debug.Log("apa");
        }
    }
}
