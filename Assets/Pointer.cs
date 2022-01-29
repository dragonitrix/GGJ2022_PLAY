using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pointer : MonoBehaviour
{

    public string id = "Ying";

    GameObject currentHoverObj = null;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Button")
        {
            currentHoverObj = collider.gameObject;
            var btn = currentHoverObj.GetComponent<SpriteButton>();
            btn.OnPointerEnter(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Button")
        {
            if (currentHoverObj == collider.gameObject)
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
        var target = Vector3.zero;
        transform.up = target - transform.position;
        

        if (Input.GetMouseButtonDown(0))
        {
            if (currentHoverObj != null)
            {
                var btn = currentHoverObj.GetComponent<SpriteButton>();
                btn.OnPointerClick(this);
            }
        }



    }

    public virtual void UpdatePointerPos()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);

    }

}
