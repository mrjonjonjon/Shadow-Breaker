using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour
{
    public List<Tilemap> tilemaps;
public Tile wallTile;
public Tile groundTile;
public Tile ZeroElevationTile;

public Tile a,b,c;
public bool GENERATE=false;
int GetHeight(TileBase tb){
    // return 0;
    switch(tb.name){
       
    case "h0":
        return 0;
        case "h1":
        return 1;
        case "h2":
        return 2;
        case "h4":
        return 4;
        case "hn5-5":
        return -5;
        default:
        return 0;
    }
}
public void Update(){
if(GENERATE){
    GENERATE=false;
    x();
}
}
void x()
{

    foreach(Tilemap tilemap in tilemaps){
                                       //  tilemap.GetComponent<TilemapRenderer>().SetEnabled(false);

            for (int y = tilemap.cellBounds.yMax; y >= tilemap.cellBounds.yMin; y--){

                for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++){
                
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    
                        if (tilemap.HasTile(tilePosition)){
                                TileBase tile = tilemap.GetTile(tilePosition);
                                int level = GetHeight(tile);
                                tilemap.SetTileFlags(tilePosition, TileFlags.None);
                                Vector3Int belowTile = new Vector3Int(x, y-1, 0);
                                //if(!tilemap.HasTile(belowTile)){
                                    for(int i=0;i<level;i++){ 
                                            Matrix4x4 matri = Matrix4x4.TRS(Vector3.forward*-1*(0.5f + i), Quaternion.Euler(-45f, 0f, 0f), new Vector3(1,Mathf.Sqrt(2),1));

                                            tilemap.transform.Find("visuals").GetComponent<Tilemap>().SetTile(new Vector3Int(x,y+i,0),wallTile);
                                            tilemap.transform.Find("visuals").GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int(x,y+i,0),matri);



                                            //yield return null;
                                    } 
                              //  }
                                
                                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.forward*-1*level-0.01f*Vector3.forward, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
                                Tile toUse= level ==0?ZeroElevationTile:groundTile;
                                tilemap.transform.Find("visuals").GetComponent<Tilemap>().SetTile(new Vector3Int(x,y+level,0),toUse);
                                tilemap.transform.Find("visuals").GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int(x,y+level,0),matrix);
                                
                          

                        
                        }
                }
            }
    }
}  


}
