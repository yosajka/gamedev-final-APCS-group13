using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class IClickable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
=======
public interface IClickable {
    void OnLeftClick();
    void OnRightClick();
    
    void OnHoverEnter();
    void OnHoverExist();
>>>>>>> Stashed changes
}