using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cleaker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceText;
    [SerializeField] TextMeshProUGUI clickByOneClickPriceText;
    [SerializeField] TextMeshProUGUI clickByOneSecPriceText;

    [SerializeField] AudioSource clickAudio;
    [SerializeField] AudioSource upAudio;
    
    void Start() {
        Progress.progress.clickByOneClick = Progress.progress.clickByOneClick == 0 ? 1 : Progress.progress.clickByOneClick;
        Progress.progress.clickByOneClickPrice = Progress.progress.clickByOneClickPrice == 0 ? 2 : Progress.progress.clickByOneClickPrice;
        Progress.progress.clickByOneSecPrice = Progress.progress.clickByOneSecPrice == 0 ? 2 : Progress.progress.clickByOneSecPrice;
        clickByOneClickPriceText.text = $"{Progress.progress.clickByOneClickPrice}$ + {Progress.progress.clickByOneClick + 20} за клик";
        clickByOneSecPriceText.text = $"{Progress.progress.clickByOneSecPrice}$ +{Progress.progress.clickByOneSec + 10} за секунду";
        balanceText.text = Progress.progress.balance.ToString() + "$";
    }


    

    float timer = 0;
    void Update() {
        //Debug.Log(timer);
        if (timer < 0) {
            Progress.progress.balance += Progress.progress.clickByOneSec;
            balanceText.text = Progress.progress.balance.ToString() + "$";
            timer = 1;
        }
        else 
            timer -= Time.deltaTime;
    }

    void click() {
        Progress.progress.balance += Progress.progress.clickByOneClick;
        balanceText.text = Progress.progress.balance.ToString() + "$";
        clickAudio.Play();
    }

    void clickByOne() {
        if (Progress.progress.balance - Progress.progress.clickByOneClickPrice >= 0) { 
            upAudio.Play();
            Progress.progress.balance -= Progress.progress.clickByOneClickPrice;
            Progress.progress.clickByOneClickPrice += Progress.progress.clickByOneClickPrice;
            clickByOneClickPriceText.text = $"{Progress.progress.clickByOneClickPrice}$ + {Progress.progress.clickByOneClick + 20} за клик";
            Progress.progress.clickByOneClick += 20;
            balanceText.text = Progress.progress.balance.ToString() + "$";
        }
    }

    void autoCleaker() {
        if (Progress.progress.balance - Progress.progress.clickByOneSecPrice >= 0) { 
            upAudio.Play();
            Progress.progress.balance -= Progress.progress.clickByOneSecPrice;
            Progress.progress.clickByOneSecPrice += Progress.progress.clickByOneSecPrice;
            clickByOneSecPriceText.text = $"{Progress.progress.clickByOneSecPrice}$ +{Progress.progress.clickByOneSec + 10} за секунду";
            Progress.progress.clickByOneSec += 10;
            balanceText.text = Progress.progress.balance.ToString() + "$";
        }
    }

    void exit() {
        Progress.progress.Safe();
        SceneManager.LoadScene("dress");
    }

    public void ButtonsManager(string MODE) {
        switch (MODE) {
            case "CLICK":
                click();
            break;
            case "BYONE":
                clickByOne();
            break;
            case "AUTO":
                autoCleaker();
            break;
            case "EXIT":
                exit();
            break;
        }
    }

}
