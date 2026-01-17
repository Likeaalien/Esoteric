using UnityEngine;

public class Target : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider)
    {
              if (collider.gameObject.tag == "Arrow")
        {
            Debug.Log("OnCollisionEnter2D");   
        }
        else
        {
            Debug.Log("Error");
        }        
    }
}
