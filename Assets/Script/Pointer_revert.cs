using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer_revert : Pointer
{
    private void Start()
    {
        id = "Yang";
    }

    public override void UpdatePointerPos()
    {
        var mousePos = Input.mousePosition;

        var remap_mousePos = new Vector2(
                mousePos.x.Remap(0, Screen.width, Screen.width, 0),
                mousePos.y.Remap(0, Screen.height, Screen.height, 0)
            );

        var pos = Camera.main.ScreenToWorldPoint(remap_mousePos);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
