using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;



public class TouchFox : MonoBehaviour
{
    
    [SerializeField] bool move = false;    
    public static bool UIDetect = false;   //DETECTAR CUANDO UN RAYO TOCA UN CANVAS O EL JUEGO
    private Rigidbody rb;
    private Animator anim;    
    private bool Idle = true;
    private bool Walk = false;
    public bool iniciateMove = false;//INICIAR MOVIMIENTO en terrain scripts
    [SerializeField] LayerMask evitarRayos;    
    [SerializeField]private GameObject canvas;      
    [SerializeField] private GameObject reinicioLvl;
    [SerializeField] private GameObject proxLevel;    
    private float SPEED = 1;
    private GameObject target;
    [SerializeField] private GameObject prefabTarget;
    private float speedElevation = 0.2f;
    void Start()
    {        
        target = GameObject.FindGameObjectWithTag("targetFox");
        target.transform.position = this.transform.position;
        canvas = GameObject.FindGameObjectWithTag("canvas");        
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        reinicioLvl = canvas.transform.GetChild(3).gameObject;
        proxLevel = canvas.transform.GetChild(2).gameObject;
    }
    
    void Update()
    {    
       
        if (target == null)//Si target es null, lo vuelvo a instanciar
        {            
            Instantiate(prefabTarget);
            target = GameObject.FindGameObjectWithTag("targetFox");
            target.transform.position = this.transform.position;
        }        
        if (Vector3.Distance(this.transform.position,target.transform.position)<0.001)//Distancia entre el target y la posicion para las animaciones
        {            
            Walk = false;
            Idle = true;
            move = false;
        }
        else if(Vector3.Distance(this.transform.position, target.transform.position) > 0.02f)
        {            
            Walk = true;
            Idle = false;
        }
        IsPointerOverUIObject();//Verifico que no se presione en un canvas 
        Touch();//Raycast para mover
        anim.SetBool("Idle", Idle);        
        anim.SetBool("Walk", Walk);       
        
    }
    private void FixedUpdate()
    {
        if (move)//movimineto por rigidbody
        {
            Movement(target.transform.position);
        }
            
    }
    private void OnCollisionEnter(Collision collision)
    {               
             
        if (collision.gameObject.CompareTag("ball"))
        {            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 0.6f, ForceMode.Impulse);            
        }
        if (collision.gameObject.CompareTag("lava") || collision.gameObject.CompareTag("arrow") || collision.gameObject.CompareTag("tronco"))
        {
            move = false;
            StartCoroutine(DestroyLevelAndFox(0.01f));
            reinicioLvl.SetActive(true);
            rb.detectCollisions = false;
        }
        if (collision.gameObject.CompareTag("NextLevel"))
        {            
            Walk = false;
            Idle = true;
            StartCoroutine(DestroyLevelAndFox(0.01f));
            proxLevel.SetActive(true);
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("plataformaMov"))
        {
            this.transform.SetParent(collision.gameObject.transform);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("plataformaMov"))
        {
            this.transform.SetParent(null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("elevation"))
        {            
            rb.AddForce(new Vector3(0, 1f, 0) * speedElevation, ForceMode.Impulse);
        }
    }

    void Movement(Vector3 target)
    {        
        rb.MovePosition(Vector3.MoveTowards(this.transform.position, new Vector3(target.x, this.transform.position.y, target.z), 0.1f * SPEED * Time.deltaTime));//SPEED
    }
    void Touch()
    {
        if (Input.touchCount > 0 && iniciateMove)
        {            
            Touch toque = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(toque.position);
            RaycastHit hit;            
            canvas.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            if (Physics.Raycast(ray, out hit,100, ~evitarRayos) && !UIDetect)//Ciertas capas evitables
            {                
                if (!hit.collider.CompareTag("fox"))//Si el rayo lanzado no choca con fox
                {                    
                       target.transform.SetParent(null);
                       target.transform.position = hit.point;//pongo target donde colisiono el rayo
                        target.transform.GetChild(0).gameObject.SetActive(true);//activo el marcador del target
                       var rotatition = target.transform.position-this.transform.position;
                       rotatition.y = 0;
                       this.transform.rotation = Quaternion.LookRotation(rotatition);//Y roto el lobo hacia el target
                       move = true;                       

                }
            }
        }
    }   

    private void IsPointerOverUIObject()//DETECTO UI, lanzo ryao antes que el otro raycast, para detectar si se toco o no un boton o algo del UI
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
        {
            UIDetect = true;
        }
        else
        {
            UIDetect = false;
        }
    }   

    public IEnumerator DestroyLevelAndFox(float timeDestroy)//Corrutina para eliminar al cambiar o reiniciar los niveles
    {
        yield return new WaitForSeconds(timeDestroy);        
        var level = GameObject.FindGameObjectWithTag("level");
        target.transform.SetParent(null);
        Destroy(level);        
        Destroy(this.gameObject);        
    }

    public void SetSpeed(float speedNew)//metodo para setear la velocidad del lobo
    {
        SPEED = speedNew;
    }
    

}
