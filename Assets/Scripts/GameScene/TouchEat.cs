using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class TouchEat : MonoBehaviour
{
    private Vector3 target;
    public bool move = false;    
    public static bool UIDetect = false;   
    private Rigidbody rb;
    private Animator anim;    
    private bool Idle = true;
    private bool Walk = false;
    public bool iniciateMove = false;

    public LayerMask evitarRayos;    
    private GameObject canvas;  
    //[SerializeField] private GameObject printss;
    [SerializeField] private GameObject reinicioLvl;
    [SerializeField] private GameObject proxLevel;
    private Quaternion rotationInicio;
    // Start is called before the first frame update
    
    void Start()
    {
        target = this.transform.position;             
        
        canvas = GameObject.FindGameObjectWithTag("canvas");        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        reinicioLvl = canvas.transform.GetChild(3).gameObject;
        proxLevel = canvas.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(this.transform.position,target)<0.001)
        {            
            Walk = false;
            Idle = true;
            move = true;//Move en true para permitir nuevamente el movimiento
        }
        else if(Vector3.Distance(this.transform.position, target) > 0.02f)
        {            
            Walk = true;
            Idle = false;
        }
        IsPointerOverUIObject();        
        Touch();
        anim.SetBool("Idle", Idle);        
        anim.SetBool("Walk", Walk);       
        
    }
    private void FixedUpdate()
    {        
        Movement(target);        
    }
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("tree"))
        {
            move = true;
        }
             
        if (collision.gameObject.CompareTag("ball"))
        {            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 2f, ForceMode.Force);
            
        }
        if (collision.gameObject.CompareTag("lava"))
        {
            move = false;
            StartCoroutine(DestroyLevelAndFox(1.5f));
            reinicioLvl.SetActive(true);
            rb.detectCollisions = false;
        }
        if (collision.gameObject.CompareTag("NextLevel"))
        {
            target = this.transform.position;
            Walk = false;
            Idle = true;
            StartCoroutine(DestroyLevelAndFox(1.5f));
            proxLevel.SetActive(true);
        }
    }    
    
    void Movement(Vector3 target)
    {        
        rb.MovePosition(Vector3.MoveTowards(this.transform.position, new Vector3(target.x, this.transform.position.y, target.z), 0.1f * 0.5f * Time.deltaTime));//SPEED
    }
    void Touch()
    {
        if (Input.touchCount > 0 && move && iniciateMove)
        {            
            Touch toque = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(toque.position);
            RaycastHit hit;            
            canvas.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            if (Physics.Raycast(ray, out hit,100, ~evitarRayos) && !UIDetect)
            {                        
                 if (!hit.collider.CompareTag("fox"))
                 {
                        target = hit.point; 
                        var rotatition = target-this.transform.position;
                        rotatition.y = 0;
                        this.transform.rotation = Quaternion.LookRotation(rotatition);
                       // move = false;//Para no permitir un movimiento mientras se mueve
                    
                 }
                 if (hit.collider.CompareTag("tree"))
                 {
                    target = this.transform.position;
                    var rotatition = hit.point - this.transform.position;
                    rotatition.y = 0;
                    this.transform.rotation = Quaternion.LookRotation(rotatition);
                 }

            }
        }
    }   

    private void IsPointerOverUIObject()//DETECTO UI 
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

    IEnumerator DestroyLevelAndFox(float timeDestroy)
    {
        yield return new WaitForSeconds(timeDestroy);        
        var level = GameObject.FindGameObjectWithTag("level");
        Destroy(level);
        Destroy(this.gameObject);
    }

    

}
