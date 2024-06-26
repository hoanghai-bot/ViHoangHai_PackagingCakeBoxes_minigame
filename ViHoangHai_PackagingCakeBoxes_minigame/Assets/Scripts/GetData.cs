using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    public static GetData instance;



    private int _isPlay;
    public int isPlay
    {
        get => _isPlay;
        set
        {
            _isPlay = value;
            PlayerPrefs.SetInt("isPlay", 0);
        }
    }
    private int _level;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            PlayerPrefs.SetInt("level", level);
        }
    }
    private int _levelPlayed;
    public int levelPlayed
    {
        get => _levelPlayed;
        set
        {
            _levelPlayed = value;
            PlayerPrefs.SetInt("levelPlayed", levelPlayed);
        }
    }

    public List<int> star;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = false;
        isPlay = PlayerPrefs.GetInt("isPlay", 0);
        level = PlayerPrefs.GetInt("level", 1);
        levelPlayed = PlayerPrefs.GetInt("levelPlayed", 1);
        star = LoadList<int>("star");
    }

    public void SaveListStar()
    {
        SaveList<int>("star", star);


    }

    // Một lớp để lưu list
    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> list;

        public ListWrapper(List<T> list)
        {
            this.list = list;
        }
    }

    // Hàm lưu list vào PlayerPrefs
    public static void SaveList<T>(string key, List<T> list)
    {
        ListWrapper<T> listWrapper = new ListWrapper<T>(list);
        string json = JsonUtility.ToJson(listWrapper);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    // Hàm load list từ PlayerPrefs
    public static List<T> LoadList<T>(string key)
    {
        string json = PlayerPrefs.GetString(key, string.Empty);
        if (!string.IsNullOrEmpty(json))
        {
            ListWrapper<T> listWrapper = JsonUtility.FromJson<ListWrapper<T>>(json);
            return listWrapper.list;
        }
        return new List<T>();
    }
}
