using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButton_ColorCue : SpriteButton
{
    public GameManager.MainColor color;

    public override void CheckCondition(Pointer pointer)
    {
        if (color == pointer.pointerColor)
        {
            condition = true;
        }
        else
        {
            condition = false;
        }
    }

}
