using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

public class Background : MonoBehaviour
{

    public GameManager.MainColor color;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = GameManager.instance.GetColorValue(color);
    }

    public void Spawn(float duration)
    {
        //spawn tween 
        //var temp_scale = transform.localScale;
        var temp_scale = Vector3.one * 30;
        transform.localScale = Vector3.zero;

        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, Vector3.zero, temp_scale, duration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
    public void Spawn()
    {
        Spawn( 0.25f);
    }
    public void Despawn(float duration)
    {
        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
            //kill();
            Destroy(this.gameObject);
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, transform.localScale, Vector3.zero, duration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
    public void Despawn()
    {
        Despawn(0.25f);
    }
}
