using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum CardState
{
    Disable,
    Cooling,
    WaitingSun,
    Ready
}
public enum PlantType
{
    Sunflower,
    PeaShooter
}
public class Card : MonoBehaviour
{
    //冷却 可以被点击 不可用
    private CardState cardState = CardState.Disable;
    public PlantType plantType= PlantType.Sunflower;
    public GameObject cardLight;
    public GameObject cardGray;
    public Image cardMask;
    [SerializeField]
    private float cdTime = 2;
    private float cdTimer = 0;
    [SerializeField]
    private int needSunPoint=50;
    private void Update()
    {
        switch (cardState)
        {
            case CardState.Cooling:
                CoolingUpdate();
                break;
            case CardState.WaitingSun:
                WaitingSunUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
            default:
                break;
        }
    }
    void CoolingUpdate()
    {
        cdTimer += Time.deltaTime;
        cardMask.fillAmount = (cdTime - cdTimer) / cdTime;
        if(cdTimer>=cdTime)
        {
            transitionToWaitingSun();
        }
    }
    void WaitingSunUpdate()
    {
        if (SunManager.Instance == null) return;
        if(needSunPoint<=SunManager.Instance.SunPoint)
        {
            transitionToReady();
        }
    }
    void ReadyUpdate()
    {
        if (SunManager.Instance == null) return;
        if(needSunPoint>SunManager.Instance.SunPoint)
        {
            transitionToWaitingSun();
        }
    }
    void transitionToWaitingSun()
    {
        cardState = CardState.WaitingSun;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(false);
    }
    void transitionToReady()
    {
        cardState = CardState.Ready;
        cardLight.SetActive(true);
        cardGray.SetActive(false);
        cardMask.gameObject.SetActive(false);
    }
    void transitionToCooling()
    {
        cardState = CardState.Cooling;
        cdTimer = 0;
        cardLight.SetActive(false);
        cardGray.SetActive(true);
        cardMask.gameObject.SetActive(true);
    }
    public void OnClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        if (cardState == CardState.Disable) return;
        if (SunManager.Instance == null) return;
        if (needSunPoint > SunManager.Instance.SunPoint) return;
        

        if (HandManager.Instance == null) return;
        bool isSuccess = HandManager.Instance.AddPlant(plantType);
        if(isSuccess )
        {
            SunManager.Instance.SubSun(needSunPoint);
            transitionToCooling();
        }
        

    }
    public void DisableCard()
    {
        cardState = CardState.Disable;
    }
    public void EnableCard()
    {
        transitionToCooling();
    }

}
