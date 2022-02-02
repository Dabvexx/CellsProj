using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{

    #region Variables
    [Range(1, 250)]
    public int initialNodeAmt = 10;
    public float nodeScale = .5f;

    public List<GameObject> nodes = new List<GameObject>();

    public GameObject nodeObj;

    [Range(0,1)]
    public float ConstrainSpawnX = 1f;
    [Range(0, 1)]
    public float ConstrainSpawnY = 1f;
    #endregion

    #region Unity Methods

    void Start()
    {
        InstantiateNode(initialNodeAmt);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Instantiates a node at a certain position on screen.
    /// </summary>
    /// <param name="amount">The location in the array the node is at.</param>
    private void InstantiateNode(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Instantiate
            GameObject node = Instantiate(nodeObj, RandomizePosition(), Quaternion.identity);
            node.transform.localScale *= nodeScale;
            node.name = $"Node_{i + 1}";

            // TODO: THIS IS CATASTROPHICALLY BAD! PLEASE FIX THIS!

            nodes.Add(node);
        }
    }

    // Randomize the position of the nodes 
    private Vector2 RandomizePosition()
    {
        // Divided by 2 since the bottom left corner is actually in the center
        // Dividing makes the node plane into the bounds of the screen
        float positionX = Random.Range(-ScreenBounds.windowWidth / 2f * ConstrainSpawnX, ScreenBounds.windowWidth / 2f * ConstrainSpawnX);
        float positionY = Random.Range(-ScreenBounds.windowHeight / 2f * ConstrainSpawnY, ScreenBounds.windowHeight / 2f * ConstrainSpawnY);

        return new Vector2(positionX, positionY);
    }

    #endregion
}
