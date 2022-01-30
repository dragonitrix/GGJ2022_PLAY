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
            //condition = false;
        }
    }

    public override void SetTextColorByCondition(Pointer pointer)
    {
        if (condition)
        {
            text.color = GameManager.instance.GetColorValue(color);
        }
        else
        {
            text.color = Color.white; // default
        }
    }
}
