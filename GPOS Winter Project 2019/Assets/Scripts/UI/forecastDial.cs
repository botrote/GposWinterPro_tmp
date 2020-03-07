using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forecastDial : MonoBehaviour
{
    protected float leftTime;
    protected Slider slider;
    protected bool isCounting;
    public Image[] unitImage;
    // Start is called before the first frame update
    void Awake()
    {
        isCounting = false;
        slider = gameObject.GetComponent<Slider>();
    }
    
    public void StartCountDown(string name,float time)
    {
        AddImage(name);
        if(!isCounting) StartCoroutine(CountDown(time));
    }

    protected void AddImage(string name)
    {
        int i = 0;
        while(i < 4 && unitImage[i].sprite != null)
        {
            i++;
        }
        if (i < 4) unitImage[i].sprite = Resources.Load<Sprite>("Sprites/" + name);
    }

    protected IEnumerator CountDown(float time)
    {
        isCounting = true;
        slider.maxValue = time;
        leftTime = 0;
        while (leftTime < time)
        {
            leftTime += Time.deltaTime;
            slider.value = leftTime;
            yield return null;
        }
        foreach(Image image in unitImage)
        {
            image.sprite = null;
        }
        gameObject.SetActive(false);
        isCounting = false;
    } 
}
