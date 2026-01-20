using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Bridge bridge;
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag != "Arrow")
            return; 
        
        bridge.ActivateBridge();
        Instantiate(Resources.Load<GameObject>("Prefabs/BridgeObject"));
    }
}
