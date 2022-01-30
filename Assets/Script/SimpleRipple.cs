using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

public class SimpleRipple : MonoBehaviour
{

    SpriteRenderer sr;

    private void Awake()
    {

        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        transform.localScale = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    public void Ripple(Vector3 size, Color color, float duration)
    {
        sr.color = color;
        var targetColor = new Color(color.r, color.g, color.b, 0f);

        System.Action<ITween<Vector3>> sizeTweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };
        System.Action<ITween<Vector3>> sizeTweenCompleted = (t) =>
        {
            Destroy(this.gameObject);
        };
        gameObject.Tween(null, transform.localScale, size, duration, TweenScaleFunctions.CubicEaseOut, sizeTweenUpdate, sizeTweenCompleted);

        System.Action<ITween<Color>> colorTweenUpdate = (t) =>
        {
            sr.color = t.CurrentValue;
        };
        gameObject.Tween(null, color, targetColor, duration, TweenScaleFunctions.CubicEaseOut, colorTweenUpdate);
    }

}
