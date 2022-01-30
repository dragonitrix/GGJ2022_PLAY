using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<SpriteButton> buttons = new List<SpriteButton>();
       
    public List<bool> conditions = new List<bool>();
    

    public void CheckCondition()
    {

        if (conditions.Count != buttons.Count) return;

        var condition = true;

        foreach (SpriteButton btn in buttons)
        {
            //Debug.Log(btn.condition);
            if (!btn.condition)
            {
                condition = false;
            }
        }

        //Debug.Log("---");

        if (condition)
        {
            //all condition fullfilled
            GameManager.instance.NextLevel();
        }

        conditions.Clear();

    }

    // Start is called before the first frame update
    void Start()
    {
        buttons.AddRange(transform.GetComponentsInChildren<SpriteButton>());

        foreach (SpriteButton btn in buttons)
        {
            btn.Spawn(this);
        }

    }

    public float DespawnLevel()
    {
        float delay = 0.25f;

        foreach (SpriteButton btn in buttons)
        {
            btn.Despawn(delay);
        }

        Destroy(this.gameObject, delay + 0.1f);

        return delay;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
