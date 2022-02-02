using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeAI : LineManager
{

    #region Variables
    // The speed at which the node travels on a given axis, randomized at run time.
    [SerializeField]
    [Range(-.005f, .005f)] 
    public float velocityX = 0f;
    [SerializeField]
    [Range(-.005f, .005f)]
    public float velocityY = 0f;

    [SerializeField]
    [Range(-.001f, -.01f)]
    private float minSpeed = -.005f;

    [SerializeField]
    [Range(.001f, .01f)]
    private float maxSpeed = .005f;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        RandomizeVelocityX();
        RandomizeVelocityY();
    }

    void Update()
    {
        MoveNode();
        UpdateLines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tried to make a system so their wouldnt be 2 LineRenderers stacked ontop of eachother
        /*Node collidedNode = collision.GetComponent(typeof (Node)) as Node;

        bool isInNeighborList = false;

        foreach (var item in collidedNode.neighbors)
        {
            if (item == this.gameObject)
            {
                isInNeighborList = true;
            }
        }

        if (!isInNeighborList)
        {
            neighbors.Add(collision.gameObject);
            Debug.Log($"{collision} is colliding with {name}");
        }*/
        // Add node to the list and create a line renderers
        // Dont connect to the spawner gameobjects by looking for tags
        if (!collision.CompareTag("Spawner") && !collision.CompareTag("MainCamera"))
        {
            AddConnection(collision.gameObject);
        }

        //Debug.Log($"{collision} is colliding with {name}");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the node from neighbor list along with the line renderer.
        RemoveConnection(collision.gameObject);

        // Remove line renderer associated to neighbor.
        //Debug.Log($"{collision} is no longer colliding with {name}");
    }

    #endregion

    #region Private Methods

    private void CullNeighborList()
    {

    }

    private void MoveNode()
    {
        // Simple translate to move nodes.
        transform.Translate(velocityX, velocityY, 0);
    }



    // Randomizes the horizontal direction of node.
    public void RandomizeVelocityX()
    {
        velocityX = Random.Range(minSpeed, maxSpeed);
    }

    // Randomizes the vertical direction of node.
    public void RandomizeVelocityY()
    {
        velocityY = Random.Range(minSpeed, maxSpeed);
    }

    #endregion
}
