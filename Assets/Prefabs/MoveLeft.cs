using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 5f; 

    void Update()   
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
