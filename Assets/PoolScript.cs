using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[ExecuteInEditMode]
public class PoolScript : MonoBehaviour
{
    public bool GENERATE=false;
    public Tile waterTile;
    public int waterLevel;
    public float acceleration=0.03f;
    public float localGravity=0f;

    public void Update(){
        if(GENERATE){
            GENERATE=false;
            x();
        }
    }
    public void x(){

        Tilemap tilemap = GetComponent<Tilemap>();
         //transform.localPosition = waterLevel*Vector3.up +  waterLevel* (- Vector3.forward);
         for (int y = tilemap.cellBounds.yMax; y >= tilemap.cellBounds.yMin; y--){

                for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++){
                
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    
                        if (tilemap.HasTile(tilePosition)){

                                tilemap.SetTileFlags(tilePosition, TileFlags.None);
                                Vector3Int belowTile = new Vector3Int(x, y-1, 0);
                                //if(!tilemap.HasTile(belowTile)){
                                    for(int i=0;i<waterLevel;i++){ 
                                            Matrix4x4 matri = Matrix4x4.TRS(Vector3.forward*-1*(0.5f + i), Quaternion.Euler(-45f, 0f, 0f), new Vector3(1,Mathf.Sqrt(2),1));

                                            tilemap.transform.Find("visuals/bottom").GetComponent<Tilemap>().SetTile(new Vector3Int(x,y+i,0),waterTile);
                                            tilemap.transform.Find("visuals/bottom").GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int(x,y+i,0),matri);



                                            //yield return null;
                                    } 
                              //  }
                                
                                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.forward*-1*waterLevel-0.01f*Vector3.forward, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
                               // Tile toUse= GetHeight(tilemap) ==0?ZeroElevationTile:groundTile;
                                tilemap.transform.Find("visuals/top").GetComponent<Tilemap>().SetTile(new Vector3Int(x,y+waterLevel,0),waterTile);
                                 tilemap.transform.Find("visuals/top").GetComponent<Tilemap>().SetTileFlags(new Vector3Int(x,y+waterLevel,0), TileFlags.None);
                                //tilemap.transform.Find("visuals").GetComponent<Tilemap>().SetColor(new Vector3Int(x,y+waterLevel,0),new Color(0f,0f,01f,0.5f));
                                tilemap.transform.Find("visuals/top").GetComponent<Tilemap>().SetTransformMatrix(new Vector3Int(x,y+waterLevel,0),matrix);
                                
                          

                        
                        }
                }
            }
    }
   
    
}
