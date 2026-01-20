using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bridge : MonoBehaviour
{
    public bool bridge_activated = false;
    [SerializeField] private Tilemap world_tilemap;
    private BoxCollider2D area;

    void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }
    public void ActivateBridge()
    {
        if (bridge_activated) 
            return;

        bridge_activated = true;
        
        Bounds bounds = area.bounds;

        Vector3Int min = world_tilemap.WorldToCell(bounds.min);
        Vector3Int max = world_tilemap.WorldToCell(bounds.max);

        for (int x = min.x; x <= max.x; x++)
        {
            for (int y = min.y; y <= max.y; y++)
            {
                world_tilemap.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }
}

