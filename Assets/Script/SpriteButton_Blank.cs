using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButton_Blank : SpriteButton
{
    private void Start()
    {
        condition = true;
    }

    public override void CheckCondition(Pointer pointer)
    {
        condition = true;
    }

    public override void SetTextColorByCondition(Pointer pointer)
    {
    }
}
