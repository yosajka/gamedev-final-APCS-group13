using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody _rigidBody;
    
    Vector3 _startPosition;
    
    //[SerializeField] float _launchForce = 100;

    //Vector3 z_positive = new Vector3 (0,0,1);
    //Vector3 z_negative = new Vector3 (0,0,-1);

    bool isGrounded;
    bool movable = true;

    private Vector3 mOffset;

    private float mZCoord;

    void Awake() 
    {
        _rigidBody = GetComponent<Rigidbody>();
        
        
    }
    void Start()
    {
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


    void OnMouseUp()
    {
        Vector3 direction = (GetMouseAsWorldPoint() + mOffset - transform.position).normalized;
        
        direction = GetXorZDirection(direction);
        int i = 1;
        while (i > 0)
        {
            SlideBall(direction);
            //yield return new WaitForSeconds(0.1f);

            // if (!isGrounded)
            // {
            //     Vector3 position = transform.position;
            //     transform.position = new Vector3 (position.x, position.y-1,position.z);
            //     yield return new WaitForSeconds(0.1f);
            // }

            i--;
        }
        
        Debug.Log(direction);
        
    }

    void SlideBall(Vector3 direction)
    {
        movable = true;
        int i = 0;
        while (movable) 
        {
            if (Physics.Raycast(transform.position, direction, 2f))
            {
                Debug.DrawRay(transform.position, direction, Color.yellow);
                Debug.Log("Hit");
                movable = false;
                break;
            }
            else
            {
                Debug.DrawRay(transform.position, direction, Color.yellow);
            }
            
            transform.position = transform.position + direction * 2;
            transform.Rotate(direction*45f, Space.World);
            //System.Threading.Thread.Sleep(1000);
            // for debug purpose
            if (i == 1)
                break;
            i++;
        }
        
        // Debug.Log(isGrounded);
        // if (isGrounded == false)
        // {
        //     Vector3 position = transform.position;
        //     transform.position = new Vector3 (position.x, position.y - 2, position.z);
        // }
        
        // Rotate the ball to make it feel like rolling
        
           
        
        
        //yield return new WaitForSeconds(0.5f);
    }

    Vector3 GetXorZDirection(Vector3 direction)
    {
        direction = new Vector3 (Mathf.Round(direction.x), 0, Mathf.Round(direction.z));

        // The ball cannot move diagonally
        if (direction.x != 0f && direction.z != 0f)
        {
            direction = Vector3.zero;
        }
        return direction;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
 
    void OnTriggerExit(Collider col)
    {
        Debug.Log("Ground");
        if(col.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
    }

    void OnCollisionExit(Collision col)
    {
        
    }

    // void OnTriggerEnter(Collider col)
    // {
    //     if(col.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    //     _rigidBody.isKinematic = true;
    // }
   

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0 || transform.position.y > 10) 
        {
            Destroy(gameObject);
        }
       
        
    }

   

    
}
