using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody _rigidBody;
    
    Vector3 _startPosition;
    Vector3 _previousPosition;
    [SerializeField] float _launchForce = 100;
    Vector3 z_positive = new Vector3 (0,0,1);
    Vector3 z_negative = new Vector3 (0,0,-1);

    private Vector3 mOffset;

    private float mZCoord;

    void Awake() 
    {
        _rigidBody = GetComponent<Rigidbody>();
        
        //_transform = GetComponent<Transform>();
    }
    void Start()
    {
        GameObject [] ground  = GameObject.FindGameObjectsWithTag("Ground");
        _startPosition = transform.position;
    }

    



    void OnMouseDown()

    {

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }



    private Vector3 GetMouseAsWorldPoint()

    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen

        mousePoint.z = mZCoord;

        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }



    // void OnMouseDrag()

    // {

    //     transform.position = GetMouseAsWorldPoint() + mOffset;

    // }

    void OnMouseUp()
    {
        Vector3 direction = (GetMouseAsWorldPoint() + mOffset - transform.position).normalized;
        _rigidBody.AddForce(direction*_launchForce);
    }

    // void OnMouseUp() 
    // {
    //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     Vector3 direction = _startPosition - mousePosition;
    //     direction = new Vector3(direction.x, 0, direction.z).normalized;
        
    //     _rigidBody.AddForce(direction*_launchForce);
        
    // }

   

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0 || transform.position.y > 10) 
        {
            Destroy(gameObject);
        }
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime, Space.World);
        }
             
        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(Vector3.right);
            transform.Translate(Vector3.right * Time.deltaTime, Space.World);
        }
             
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(z_positive * Time.deltaTime, Space.World);
        }
             
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(z_negative * Time.deltaTime, Space.World);
        }
        
        
        
    }

   

    
}
