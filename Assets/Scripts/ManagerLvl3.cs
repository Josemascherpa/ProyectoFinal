using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ManagerLvl3 : MonoBehaviour
{
    [SerializeField] private GameObject Chicken;
    [SerializeField] private GameObject Ball;
    public int cantChicken;
    private GameObject X;
    private GameObject X2;
    private GameObject Z;
    private GameObject Z2;
    [SerializeField] private GameObject positionBall;
    [SerializeField] private GameObject canvas;
    public int chickenPick=0;
    private float timeFloat=0;
    private int time=60;
    private GameObject timerText;
    public bool startTimer = false;
    private GameObject fox;
    private bool setSpeed = false;
    private bool instaChicken = false;
    private GameObject targetFox;
    void Start()
    {
        targetFox = GameObject.FindGameObjectWithTag("targetFox");
        positionBall = GameObject.FindGameObjectWithTag("positionBallLvl3");
        Instantiate(Ball, positionBall.transform.position,Quaternion.identity);                
        canvas = GameObject.FindGameObjectWithTag("canvas");
        timerText = canvas.transform.GetChild(4).gameObject;
        timerText.SetActive(true);
    }
    private void Update()
    {
        Z = GameObject.FindGameObjectWithTag("Zlvl3");
        Z2 = GameObject.FindGameObjectWithTag("Z2lvl3");
        X = GameObject.FindGameObjectWithTag("Xlvl3");
        X2 = GameObject.FindGameObjectWithTag("X2lvl3");
        if (!instaChicken)
        {
            for (int i = 0; i < cantChicken; i++)
            {                     
                var chicken = Instantiate(Chicken,this.transform);
                chicken.transform.position = new Vector3(Random.Range(X.transform.position.x, X2.transform.position.x), X.transform.position.y, Random.Range(Z.transform.position.z, Z2.transform.position.z));
            }
            instaChicken = true;
        }                
        if (!setSpeed)
        {           
            fox = GameObject.FindGameObjectWithTag("fox");
            fox.GetComponent<TouchFox>().SetSpeed(2);
            fox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            setSpeed = true; 
        }
        if (startTimer)
        {
            TimerGame();
        }
        if (time <= 0)
        {
            Win();
            GameOver();
        }        
    }
    private void TimerGame()
    {
        if (time >= 0 && fox != null)
        {
            timeFloat += Time.deltaTime;
        }        
        if (timeFloat >= 1)
        {
            time--;
            timeFloat = 0;
        }
        timerText.GetComponent<TMP_Text>().text = "" + time;
    }
    private void Win()
    {
        if (chickenPick == cantChicken)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            if (fox != null) {
                
                Destroy(fox);
                targetFox.transform.SetParent(null);
                var ball = GameObject.FindGameObjectWithTag("ball");
                Destroy(ball);
                var level = GameObject.FindGameObjectWithTag("level");
                Destroy(level);                
            }
        }
    }
    private void GameOver()
    {
        if (chickenPick != cantChicken)
        {
            timerText.GetComponent<TMP_Text>().text = "";
            time = 60;            
            canvas.transform.GetChild(3).gameObject.SetActive(true);
            if (fox != null)
            {
                Destroy(fox);
                var ball = GameObject.FindGameObjectWithTag("ball");
                Destroy(ball);
                var level = GameObject.FindGameObjectWithTag("level");
                Destroy(level);
            }            
        }
    }


}
