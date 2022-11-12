using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private RectTransform hueso;
    private Vector3 velocity = new Vector3(1,1,1);
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        Singleton.touchPlayMainMenu = false;
    }
    private void Update()
    {
        hueso.sizeDelta = Vector3.SmoothDamp(hueso.sizeDelta, new Vector3(893.2603f, 517.2282f,0), ref velocity, 0.8f);
        if (Input.touchCount > 0 && Singleton.touchPlayMainMenu)
        {
            SceneManager.LoadScene("Game");      
        }

    }
    public void Exit(){        
        Application.Quit();
    }

    
}
