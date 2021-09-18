using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody _rigidBody;
    //public AudioSource clapSound;
    Vector3 _startPosition;
    Vector3 direction;
    Vector3 _currentPosition;
    
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


    IEnumerator  OnMouseUp()
    {
        direction = (GetMouseAsWorldPoint() + mOffset - transform.position).normalized;
        
        direction = GetXorZDirection(direction);
        
        movable = true;
        
        while (movable) 
        {
            if (Physics.Raycast(_currentPosition, direction, 2f))
            {
                Debug.Log("Hit obstacle");
                movable = false;
                break;
            }
            else
            {
                movable = true;
            }
            
            SlideBall(direction);
            yield return new WaitForSeconds(0.05f);

            isGrounded = IsOnGround();
            
            if (isGrounded == false)
            {
                GoDown();
            }
            yield return new WaitForSeconds(0.05f);
        }
        
        Debug.Log(direction);
        
    }

    void SlideBall(Vector3 direction)
    {
        //movable = true;
        transform.position = transform.position + direction * 2;
        transform.Rotate(direction*45f, Space.World);
        
    }

    void GoDown()
    {
        //Vector3 y_direction = new Vector3 (0f, -1f, 0f);
        transform.position = transform.position - Vector3.up * 2;
        transform.Rotate(-Vector3.up*45f, Space.World);
    }

    bool IsOnGround()
    {
        RaycastHit hit;
        Ray downRay = new Ray(_currentPosition, -Vector3.up);

        if (Physics.Raycast(downRay, out hit))
        {
    
            if (hit.distance > 2f)
            {
               return false;
            }
        }
        return true;
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
        
    }
 
    void OnTriggerExit(Collider col)
    {
        
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "FinishBox")
        {
            GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
        }
    }

    void OnCollisionExit(Collision col)
    {
        
    }

    
    void Update()
    {
        if (transform.position.y < 0 || transform.position.y > 10) 
        {
            Destroy(gameObject);
        }
       
        _currentPosition = transform.position;
        
    }

   

    
}
