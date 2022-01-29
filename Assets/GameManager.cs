using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pointer_Yin;
    public GameObject pointer_Yang;

    public List<GameObject> buttons = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        buttons.AddRange(GameObject.FindGameObjectsWithTag("Button"));


        //disable default cursor
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
