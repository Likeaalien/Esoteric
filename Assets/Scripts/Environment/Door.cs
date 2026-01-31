using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject invisible_wall;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (player.wooden_key == false)
                return;

            Destroy(gameObject);
            Destroy(invisible_wall);
            Instantiate(Resources.Load<GameObject>("Prefabs/Wooden_door_open"), transform.position, Quaternion.identity);
        }
    }
}
