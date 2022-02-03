using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    #region Variables
    // Node gameobject prefab.
    public GameObject nodePrefab;

    // Constrain the nodes within the border.
    [Range(0, 1f)]
    public float constrainSpawnAreaX = 1f;

    [Range(0, 1f)]
    public float constrainSpawnAreaY = 1f;

    // Enum of node rendering layers
    private string[] NodeLayer = new string[]
    {
        "NodeFront",
        "NodeMid",
        "NodeBack"
    };

    // Foreground node variables.
    #region Foreground Nodes

    [Header("Foreground Nodes Variables")]

    // Initial node amount spawned.
    [Range(0, 250)]
    public int initialNodeAmtFront = 100;

    // List of all gameobjects in scene in mid layer.
    public List<GameObject> nodesFront = new List<GameObject>();

    // The Scale Of the nodes
    [Range(0, 2f)]
    public float  nodeScaleFront = .5f;

    // Control the transparency of nodes.
    [Range(0, 1f)]
    public float nodeAlphaFront = 1f;

    #endregion Foreground Nodes

    // Midground node variables.
    #region Midground Nodes

    [Header("Midground Nodes Variables")]

    // Initial node amount spawned.
    [Range(0, 250)]
    public int initialNodeAmtMid = 100;

    // List of all gameobjects in scene in front layer.
    public List<GameObject> nodesMid = new List<GameObject>();

    // The Scale Of the nodes
    [Range(0, 2f)]
    public float nodeScaleMid = .5f;

    // Control the transparency of nodes.
    [Range(0, 1f)]
    public float nodeAlphaMid = 1f;

    #endregion Midground Nodes

    // Background node variables.
    #region Background Nodes

    [Header("Background Nodes Variables")]

    // Initial node amount spawned.
    [Range(0, 250)]
    public int initialNodeAmtBack = 100;

    // List of all gameobjects in scene in front layer.
    public List<GameObject> nodesBack = new List<GameObject>();

    // The Scale Of the nodes
    [Range(0, 2f)]
    public float nodeScaleBack = .5f;

    // Control the transparency of nodes.
    [Range(0, 1f)]
    public float nodeAlphaBack = 1f;

    #endregion Background Nodes

    #endregion

    #region Unity Methods

    void Start()
    {
        SpawnNodes();
    }

    #endregion

    #region Methods

    private void SpawnNodes()
    {
        InstantiateNodes(initialNodeAmtFront, nodesFront, nodeScaleFront, nodeAlphaFront, NodeLayer[0]);
        InstantiateNodes(initialNodeAmtMid, nodesMid, nodeScaleMid, nodeAlphaMid, NodeLayer[1]);
        InstantiateNodes(initialNodeAmtBack, nodesBack, nodeScaleBack, nodeAlphaBack, NodeLayer[2]);
    }

    /// <summary>
    /// Instantiates a node at a certain position on screen.
    /// </summary>
    /// <param name="amount">The amount of nodes to spawn.</param>
    private void InstantiateNodes(int amount, List<GameObject> nodes, float scale, float alpha, string layer)
    {
        if(amount < 0)
        {
            //return;
        }

        for (int i = 0; i < amount; i++)
        {
            // Instantiate node with scale and name.
            GameObject node = Instantiate(nodePrefab, RandomizePosition(), Quaternion.identity);
            node.layer = LayerMask.NameToLayer(layer);
            node.transform.localScale *= scale;
            node.name = $"Node_{nodes.Count + 1}";

            // Get node sprite renderer to change its layer.
            var nodeSr = node.GetComponent<SpriteRenderer>();

            // Change the alpha of the color and set it.
            Color nodeColor = nodeSr.color;
            nodeColor.a = alpha;
            nodeSr.color = nodeColor;

            // Set the sorting layer of node.
            nodeSr.sortingLayerName = layer;

            // Add node to list
            nodes.Add(node);
        }
    }

    /// <summary>
    /// Randomize the position of spawned nodes 
    /// </summary>
    /// <returns></returns>
    private Vector2 RandomizePosition()
    {
        // Divided by 2 since the bottom left corner is actually in the center
        // Dividing makes the node plane into the bounds of the screen
        float positionX = Random.Range(-ScreenBounds.windowWidth / 2f * constrainSpawnAreaX, ScreenBounds.windowWidth / 2f * constrainSpawnAreaX);
        float positionY = Random.Range(-ScreenBounds.windowHeight / 2f * constrainSpawnAreaY, ScreenBounds.windowHeight / 2f * constrainSpawnAreaY);

        return new Vector2(positionX, positionY);
    }

    #endregion
}
