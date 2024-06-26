using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void NextScene(int clickPhay)
    {
        SceneManager.LoadScene("GamePlay");
        GetData.instance.isPlay = clickPhay;
    }
}
