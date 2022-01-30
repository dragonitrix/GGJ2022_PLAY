using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

using TMPro;

public class SpriteButton : MonoBehaviour
{

    protected bool pendingDestroy = false;

    float hoverTweenDuration = 0.1f;

    Level parentLevel;

    public bool condition = false;

    ITween enterTween = null;
    ITween exitTween = null;

    protected TextMeshPro text;

    public void OnPointerOver(Pointer pointer)
    {
        if (pendingDestroy) return;
        //Debug.Log("OnPointerOver " + pointer.id);
    }
    public void OnPointerEnter(Pointer pointer)
    {
        if (pendingDestroy) return;
        //Debug.Log("OnPointerEnter " + pointer.id);
        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
            enterTween = null;
        };

        // completion defaults to null if not passed in
        enterTween = gameObject.Tween(null, transform.localScale, Vector3.one * 1.1f, hoverTweenDuration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
    public void OnPointerExit(Pointer pointer)
    {
        if (pendingDestroy) return;
        //Debug.Log("OnPointerExit " + pointer.id);
        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
            exitTween = null;
            isOnClicking = false;
        };

        // completion defaults to null if not passed in
        exitTween = gameObject.Tween(null, transform.localScale, Vector3.one, hoverTweenDuration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }

    bool isOnClicking = false;

    public void OnPointerClick(Pointer pointer)
    {
        if (pendingDestroy) return;
        if (isOnClicking) return;
        //Debug.Log("OnPointerClick " + pointer.id);

        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, transform.localScale, Vector3.one * 1.2f, hoverTweenDuration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
    public void OnPointerRelease(Pointer pointer)
    {
        if (pendingDestroy) return;
        //Debug.Log("OnPointerClick " + pointer.id);

        CheckConditionSequence(pointer);

        System.Action<ITween<Vector3>> tweenUpdate = (t) =>
        {
            transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> tweenCompleted = (t) =>
        {
            //Debug.Log("tween completed");
            isOnClicking = false;
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, transform.localScale, Vector3.one, hoverTweenDuration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }

    public void CheckConditionSequence(Pointer pointer)
    {
        CheckCondition(pointer);
        SetTextColorByCondition(pointer);

        //parentLevel.PendingCheckCondition();
        parentLevel.conditions.Add(condition);
        parentLevel.CheckCondition();

    }
    
    public virtual void CheckCondition(Pointer pointer)
    {
        this.condition = true;


    }
    public virtual void SetTextColorByCondition(Pointer pointer)
    {
        if (condition)
        {
            text.color = GameManager.instance.GetColorValue(pointer.pointerColor);
        }
        else
        {
            text.color = Color.white; // default
        }
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
    public virtual void Despawn(float duration)
    {
        pendingDestroy = true;

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
        gameObject.Tween(null, transform.localScale, Vector3.zero, duration, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
    public void Despawn()
    {
        Despawn(0.25f);
    }

    protected void kill()
    {
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Spawn();
        //var collider = GetComponentInChildren<SpriteButton_Collider>();
        //collider.parent = this;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
