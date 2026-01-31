using UnityEngine;

public class Hunter : MonoBehaviour
{
    public bool hunter_quest_1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hunter_quest_1 == true)
        return;

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (player.gold_currency < 10)
                return;
                
            if (player.gold_currency >= 10)
            {
                player.gold_currency -= 10;
                hunter_quest_1 = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/Bow"), transform.position + 2 * Vector3.down, Quaternion.identity);
            }
        }
    }
}
