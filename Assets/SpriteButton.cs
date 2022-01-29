using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButton : MonoBehaviour
{
    public void OnPointerOver(Pointer pointer)
    {
        Debug.Log("OnPointerOver " + pointer.id);
    }

    public void OnPointerEnter(Pointer pointer)
    {
        Debug.Log("OnPointerEnter " + pointer.id);
    }
    public void OnPointerExit(Pointer pointer)
    {
        Debug.Log("OnPointerExit " + pointer.id);
    }
    public void OnPointerClick(Pointer pointer)
    {
        Debug.Log("OnPointerClick " + pointer.id);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
