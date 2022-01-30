using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

public class SpriteButton_Off : SpriteButton
{
    public override void CheckCondition(Pointer pointer)
    {
        base.CheckCondition(pointer);
        if (condition)
        {
            pointer.FullHide();
            Despawn();
        }
    }
    public override void SetTextColorByCondition(Pointer pointer)
    {
        if (condition)
        {
            //text.color = GameManager.instance.GetColorValue(pointer.pointerColor);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = GameManager.instance.GetColorValue(pointer.pointerColor);
        }
        else
        {
            text.color = Color.white; // default
        }
    }
    public override void Despawn(float duration)
    {
        if (pendingDestroy) return;

        duration = 0.2f;

        pendingDestroy = true;

        var targetScale = new Vector3(transform.localScale.x * 1.2f, 0, 0);

        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
            kill();
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, transform.localScale, targetScale, duration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
}
