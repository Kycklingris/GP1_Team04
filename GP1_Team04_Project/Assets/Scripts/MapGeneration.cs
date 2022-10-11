using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private MapPiece[] straightPieces;
    [SerializeField] private int straightWeight = 60;
    [SerializeField] private MapPiece[] rightPieces;
    [SerializeField] private int rightWeight = 20;
    [SerializeField] private MapPiece[] leftPieces;
    [SerializeField] private int leftWeight = 20;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private List<MapPiece> placedPieces = new List<MapPiece>();
    [SerializeField] private float movementSpeed;
    [SerializeField] private float zCutOffPoint;

    private int combinedWeight;

    // Start is called before the first frame update
    void Start()
    {
        this.combinedWeight = this.straightWeight + this.rightWeight + this.leftWeight;

        this.Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.Rotate(90);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.Rotate(-90);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var placed in this.placedPieces) // Move pieces
        {
            placed.gameObject.transform.position += new Vector3(0.0f, 0.0f, this.movementSpeed * Time.deltaTime);
        }

        if (this.placedPieces[0].gameObject.transform.position.z < this.zCutOffPoint)
        {
            Destroy(this.placedPieces[0].gameObject);
            this.placedPieces.RemoveAt(0);
        }
        
        if (this.placedPieces.Count < 5)
        {
            this.Generate();
        } else if (this.placedPieces.Count > 20)
        {
            Destroy(this.placedPieces[0].gameObject);
            this.placedPieces.RemoveAt(0);
        }
    }

    public void Rotate(float degrees)
    {
        foreach (var placed in this.placedPieces)
        {
            placed.gameObject.transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, degrees);
        }
    }

    void Generate()
    {
        while(true)
        {
            var last = this.placedPieces[this.placedPieces.Count - 1];

            if (last.type != MapPieceType.Straight) // To not get two turns in a row
            {
                this.Spawn(last.spawnPoint, last.forward, MapPieceType.Straight);
                return;
            }

            var rand = Random.Range(0, this.combinedWeight - 1);

            if (rand < this.straightWeight) // Generate until next turn
            {
                this.Spawn(last.spawnPoint, last.forward, MapPieceType.Straight);
                continue;
            } else if (rand < this.straightWeight + this.rightWeight)
            {
                this.Spawn(last.spawnPoint, last.forward, MapPieceType.Right);
                return;
            } else if (rand < this.straightWeight + this.rightWeight + this.leftWeight)
            {
                this.Spawn(last.spawnPoint, last.forward, MapPieceType.Left);
                return;
            }
        }
    }

    void Spawn(Vector3 position, Vector3 forward, MapPieceType type)
    {
        MapPiece prefab;
        if (type == MapPieceType.Right)
        {
            var rand = Random.Range(0, this.rightPieces.Length - 1);
            prefab = this.rightPieces[rand];
        }
        else if (type == MapPieceType.Left)
        {
            var rand = Random.Range(0, this.leftPieces.Length - 1);
            prefab = this.leftPieces[rand];
        }
        else if (type == MapPieceType.Straight)
        {
            var rand = Random.Range(0, this.straightPieces.Length - 1);
            prefab = this.straightPieces[rand];
        }
        else 
        {
            return;
        }

        var newPiece = GameObject.Instantiate(prefab.gameObject, position, Quaternion.LookRotation(forward, Vector3.up));

        this.placedPieces.Add(newPiece.GetComponent<MapPiece>());
    }
}