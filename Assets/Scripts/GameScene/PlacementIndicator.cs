using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

public class PlacementIndicator : MonoBehaviour
{

    private ARRaycastManager rayManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    [SerializeField] private ARPointCloudManager arPointCloudManager;    
    [SerializeField] private bool scan = false;
    private float timeScan = 0;
    [SerializeField] private bool Initialize = false;
    [SerializeField] private TMP_Text timeCanvas;
    [SerializeField] private GameObject Ubicator;
    [SerializeField] private GameObject phone;        
    [SerializeField] private GameObject managerLvls;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        
        rayManager = FindObjectOfType<ARRaycastManager>();        
        Ubicator.SetActive(false);//DESACTIVO EL CUADRADO DE UBICACION

    }

    void Update()
    {     

        if(timeScan<10)//Tiempo para escanear
        {            
            timeScan += Time.deltaTime;
            timeCanvas.text = "ESCANEA LA SUPERFICIE UNOS SEGUNDOS";
        }
        
        if (timeScan > 10f){            
            Destroy(phone);
            scan = true;
            Ubicator.SetActive(true);//ACTIVO CUADRADO UBICACION
            timeCanvas.text = "USA EL RECUADRO PARA UBICAR EL LEVEL 1";
        }      

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);//Tiro un rayo constante hacia los planos desde la camara
        if (hits.Count > 0 && scan)//Si el rayo pega en algo
        {
            transform.position = hits[0].pose.position;//Voy poniendo la rotacion y posicion del cuadrado en ese rayo
            transform.rotation = hits[0].pose.rotation;

            if (Input.touchCount > 0)//AL presionar borrar todos los planos y dejar solo el que se presiono
            {
                if (!Initialize)
                {
                    var planeGO = arPlaneManager.GetPlane(hits[0].trackableId).gameObject;
                    planeGO.name = "planes";
                    var array = GameObject.FindGameObjectsWithTag("plane");
                    for (int i = 0; i < array.Length; i++)//array con todo los planos
                    {
                        if (array[i].name != "planes")
                        {
                            Destroy(array[i].gameObject);
                        }
                    }
                    timeCanvas.text = "";
                    var lvl1 = Instantiate(managerLvls.GetComponent<ManagerLevels>().listaNiveles[Singleton.Level]);//Instancio primer nivel
                    lvl1.transform.position = hits[0].pose.position;
                    Singleton.positionLevels = hits[0].pose.position;//guardo posiciones en el singleton para los demas niveles
                    lvl1.transform.rotation = Ubicator.transform.rotation;
                    Singleton.rotationLevels = Ubicator.transform.rotation;
                    lvl1.transform.SetParent(null);                    
                    arPlaneManager.enabled = false;
                    arPointCloudManager.enabled = false;
                    Destroy(planeGO);
                    var arraypoint = GameObject.FindGameObjectsWithTag("pointCloud");
                    for(int i = 0; i < arraypoint.Length; i++)
                    {
                        Destroy(arraypoint[i]);
                    }
                    Destroy(arPointCloudManager);
                    Destroy(gameObject);
                    Initialize = true;

                }

            }

        }

    }

    


}