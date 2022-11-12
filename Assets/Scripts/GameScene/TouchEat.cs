using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TouchEat : MonoBehaviour
{      
    private Vector3 target;
    private bool move = false;
    public static bool spawnBanana = false;
    public static bool UIDetect = false;   
    private Rigidbody rb;
    private Animator anim;
    private bool Sit = false;
    private bool Idle = true;
    private bool Walk = false;
    private bool Eat = false;

    Vector3 posicionInicial;
    Vector3 posicionFinal;
    Vector3 lookPosition;
    Vector3 traspasoHit;
    bool sitF = false;
    bool moveF = false;
    bool eatF = false;

    public LayerMask collectChicken;

    [SerializeField]private GameObject FloatingTextPrefab;
    private GameObject canvas;
    private GameObject positionFloatingText;
    
    

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
        target = this.transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        
        Invoke("MovementOn", 2f);
        target = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(this.transform.position, target) < 0.001f)
        {
            Idle = true;
            Walk = false;            
            target = this.transform.position;
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
            Movement(target);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cucha"))
        {            
            
        }        
        if (collision.gameObject.CompareTag("ball"))
        {            
            collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 2f, ForceMode.Force);
            
        }
    }    
    
    void Movement(Vector3 target)
    {
        rb.MovePosition(Vector3.MoveTowards(this.transform.position, new Vector3(target.x, this.transform.position.y, target.z), 0.1f * 2f * Time.deltaTime));//SPEED
    }
    void Touch()
    {
        if (Input.touchCount > 0)
        {            
            Touch toque = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(toque.position);
            RaycastHit hit;            
            canvas.transform.GetChild(0).GetComponent<TMP_Text>().text = "";            
            if (Physics.Raycast(ray, out hit,100, ~collectChicken) && !UIDetect)
            {                        
                 if (!hit.collider.CompareTag("fox"))
                 {
                        target = hit.point; 
                        var rotatition = target-this.transform.position;
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

    void MovementOn()
    {
        move = true;
    }


    IEnumerator DestroyObject(GameObject colision, float timeDestroy)
    {
        yield return new WaitForSeconds(timeDestroy);
        Idle = true;
        Walk = false;
        Sit = false;
        Eat = false;
        eatF = false;
        Destroy(colision);
        
    }
        

}
