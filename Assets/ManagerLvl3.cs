using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagerLvl3 : MonoBehaviour
{
    [SerializeField] private GameObject Chicken;
    [SerializeField] private GameObject Ball;
    public int cantChicken;
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject X2;
    [SerializeField] private GameObject Z;
    [SerializeField] private GameObject Z2;
    [SerializeField] private GameObject positionBall;
    [SerializeField] private GameObject canvas;
    public int chickenPick=0;
    private float timeFloat=0;
    private int time=60;
    private GameObject timerText;
    public bool startTimer = false;
    void Start()
    {        
        canvas = GameObject.FindGameObjectWithTag("canvas");
        timerText = canvas.transform.GetChild(4).gameObject;
        timerText.SetActive(true);
        positionBall = GameObject.FindGameObjectWithTag("positionBallLvl3");
        Instantiate(Ball, positionBall.transform.position,Quaternion.identity);
        Z = GameObject.FindGameObjectWithTag("Zlvl3");
        Z2 = GameObject.FindGameObjectWithTag("Z2lvl3");
        X = GameObject.FindGameObjectWithTag("Xlvl3");
        X2 = GameObject.FindGameObjectWithTag("X2lvl3");
        for (int i=0;i<cantChicken;i++)
        {            
            Instantiate(Chicken, new Vector3(Random.Range(X.transform.position.x, X2.transform.position.x), X.transform.position.y, Random.Range(Z.transform.position.z, Z2.transform.position.z)),Chicken.transform.rotation);
        }
    }
    private void Update()
    {
        if (startTimer)
        {
            TimerGame();
        }
        
        Win();
        GameOver();
    }
    private void TimerGame()
    {
        timeFloat += Time.deltaTime;
        if (timeFloat >= 1)
        {
            time--;
            timeFloat = 0;
        }
        timerText.GetComponent<TMP_Text>().text = "" + time;
    }
    private void Win()
    {
        if (chickenPick == cantChicken && time>=0)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    private void GameOver()
    {
        if (time <0 && chickenPick != cantChicken)
        {
            canvas.transform.GetChild(3).gameObject.SetActive(true);
        }
    }


}
