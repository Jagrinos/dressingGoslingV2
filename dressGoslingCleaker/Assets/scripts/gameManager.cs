using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public List<GameObject> hats;
    public List<GameObject> clothes;
    public List<GameObject> pants;
    public List<GameObject> shoes;
    [SerializeField] List<GameObject> windows;
    [SerializeField] GameObject[] choise;
    [SerializeField] GameObject back;
    [SerializeField] AudioSource putOn;
    [SerializeField] AudioSource wrong;

    [SerializeField] List<GameObject> itemsToBuy;

    [SerializeField] TextMeshProUGUI balanceText;

    public void ToCleaker() {
        Progress.progress.Safe();
        SceneManager.LoadScene("cleaker");
    }

    [SerializeField] List<GameObject> ScoreObjects;
    [SerializeField] List<GameObject> ToScoreObjects;
    [SerializeField] List<GameObject> ToScoreObjectsCanvas;
    [SerializeField] TextMeshProUGUI ScoreText;

    bool isScore;
    private void Update()
    {
        if (isScore)
        {
            ScorePoint();
        }
        
    }

    int scorePointsMax;
    float scorePoints = 0;

    int ScorePointMaxFunc()
    {
        int wearobj = 0;
        
        wearobj = 
            hats.Where(a => a.activeSelf == true).Count() + 
            clothes.Where(a => a.activeSelf == true).Count() +
            pants.Where(a => a.activeSelf == true).Count() +
            shoes.Where(a => a.activeSelf == true).Count()
            ;

        return wearobj * Random.Range(1, 1000);
    }
    void ScorePoint()
    {
        if (scorePoints < scorePointsMax)
            scorePoints += Time.deltaTime * scorePointsMax/5;
        ScoreText.text = scorePoints.ToString();
        //
    }
    public void ToScore()
    {
        foreach (var item in ToScoreObjectsCanvas)
        {
            item.SetActive(false);
        }
        foreach(var item in ToScoreObjects)
        {
            item.SetActive(true);
        }
        foreach (var item in ScoreObjects)
        {
            item.SetActive(true);
        }
        scorePointsMax = ScorePointMaxFunc();
        scorePoints = 0;
        isScore = true;
    }

    public void BackFromScore()
    {
        isScore = false;
        scorePointsMax = ScorePointMaxFunc();
        scorePoints = 0;
        foreach (var item in ToScoreObjectsCanvas)
        {
            item.SetActive(true);
        }
        foreach (var item in ScoreObjects)
        {
            item.SetActive(false);
        }
        foreach (var window in windows)
        {
            window.SetActive(false);
        }
        onChoise();
        back.SetActive(false);
    }

    void balanceChange() {
        balanceText.text = "БАЛАНС: " + Progress.progress.balance.ToString() + "$";
    }

    void Start() {
       balanceChange();
       firstCheckItemsOnBuy(itemsToBuy);
    }

    void firstCheckItemsOnBuy(List<GameObject> items) {
        foreach (var item in items) {
            if (!isBuyItem(item.name)){
                item.GetComponent<Image>().color = new Color(255,255,255,0.42f);
                item.transform.Find("price").gameObject.SetActive(true);        
            }
        }
    }

    bool isBuyItem(string title) {
        foreach (var obj in Progress.progress.buyThings) {
            if (obj == title) 
                return true;
        }
        return false;
    }

    bool BuyItem(string item) {
        foreach (var findItem in itemsToBuy) {
            if (item == findItem.name) {
                if (Progress.progress.balance - int.Parse(findItem.transform.Find("price").GetComponent<TextMeshProUGUI>().text) >= 0) {
                    Progress.progress.buyThings.Add(item);
                    Progress.progress.balance -= int.Parse(findItem.transform.Find("price").GetComponent<TextMeshProUGUI>().text);
                    balanceChange();
                    findItem.transform.Find("price").gameObject.SetActive(false);
                    findItem.GetComponent<Image>().color = new Color(255,255,255,1);
                    return true;
                }
            }
        }
        return false;
    }

    public void Hat(string title)
    {
        foreach (var hat in hats)
        {
            if (hat.name == title)
            {
                if (!isBuyItem(hat.name))
                    if(!BuyItem(hat.name))
                    {
                        wrong.Play();
                        return;
                    }
                if (hat.activeSelf == true)
                        hat.SetActive(false);
                else
                        hat.SetActive(true);
            }
            else
                hat.SetActive(false);
        }
        putOn.Play();
    }

    public void Clothes(string title)
    {
        foreach (var clothe in clothes)
        {
            if (clothe.name == title)
            {
                if (!isBuyItem(clothe.name))
                    if(!BuyItem(clothe.name))
                    {
                        wrong.Play();
                        return;
                    }
                if (clothe.activeSelf == true)
                    clothe.SetActive(false);
                else
                    clothe.SetActive(true);
            }
            else
                clothe.SetActive(false);
        }
        putOn.Play();
    }

    public void Pants(string title)
    {
        foreach (var pant in pants)
        {
            if (pant.name == title)
            {
                if (!isBuyItem(pant.name))
                    if(!BuyItem(pant.name))
                    {
                        wrong.Play();
                        return;
                    }
                if (pant.activeSelf == true)
                    pant.SetActive(false);
                else
                    pant.SetActive(true);
            }
            else
                pant.SetActive(false);
        }
        putOn.Play();
    }

    public void Shoes(string title)
    {
        foreach (var shoe in shoes)
        {
            if (shoe.name == title)
            {
                if (!isBuyItem(shoe.name))
                    if(!BuyItem(shoe.name))
                    {
                        wrong.Play();
                        return;
                    }
                if (shoe.activeSelf == true)
                    shoe.SetActive(false);
                else
                    shoe.SetActive(true);
            }
            else
                shoe.SetActive(false);
        }
        putOn.Play();
    }



    public void Skip()
    {
        //goslingFull.transform.parent = null;
        //DontDestroyOnLoad(goslingFull);
        //SceneManager.LoadScene("score");
    }

    void offChoise() {
        foreach (var cho in choise) {
            cho.SetActive(false);
        }
    }

    void onChoise() {
        foreach (var cho in choise) {
            cho.SetActive(true);
        }
    }


    public void Back()
    {
        foreach (var window in windows)
        {
            window.SetActive(false);
        }
        onChoise();
        back.SetActive(false);
    }
    public void HatChoise()
    {
        offChoise();
        foreach (var window in windows)
        {
            if (window.name == "hats")
            {
                window.SetActive(true);
            }
            else
                window.SetActive(false);
        }
        back.SetActive(true);
    }

    public void CloseChoise()
    {
        offChoise();
        foreach (var window in windows)
        {
            if (window.name == "clothes")
            {
                window.SetActive(true);
            }
            else
                window.SetActive(false);
        }
        back.SetActive(true);

    }

    public void PantsChoise()
    {
        offChoise();
        foreach (var window in windows)
        {
            if (window.name == "pants")
            {
                window.SetActive(true);
            }
            else
                window.SetActive(false);
        }
        back.SetActive(true);

    }

    public void ShoesChoise()
    {
        offChoise();
        foreach (var window in windows)
        {
            if (window.name == "shoes")
            {
                window.SetActive(true);
            }
            else
                window.SetActive(false);
        }
        back.SetActive(true);

    }


    
}
