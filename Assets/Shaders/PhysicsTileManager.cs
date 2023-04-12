using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PhysicsTileManager : MonoBehaviour
{
  Dictionary<Vector3Int, float> tileInfo = new Dictionary<Vector3Int, float>();
public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            if (!tilemap.HasTile(position)) {
                continue;
            }
 tileInfo.Add(position,0f);
            // Tile is not empty; do stuff
        }
    }
    public float getTileInfo(Vector3Int pos){
        return tileInfo[pos];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
