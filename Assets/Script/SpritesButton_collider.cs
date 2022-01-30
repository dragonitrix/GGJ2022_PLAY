using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesButton_collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        var box = GetComponent<BoxCollider2D>();

        box.size = sr.size;
    }
}
