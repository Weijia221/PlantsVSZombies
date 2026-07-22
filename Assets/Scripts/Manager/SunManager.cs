using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance {  get; private set; }
   
    [SerializeField]
    private int sunPoint;
    public int SunPoint
    {
        get { return sunPoint; }
    }
    public TextMeshProUGUI sunPointText;
    public float produceTime;
    private float produceTimer;
    public GameObject sunPrefab;
    private bool isStartProduce=false;
    private void Awake()
    {
        Instance= this;
    }
    private void Start()
    {
        UpdateSunPointText();
        //StartProduce();
    }
    private void Update()
    {
        if(isStartProduce)
        {
            ProduceSun();
        }
    }
    public void StartProduce()
    {
        isStartProduce = true;
    }
    public void StopProdeuce()
    {
        isStartProduce= false;
    }

    private void UpdateSunPointText()
    {
        sunPointText.text=SunPoint.ToString();
    }
    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText();
    }
    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText();
    }
    public Vector3 GetSunPointTextPosition()
    {
        Vector3 screenPos = sunPointText.transform.position;
        screenPos.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0;
        return worldPos;
    }
    void ProduceSun()
    {
        produceTimer += Time.deltaTime;
        if(produceTimer>produceTime)
        {
            produceTimer = 0;
            Vector3 position = new Vector3(Random.Range(-5, 6.5f), 6.2f, -1);
            GameObject go = GameObject.Instantiate(sunPrefab,position,Quaternion.identity);
            position.y = Random.Range(-4, 3f);
            go.GetComponent<Sun>().LinearTo(position);
        }
    }
}
