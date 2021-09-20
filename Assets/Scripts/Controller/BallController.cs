using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour, IClickable
{
    Rigidbody _rigidbody;

    Vector3 _startPosition;
    Vector3 _direction;
    Vector3 _currentPosition;


    bool isOnGround;
    bool isNotBlocked = true;
    public bool isOnWhiteBox = false;

    public float _maxVelocity;
    public float _force;
    private Vector3 mOffset;

    private float mZCoord;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();


    }
    void Start()
    {
        _startPosition = transform.position;
    }

    private Vector3 GetMouseAsWorldPoint()

    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void SlideBall(Vector3 direction)
    {
        transform.position = transform.position + direction * 2;
        transform.Rotate(direction * 30f, Space.World);

    }

    void GoDown()
    {
        transform.position = transform.position - Vector3.up * 2;
        transform.Rotate(-Vector3.up * 45f, Space.World);
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
        direction = new Vector3(Mathf.Round(direction.x), 0, Mathf.Round(direction.z));

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
        if (col.tag == "WhiteBox")
        {

            isOnWhiteBox = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        // if (col.tag == "FinishBox")
        // {
        //     //GetComponent<AudioSource>().Play();
        //     gameObject.SetActive(false);
        // }
        if (col.tag == "WhiteBox")
        {
            Debug.Log("Ball Collide with whitebox");
            isOnWhiteBox = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "FinishBox")
        {
            //Debug.Log(col.gameObject.tag);
            //_rigidBody.velocity = Vector3.zero;
            //_rigidBody.angularVelocity = Vector3.zero; 
            _rigidbody.isKinematic = true;

            Vector3 collideDir = (col.GetContact(0).point - transform.position).normalized;
            //Debug.Log(collideDir);
            if (_direction == collideDir)
            {
                gameObject.SetActive(false);
            }
        }

    }

    void OnCollisionExit(Collision col)
    {

    }


    void Update()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }



        _currentPosition = transform.position;


    }

    void FixedUpdate()
    {
        _rigidbody.AddForce(_direction * _force);
        _direction = new Vector3(0,0,0);
        if (_rigidbody.velocity.magnitude > _maxVelocity)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, 10);
        }
    }

    public void OnRightClickDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }
    public void OnRightClickUp()
    {
        _direction = (GetMouseAsWorldPoint() + mOffset - transform.position).normalized;
        _direction = GetXorZDirection(_direction);
        Debug.Log(_direction);

    }

    public void OnMouseEnterHover()
    {
        HoverOn();
        return;
    }


    public void OnMouseExistHover()
    {
        HoverOff();
        return;
    }

    public void OnLeftClick()
    {
        HoverOn();
        Debug.Log("BallController.OnLeftClick() was called.");
        return;
    }


    private void HoverOn()
    {
        GetComponentInChildren<Outline>().OutlineWidth = 4;
    }

    private void HoverOff()
    {
        GetComponentInChildren<Outline>().OutlineWidth = 0;
    }

}
