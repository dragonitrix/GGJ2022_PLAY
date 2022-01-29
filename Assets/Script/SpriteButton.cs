using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

public class SpriteButton : MonoBehaviour
{

    Level parentLevel;

    public bool condition = false;

    public void OnPointerOver(Pointer pointer)
    {
        //Debug.Log("OnPointerOver " + pointer.id);
    }
    public void OnPointerEnter(Pointer pointer)
    {
        //Debug.Log("OnPointerEnter " + pointer.id);
    }
    public void OnPointerExit(Pointer pointer)
    {
        //Debug.Log("OnPointerExit " + pointer.id);
    }
    public void OnPointerClick(Pointer pointer)
    {
        Debug.Log("OnPointerClick " + pointer.id);
        CheckCondition(pointer);
        parentLevel.CheckCondition();
    }

    public virtual void CheckCondition(Pointer pointer)
    {
        this.condition = true;
    }

    public void Spawn(Level level, float duration)
    {
        //assign level parent
        parentLevel = level;

        //spawn tween 
        var temp_scale = transform.localScale;
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
    public void Spawn(Level level)
    {
        Spawn(level, 0.25f);
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
            Destroy(this.gameObject);
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, transform.localScale, Vector3.zero, duration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
    public void Despawn()
    {
        Despawn(0.25f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
