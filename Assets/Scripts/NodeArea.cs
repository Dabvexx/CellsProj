using UnityEngine;

public class NodeArea : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private BoxCollider2D bcInner;
    [SerializeField]
    private BoxCollider2D bcOutter;

    Bounds bounds;
    #endregion

    #region Unity Methods

    private void Start()
    {
        bcInner.size = new Vector2(ScreenBounds.windowWidth, ScreenBounds.windowHeight);
        bcOutter.size = new Vector2(ScreenBounds.windowWidth + 2f, ScreenBounds.windowHeight + 2f);

        bounds = bcInner.bounds;
        bounds.Expand(.75f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {                                                
        WarpNode(collision.gameObject);
    }

    #endregion

    #region Private Methods

    // TODO: THIS ENTIRE IDEA IS DUMB! SCRAP ALL OF THIS! JUST SPAWN A NEW NODE OUT OF BOUNDS WITH INCOMING VELOCITY!
    // Teleport a node somewhere offscreen moving back onscreen
    public static void WarpNode(GameObject node)
    {
        // Make sure to make the node have a velocity back inbounds
        // Move node to one of the edges +- 3.2f
        // Slightly over 3 so that the connections dont just suddenly appear and instead fade in smoothly

        // TODO: this is inefficient, make a dictionary when the nodes are first spawned with thier node script as the value and the gameobject as the key.
        var nodeScript = node.GetComponent<NodeAI>();

        // Note: Random.Range is inclusive, exclusive, so i need to make the value 5 to actually get 4 as a value
        //1: North, 2: South, 3: East, 4: West
        var spawnSide = Random.Range(1, 5);

        switch (spawnSide)
        {
            case 1:
                // North spawning.
                node.transform.position = new Vector2(RandomizePositionX(), ScreenBounds.windowHeight * .5f + 1f);
                nodeScript.RandomizeVelocityX();

                if (Mathf.Sign(nodeScript.velocityY) == 1)
                {
                    nodeScript.velocityY *= -1f;
                }

                break;

            case 2:
                // South spawning.
                node.transform.position = new Vector2(RandomizePositionX(), -ScreenBounds.windowHeight * .5f - 1f);
                nodeScript.RandomizeVelocityX();

                if (Mathf.Sign(nodeScript.velocityY) == -1)
                {
                    nodeScript.velocityY *= -1f;
                }

                break;

            case 3:
                // East spawning
                node.transform.position = new Vector2(ScreenBounds.windowWidth * .5f + 1f, RandomizePositionY());
                nodeScript.RandomizeVelocityY();

                if (Mathf.Sign(nodeScript.velocityX) == 1)
                {
                    nodeScript.velocityX *= -1f;
                }

                break;

            case 4:
                // West spawning
                node.transform.position = new Vector2(-ScreenBounds.windowWidth * .5f - 1f, RandomizePositionY());
                nodeScript.RandomizeVelocityY();

                if (Mathf.Sign(nodeScript.velocityX) == -1)
                {
                    nodeScript.velocityX *= -1f;
                }

                break;
        }

        // Use the bounds of the box collider to tell how far close to the bounds a node is, if the node is over (or under) a threshold, reverse the direction of the axis.

    }

    private static float RandomizePositionX()
    {
        // Divided by 2 since the bottom left corner is actually in the center
        // Dividing makes the node plane into the bounds of the screen
        float positionX = Random.Range(-ScreenBounds.windowWidth / 2f - 1, ScreenBounds.windowWidth / 2f + 1);

        return positionX;
    }

    private static float RandomizePositionY()
    {
        // Divided by 2 since the bottom left corner is actually in the center
        // Dividing makes the node plane into the bounds of the screen
        float positionY = Random.Range(-ScreenBounds.windowHeight / 2f - 1, ScreenBounds.windowHeight / 2f + 1);

        return positionY;
    }

    private void NodeCheck(GameObject node)
    {

    }

    #endregion
}
