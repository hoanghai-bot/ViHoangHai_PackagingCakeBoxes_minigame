using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerLevel : MonoBehaviour
{

    private int countData = 0;
    public GameObject prefabButton;
    public Transform content;

    public Sprite starWin;
    // Start is called before the first frame update
    void Start()
    {
        CheckData();
        for (int i = 0; i < countData; i++)
        {
            var temp = Instantiate(prefabButton, content);
            temp.SetActive(true);
            var levelText = temp.GetComponentInChildren<TextMeshProUGUI>();
            levelText.text = (i + 1).ToString();
            if ((i + 1) > GetData.instance.levelPlayed)
            {

                temp.transform.Find("lock").gameObject.SetActive(true);
                temp.transform.Find("Button").GetComponent<Button>().enabled = false;
                levelText.gameObject.SetActive(false);
            }
            else
            {
                temp.transform.Find("lock").gameObject.SetActive(false);
                temp.transform.Find("Button").GetComponent<Button>().enabled = true;
                if (i>GetData.instance.star.Count-1)continue;
                switch(GetData.instance.star[i]) {
                    case 1:
                        temp.transform.Find("star1").GetComponent<Image>().sprite = starWin;
                        break;
                    case 2:
                        temp.transform.Find("star1").GetComponent<Image>().sprite = starWin;
                        temp.transform.Find("star2").GetComponent<Image>().sprite = starWin;
                        break;
                    case 3:
                        temp.transform.Find("star1").GetComponent<Image>().sprite = starWin;
                        temp.transform.Find("star2").GetComponent<Image>().sprite = starWin;
                        temp.transform.Find("star3").GetComponent<Image>().sprite = starWin;
                        break;
                      
                }
            }
        }

    }



    public void ActiveLevel(TextMeshProUGUI text)
    {
        GetData.instance.level = int.Parse(text.text);
        GetData.instance.isPlay = 2;
        SceneManager.LoadScene("GamePlay");
    }

    private void CheckData()
    {

        // Số lượng tệp có định dạng không phải là meta
        var files = Resources.LoadAll("NewLevel");
        foreach (var file in files)
        {

            countData++;
        }

        Debug.Log("Số lượng tài nguyên trong thư mục Resources là: " + countData);

    }
}
