using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

[RequireComponent(typeof(Collider))]
public class Pointer : MonoBehaviour
{
    public bool lockRotation;
    public float angle = 0;
    public void LockRotation(float angle)
    {
        lockRotation = true;
        this.angle = angle;

        transform.up = Vector3.up;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
    public void UnlockRotation()
    {
        lockRotation = false;
    }

    public string id = "Yin";
    public GameManager.MainColor pointerColor;

    public GameObject currentHoverObj = null;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Button")
        {
            currentHoverObj = collider.transform.parent.gameObject;
            var btn = currentHoverObj.GetComponent<SpriteButton>();
            btn.OnPointerEnter(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Button")
        {
            if (currentHoverObj == collider.transform.parent.gameObject)
            {
                var btn = currentHoverObj.GetComponent<SpriteButton>();
                btn.OnPointerExit(this);
                currentHoverObj = null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePointerPos();

        if (!lockRotation)
        {
            var target = Vector3.zero;
            transform.up = target - transform.position;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (currentHoverObj != null)
            {
                var btn = currentHoverObj.GetComponent<SpriteButton>();
                btn.OnPointerClick(this);
            }
            GameManager.instance.SpawnSimpleRipple(transform, Vector3.one * 1.5f, GameManager.instance.GetColorValue(pointerColor), 0.25f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentHoverObj != null)
            {
                var btn = currentHoverObj.GetComponent<SpriteButton>();
                btn.OnPointerRelease(this);
            }
        }



    }

    public virtual void UpdatePointerPos()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

    }

    public void HidePointerBorder()
    {
        var pointerBorder_sr = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>(); //hardcodeeee

        var startColor = pointerBorder_sr.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        System.Action<ITween<Color>> tweenUpdate = (t) =>
        {
            pointerBorder_sr.color = t.CurrentValue;
        };

        System.Action<ITween<Color>> tweenCompleted = (t) =>
        {
        };

        gameObject.Tween(null, startColor, endColor, 0.25f, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }

    public void ShowPointerBorder()
    {
        var pointerBorder_sr = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>(); //hardcodeeee

        var startColor = pointerBorder_sr.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        System.Action<ITween<Color>> tweenUpdate = (t) =>
        {
            pointerBorder_sr.color = t.CurrentValue;
        };

        System.Action<ITween<Color>> tweenCompleted = (t) =>
        {
        };

        gameObject.Tween(null, startColor, endColor, 0.25f, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }
}
