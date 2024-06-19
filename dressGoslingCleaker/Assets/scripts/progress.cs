using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress progress;
    void Awake() {
        if (progress == null) {
            progress = this;
            DontDestroyOnLoad(progress);
        }
    } 

    //clicker
    public int clickByOneClick;
    public int clickByOneClickPrice;
    public int clickByOneSec;
    public int clickByOneSecPrice;
    //clicker
    public int balance;
    public List<string> buyThings;

    public void Safe() {
        PlayerPrefs.SetInt("clickByOneClick", clickByOneClick);
        PlayerPrefs.SetInt("clickByOneSec", clickByOneSec);
        PlayerPrefs.SetInt("clickByOneClickPrice", clickByOneClickPrice);
        PlayerPrefs.SetInt("clickByOneSecPrice", clickByOneSecPrice);
        PlayerPrefs.SetInt("balance", balance);
        for (int i = 0; i < buyThings.Count; i++)
            PlayerPrefs.SetString("buyThings" + i.ToString(), buyThings[i]);
        PlayerPrefs.Save();
    }

    public void Load() {
        clickByOneClick = PlayerPrefs.GetInt("clickByOneClick");
        clickByOneSec = PlayerPrefs.GetInt("clickByOneSec");

        balance = PlayerPrefs.GetInt("balance");
        for (int i = 0; i<buyThings.Count; i++)
            buyThings.Add(PlayerPrefs.GetString("buyThings" + i.ToString()));
    }
}
