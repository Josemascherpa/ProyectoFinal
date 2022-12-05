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
    /*private GameObject X;
    private GameObject X2;
    private GameObject Z;
    private GameObject Z2;*/
    [SerializeField] private GameObject positionBall;
    [SerializeField] private GameObject canvas;
    public int chickenPick=0;
    private float timeFloat=0;
    private int time=63;
    private GameObject timerText;
    public bool startTimer = false;
    private GameObject fox;
    private bool setSpeed = false;
    private bool instaChicken = false;   
    private GameObject ballInGame;    
    private Vector3 positionInitialBall;
    private GameObject level;
    void Start()
    {
        print("CARGO EL LVL 2 START");
        
        positionBall = GameObject.FindGameObjectWithTag("positionBallLvl3");
        ballInGame = Instantiate(Ball, positionBall.transform.position,Quaternion.identity);
        positionInitialBall = ballInGame.transform.position;
        level = GameObject.FindGameObjectWithTag("level");
        ballInGame.transform.SetParent(level.transform);
        canvas = GameObject.FindGameObjectWithTag("canvas");
        timerText = canvas.transform.GetChild(4).gameObject;
        
        
    }
    private void Update()
    {       
        if(ballInGame.transform.position != positionInitialBall && !startTimer)
        {            
            startTimer = true;
        }
        if (ballInGame == null){
            ballInGame = Instantiate(Ball, positionBall.transform.position, Quaternion.identity);
            ballInGame.transform.SetParent(level.transform);
        }
        
        
        if (!instaChicken)
        {
            for (int i = 0; i < cantChicken; i++)
            {
                var Z = GameObject.FindGameObjectWithTag("Zlvl3");
                var Z2 = GameObject.FindGameObjectWithTag("Z2lvl3");
                var X = GameObject.FindGameObjectWithTag("Xlvl3");
                var X2 = GameObject.FindGameObjectWithTag("X2lvl3");
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
        if (time <= 60)
        {
            timerText.SetActive(true);
        }
        timerText.GetComponent<TMP_Text>().text = "" + time;
    }
    private void Win()
    {
        if (chickenPick == cantChicken)
        {
            canvas.transform.GetChild(2).gameObject.SetActive(true);            
            timerText.SetActive(false);
            StartCoroutine(fox.GetComponent<TouchFox>().DestroyLevelAndFox(0.1f));
            

        }
    }
    private void GameOver()
    {
        if (chickenPick != cantChicken)
        {
            StartCoroutine(fox.GetComponent<TouchFox>().DestroyLevelAndFox(0.1f));
            timerText.SetActive(false);                           
            canvas.transform.GetChild(3).gameObject.SetActive(true);                   
        }
    }


}
