using UnityEngine;

public class Resize : MonoBehaviour
{

    #region Variables
    float quadHeight;

    float quadWidth;
	#endregion
	
	#region Unity Methods
    
    void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x * .5f, Camera.main.transform.position.y * .5f, 0);
        transform.localScale = new Vector3(ScreenBounds.windowWidth / 10, ScreenBounds.windowHeight, 1);
    }
	
	#endregion
}
