using UnityEngine;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject lrPrefab;

    private int counter = 1;

    // Dictionary that associates a connection to its line renderer
    [SerializeField]
    protected IDictionary<GameObject, GameObject> connections = new Dictionary<GameObject, GameObject>();

    [SerializeField]
    private float fadeScale = .45f;
    #endregion

    #region Methods
    // Method to connect nodes together with a line renderer.
    protected GameObject ConnectNodes(GameObject connection)
    {
        // Instantiate the gameobject that holds our line renderer.
        GameObject lrChild = Instantiate(lrPrefab, transform.position, Quaternion.identity, transform);
        lrChild.name = $"LrObj_{counter}";
        counter++;

        return lrChild;
    }

    protected void RemoveConnection(GameObject connection)
    {
        if (connections.ContainsKey(connection))
        {
            // Destroy the game object from the collision that just exited
            Destroy(connections[connection].gameObject);

            // Remove the collider refrence itself from the list.
            connections.Remove(connection);
            counter--;
        }
    }

    protected void AddConnection(GameObject connection)
    {
        if (!connections.ContainsKey(connection))
        {
            // Add new connection to Dictionary, the collider as the key and the line renderer as the value.
            connections.Add(connection, ConnectNodes(connection));
        }
    }

    // Get the distance between 2 nodes.
    private float GetNeighborDistance(GameObject connection)
    {
        Vector2 v1 = new Vector2(transform.position.x, transform.position.y);
        Vector2 v2 = new Vector2(connection.transform.position.x, connection.transform.position.y);
        return Vector2.Distance(v1, v2);
    }

    // Update the line's position to follow the nodes
    protected void UpdateLines(float maxalpha)
    {
        foreach (var connection in connections)
        {
            // Get the line renderer component of the object
            var lr = connection.Value.gameObject
                .GetComponent<LineRenderer>();

            // Get the node this node is connected to
            var node = connection.Key.gameObject;

            // Fade the line programatically by Lerping between 0 and 1 with the distance between the nodes and multiplying by a value.
            // This value will be applied to the alpha (brightness), thus dimming or brightening the line
            var alpha = Mathf.Lerp(maxalpha, .1f, GetNeighborDistance(node) * fadeScale);

            // Creating a new gradient.
            Gradient lrGradient = new Gradient();

            // Setting the keys of the gradiant using the original color and the alpha value between 0 and 1 using the lerp
            lrGradient.SetKeys
            (
                lr.colorGradient.colorKeys,
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1f) }
            );

            // Set the line's gradient to the gradient
            lr.colorGradient = lrGradient;

            // Setting the start and end points of the line to the nodes.
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, node.transform.position);

        }
    }

    #endregion
}
