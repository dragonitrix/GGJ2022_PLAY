using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

public class RevertBackground : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float rotateSpeed = 10f;
    float rotateSpeed_multiplier = 20f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime * rotateSpeed_multiplier);
    }

    public void Spawn(float duration)
    {
        //spawn tween 
        //var temp_scale = transform.localScale;
        var temp_scale = Vector3.one * 2.2f;
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
        gameObject.Tween("scale", Vector3.zero, temp_scale, duration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

        System.Action<ITween<float>> multiplierUpdate = (t) =>
        {
            rotateSpeed_multiplier = t.CurrentValue;
        };
        gameObject.Tween("multiplier", 10f, 1f, duration * 2.5f, TweenScaleFunctions.CubicEaseIn, multiplierUpdate);
    }
    public void Spawn()
    {
        Spawn(0.35f);
    }
    public void Despawn(float duration)
    {
        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
            rotateSpeed_multiplier = t.CurrentProgress.Remap(0f, 1f, 1f, 20f);
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
