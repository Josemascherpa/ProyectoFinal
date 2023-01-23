using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private RectTransform hueso;
    private Vector3 velocity = new Vector3(1,1,1);
    private bool sonido = true;
    private GameObject audio;
    private bool UIdetect = false;
    private GameObject sonidoGO;
    private bool partidaDetectada = false;
    private void Awake()
    {
        sonidoGO = this.transform.GetChild(6).gameObject;
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio");
        Singleton.touchPlayMainMenu = false;

        if (SaveManager.loadLevel()!=null)//VERIFICO SI EXISTE PARTIDA
        {
            partidaDetectada = true;           
            this.transform.GetChild(8).gameObject.SetActive(true);            
        }
        
    }
    private void Update()
    {
        IsPointerOverUIObject();
        if (sonido)
        {
            audio.GetComponent<AudioSource>().UnPause();
        }
        else if (!sonido)
        {
            audio.GetComponent<AudioSource>().Pause();
        }
        hueso.sizeDelta = Vector3.SmoothDamp(hueso.sizeDelta, new Vector3(500f, 250f,0), ref velocity, 0.8f);
        if (Input.touchCount > 0 && Singleton.touchPlayMainMenu && !UIdetect && !partidaDetectada )
        {
            SceneManager.LoadScene("Game");      
        }
        sonidoGO.GetComponent<Animator>().SetBool("sonido", sonido);
    }
    public void Exit(){        
        Application.Quit();
    }

    public void Silencio()
    {
        sonido =! sonido;
    }
    private void IsPointerOverUIObject()//DETECTO UI 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
        {
            UIdetect = true;
        }
        else
        {
            UIdetect = false;
        }
    }
    public void Instagram()
    {
        Application.OpenURL("https://instagram.com/lasaventurasdeheiko");
    }

    public void CargarPartida()
    {
        Singleton.Level = SaveManager.loadLevel().level;//Igualo al singleton para que cargue ese nivel
        partidaDetectada = false;
        this.transform.GetChild(8).gameObject.SetActive(false);
        
    }
    public void EliminarPartida()
    {
        SaveManager.eliminateData();//Elimino el archivo
        partidaDetectada = false;
        this.transform.GetChild(8).gameObject.SetActive(false);
    }

}
