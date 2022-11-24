using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class TouchEat : MonoBehaviour
{
    
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
    public float SPEED;
    private GameObject target;
    [SerializeField] private GameObject prefabTarget;
   
    
    void Start()
    {        

        target = GameObject.FindGameObjectWithTag("eat");
        target.transform.position = this.transform.position;
        canvas = GameObject.FindGameObjectWithTag("canvas");        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        reinicioLvl = canvas.transform.GetChild(3).gameObject;
        proxLevel = canvas.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target == null)
        {
            Instantiate(prefabTarget);
            target = GameObject.FindGameObjectWithTag("eat");
            target.transform.position = this.transform.position;
        }        
        if (Vector3.Distance(this.transform.position,target.transform.position)<0.001)
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
        IsPointerOverUIObject();        
        Touch();
        anim.SetBool("Idle", Idle);        
        anim.SetBool("Walk", Walk);       
        
    }
    private void FixedUpdate()
    {
        if (move)
        {
            Movement(target.transform.position);
        }
            
    }
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("tree"))
        {
            //this.transform.SetParent(collision.gameObject.transform);//POR SI SE BUGEA CON EL COSTADO
        }
             
        if (collision.gameObject.CompareTag("ball"))
        {            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 0.8f, ForceMode.Impulse);            
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
            //target = this.transform.position;
            Walk = false;
            Idle = true;
            StartCoroutine(DestroyLevelAndFox(1.5f));
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
            if (Physics.Raycast(ray, out hit,100, ~evitarRayos) && !UIDetect)
            {                        
                 if (!hit.collider.CompareTag("fox"))
                 {
                        target.transform.SetParent(null);
                        target.transform.position = hit.point; 
                        var rotatition = target.transform.position-this.transform.position;
                        rotatition.y = 0;
                        this.transform.rotation = Quaternion.LookRotation(rotatition);
                        move = true;
                        if (Singleton.Level == 2)
                        {                            
                            var managerLvl3 = GameObject.FindGameObjectWithTag("managerLvl3");
                            managerLvl3.GetComponent<ManagerLvl3>().startTimer = true;
                            var fox = GameObject.FindGameObjectWithTag("fox");
                            fox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                            

                        }
                    
                 }
                 /*if (hit.collider.CompareTag("tree"))
                 {
                    target.transform.position= this.transform.position;
                    var rotatition = hit.point - this.transform.position;
                    rotatition.y = 0;
                    this.transform.rotation = Quaternion.LookRotation(rotatition);
                 }*/

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
        target.transform.SetParent(null);
        Destroy(level);
        
        Destroy(this.gameObject);
    }

    

}
