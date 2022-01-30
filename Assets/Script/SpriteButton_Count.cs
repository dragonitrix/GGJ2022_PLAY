using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SpriteButton_Count : SpriteButton
{

    public int max_count = 0;
    public int current_count = 0;

    List<TextMeshPro> textList = new List<TextMeshPro>();

    private void Awake()
    {
        textList.AddRange(GetComponentsInChildren<TextMeshPro>());
        max_count = textList.Count;
    }

    public override void CheckCondition(Pointer pointer)
    {
        current_count++;
        
        if (current_count > max_count)
        {
            current_count = 0;
        }

        if (current_count == max_count)
        {
            condition = true;
        }
        else
        {
            condition = false;
        }
    }

    public override void SetTextColorByCondition(Pointer pointer)
    {

        if (current_count == 0)
        {
            for (int i = 0; i < textList.Count; i++)
            {
                textList[i].color = Color.white;
            }

        }
        else
        {
            textList[current_count-1].color = GameManager.instance.GetColorValue(pointer.pointerColor);
        }

        //for (int i = 0; i < textList.Count; i++)
        //{
        //    if (i < current_count)
        //    {
        //        textList[i].color = GameManager.instance.GetColorValue(pointer.pointerColor);
        //    }
        //    else
        //    {
        //        textList[i].color = Color.white;
        //    }
        //}
    }

}
