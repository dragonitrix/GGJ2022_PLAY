using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum MainColor
    {
        Orange,
        Blue,
        BG
    }

    public Color GetColorValue(MainColor mainColor)
    {
        switch (mainColor)
        {
            case MainColor.Orange:
                return new Color(255f / 255f, 124f / 255f, 12f / 255f);
            case MainColor.Blue:
                return new Color(52f / 255f, 156f / 255f, 255f / 255f);
            case MainColor.BG:
                return new Color(13f / 255f, 23f / 255f, 42f / 255f);
            default:
                return Color.white;
        }
    }

    public static GameManager instance;

    public Pointer pointer_Yin;
    public Pointer pointer_Yang;

    public int GetPointerCurrentHoverCount()
    {
        int currentHoverCount = 0;
        if (pointer_Yin.currentHoverObj != null)
        {
            currentHoverCount++;
        }
        if (pointer_Yang.currentHoverObj != null)
        {
            currentHoverCount++;
        }
        return currentHoverCount;
    }
    public void HidePointerBorder()
    {
        pointer_Yin.HidePointerBorder();
        pointer_Yang.HidePointerBorder();
    }
    public void ShowPointerBorder()
    {
        pointer_Yin.ShowPointerBorder();
        pointer_Yang.ShowPointerBorder();
    }

    public List<GameObject> buttons = new List<GameObject>();

    public List<GameObject> levels_obj = new List<GameObject>();
    public int currentLevelIndex = 0;
    public Level currentLevel;

    public GameObject ripple_Prefabs;

    public void SpawnSimpleRipple(Transform transform, Vector3 size, Color color, float duration)
    {
        var ripple = Instantiate(ripple_Prefabs, transform);
        ripple.transform.position = transform.position;
        ripple.GetComponent<SimpleRipple>().Ripple(size, color, duration);
    }

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

        pointer_Yin.LockRotation(30f);
        pointer_Yang.gameObject.SetActive(false);

        SpawnLevel(0);
    }

    public void NextLevel()
    {
        if (currentLevelIndex == 0) //first level pass, start the true game.
        {
            pointer_Yang.gameObject.SetActive(true);
            pointer_Yin.UnlockRotation();
        }
        //Debug.Log("NextLevel");
        if (currentLevelIndex >= levels_obj.Count - 1)
        {
            Debug.Log("Reach the END");
            return;
        }
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
