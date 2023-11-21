using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float szog = 90;
    public SimpleCharacterController p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

void Update()
{
    // Convert the mouse position from screen space to world space
    Vector3 mouseScreenPosition = Input.mousePosition;
    mouseScreenPosition.z = Camera.main.nearClipPlane;
    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

    // Calculate the direction from the object to the mouse
    Vector3 direction = (mouseWorldPosition - transform.position).normalized;

    // Calculate the angle to rotate towards
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Set the object's rotation to that angle
    float f = 0f;
    if(p.bal) f = 180f;
    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - szog)); // Subtract 90 degrees if you want the sprite's 'up' to look at the mouse
}
}
