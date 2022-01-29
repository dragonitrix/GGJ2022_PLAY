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

    public List<GameObject> levels_obj = new List<GameObject>();
    public int currentLevelIndex = 0;
    public Level currentLevel;

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
        //buttons.AddRange(GameObject.FindGameObjectsWithTag("Button"));
        //disable default cursor
        Cursor.visible = false;
        SpawnLevel(0);
    }

    public void NextLevel()
    {
        Debug.Log("NextLevel");
        if (currentLevelIndex >= levels_obj.Count - 1) return;
        currentLevelIndex++;
        var delay = currentLevel.DespawnLevel();
        StartCoroutine(SpawnLevelDelayCoroutine(currentLevelIndex, delay));
    }

    public void SpawnLevel(int index)
    {
        var level_obj = Instantiate(levels_obj[index]);
        currentLevel = level_obj.GetComponent<Level>();
        currentLevelIndex = index;
    }

    IEnumerator SpawnLevelDelayCoroutine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnLevel(index);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
