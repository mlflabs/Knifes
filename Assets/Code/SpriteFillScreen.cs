using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFillScreen : MonoBehaviour
{
    public bool KeepAspectRatio;

    int lastScreenWidth = 0;
    int lastScreenHeight = 0;


    void Start()
    {
        adjustScreen();
    }

    private void Update()
    {
        if(lastScreenWidth != Screen.width || lastScreenHeight != Screen.height)
   {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            adjustScreen();
        }
    }

    private void adjustScreen()
    {
        transform.localScale = new Vector3(1, 1, 1);
        var rightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var worldSpaceWidth = Mathf.Abs(rightCorner.x * 2);
        var worldSpaceHeight = Mathf.Abs(rightCorner.y * 2);

        var spriteSize = GetComponent<SpriteRenderer>().bounds.size;
        float scaleFactorX = worldSpaceWidth / spriteSize.x;
        float scaleFactorY = worldSpaceHeight / spriteSize.y;
        if (KeepAspectRatio)
        {
            if (scaleFactorX > scaleFactorY)
            {
                scaleFactorY = scaleFactorX;
            }
            else
            {
                scaleFactorX = scaleFactorY;
            }
        }
        transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1);
    }
}
