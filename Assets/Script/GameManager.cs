using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using DigitalRuby.Tween;

using TMPro;

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
    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    //DontDestroyOnLoad(this.gameObject); // game only has 1 scene. no need to dontDestroyOnload
        //}
        //else if (instance != this)
        //{
        //    Destroy(this);
        //}
        
        if (instance != this)
        {
            Destroy(instance);
        }
        instance = this;

    }

    [Header("Statistics")]

    public float totalPlaytime = 0;

    public int totalClick = 0;
    public int ButtonClick = 0;

    public float accuracy = 0f;

    [Header("UI")]

    public CanvasGroup endPanel;
    public Animator endShutter;
    public TextMeshProUGUI playTimeText;
    public TextMeshProUGUI accText;

    [Header("pointer")]

    public Pointer pointer_Yin;
    public Pointer pointer_Yang;

    [Header("properties")]
    public bool isGameOver = false;

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

    public void ShowPointers()
    {
        pointer_Yin.FullShow();
        pointer_Yang.FullShow();
    }


    public List<GameObject> buttons = new List<GameObject>();

    public List<GameObject> levels_obj = new List<GameObject>();
    public int currentLevelIndex = 0;
    public Level currentLevel;

    public GameObject ripple_Prefabs;

    public void SpawnSimpleRipple(Transform transform, Vector3 size, Color color, float duration)
    {
        if (isGameOver) return;
        var ripple = Instantiate(ripple_Prefabs, transform);
        ripple.transform.position = transform.position;
        ripple.GetComponent<SimpleRipple>().Ripple(size, color, duration);
    }


    // Start is called before the first frame update
    void Start()
    {
        //buttons.AddRange(GameObject.FindGameObjectsWithTag("Button"));
        //disable default cursor
        Cursor.visible = false;

        pointer_Yin.LockRotation(30f);
        //pointer_Yang.gameObject.SetActive(false);

        SpawnLevel(0);
    }

    public void NextLevel()
    {
        if (isGameOver) return;

        if (currentLevelIndex == 0) //first level pass, start the true game.
        {
            //pointer_Yang.gameObject.SetActive(true);
            pointer_Yang.FullShow();
            pointer_Yin.UnlockRotation();
        }
        //Debug.Log("NextLevel");
        var delay = currentLevel.DespawnLevel();
        if (currentLevelIndex >= levels_obj.Count - 1)
        {
            Debug.Log("Reach the END");
            this.isGameOver = true;
            GameOver();
            return;
        }

        AudioManager.instance.PlaySound("Pass",1);

        currentLevelIndex++;
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

    public void GameOver()
    {
        AudioManager.instance.PlaySound("Off", 1);
        AudioManager.instance.PlayBGM();
        
        endShutter.SetTrigger("close");

        playTimeText.text = Mathf.Round(totalPlaytime).ToString() + " sec";
        accText.text = Mathf.Round(accuracy).ToString() + "%";


        System.Action<ITween<float>> tweenUpdate = (t) =>
        {
            endPanel.alpha = t.CurrentValue;
        };

        System.Action<ITween<float>> tweenCompleted = (t) =>
        {
            endPanel.interactable = true;
            endPanel.blocksRaycasts = true;
            Cursor.visible = true;
        };

        // completion defaults to null if not passed in
        gameObject.Tween(null, 0, 1, 5, TweenScaleFunctions.CubicEaseIn, tweenUpdate, tweenCompleted);

    }


    public void onPlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevelIndex != 0 && !isGameOver)
        {
            totalPlaytime += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {


            totalClick++;
            if (pointer_Yin.currentHoverObj != null || pointer_Yang.currentHoverObj != null)
            {
                ButtonClick++;
                AudioManager.instance.PlaySound("ClickBtn");
            }
            else
            {
                AudioManager.instance.PlaySound("Click");
            }
            accuracy = Mathf.Round(((float)ButtonClick / (float)totalClick) * 100);
        }

    }
}
