using UnityEngine;
using System.Collections;

public class ForceCameraAspect : MonoBehaviour
{
    public float CameraWidthRatio = 16;
    public float CameraHeightRatio = 9;

	// Use this for initialization
	void Start ()
	{
	    var targetAspect = CameraWidthRatio/CameraHeightRatio;
	    var windowAspect = (float)Screen.width/Screen.height;
	    var scaleHeight = windowAspect/targetAspect;

	    var camera = GetComponent<Camera>();

	    if (scaleHeight < 1.0f)
	    {
	        var rect = camera.rect;
	        rect.width = 1.0f;
	        rect.height = scaleHeight;
	        rect.x = 0;
	        rect.y = (1.0f - scaleHeight)/2.0f;

	        camera.rect = rect;
	    }
	    else
	    {
            var scalewidth = 1.0f / scaleHeight;
            var rect = camera.rect;
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
