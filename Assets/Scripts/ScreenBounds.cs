using UnityEngine;

public static class ScreenBounds
{

	#region Variables
	public static Camera cam = Camera.main;
	public static readonly float windowHeight = 2f * cam.orthographicSize;
	public static readonly float windowWidth = windowHeight * cam.aspect;
	#endregion

}
