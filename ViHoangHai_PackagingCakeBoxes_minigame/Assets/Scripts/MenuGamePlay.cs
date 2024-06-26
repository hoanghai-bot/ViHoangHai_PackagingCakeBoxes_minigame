using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGamePlay : MonoBehaviour
{
    public static MenuGamePlay instance;
    public GameObject guidTable;
    public GameObject levelTable;
    public GameObject menuTable;

    public GameObject PupUpLost;
    public GameObject PupUpWin;

    public GameObject star2;
    public GameObject star1;
    public Sprite starLost;

    private GameObject levelGame;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if(GetData.instance.isPlay == 0)
        {
            guidTable.SetActive(true);
            
        }
        if(GetData.instance.isPlay == 1)
        {
            levelTable.SetActive(true);
        }
        if (GetData.instance.isPlay == 2)
        {
            menuTable.SetActive(true);
            LoadLevelGame();
            
        }
    }

    public void LoadLevelGame()
    {

        levelGame = Instantiate(Resources.Load("NewLevel/Level " + GetData.instance.level) as GameObject, transform);
        ResetLevel();
    }

    private void ResetLevel()
    {
        
    }
    public void LoadScene(string name)
    {
        GetData.instance.isPlay = 2;
        SceneManager.LoadScene(name);
    }

    public void LoadReset()
    {
        GetData.instance.level--;
        SceneManager.LoadScene("GamePlay");
    }

    public void LostEvent()
    {
        PupUpLost.SetActive(true);
        GameController.instance.enabled = false;
    }

    public void WinEvent()
    {
        int countStar = 3;
        int levelCurrent = GetData.instance.level;
        GameController.instance.StopCoron();
        if (GameController.instance.time < 30)
        {
            star2.GetComponent<Image>().sprite = starLost;
            countStar--;
        }
        if (GameController.instance.time < 15)
        {
            star1.GetComponent<Image>().sprite= starLost;
            countStar--;
        }
        DOTween.Sequence()
            .AppendInterval(0.5f)
            .AppendCallback(() => { PupUpWin.SetActive(true); })
            .Play();
        
        GetData.instance.level++;
        if (GetData.instance.level > GetData.instance.levelPlayed)
        {
            GetData.instance.levelPlayed = GetData.instance.level;
            GetData.instance.star.Add(countStar);
        }
        else
        {
            if (countStar > GetData.instance.star[levelCurrent-1])
            {
                GetData.instance.star[levelCurrent - 1] = countStar;
            }
        }
        GetData.instance.SaveListStar();
        GameController.instance.enabled = false;
    }
}
