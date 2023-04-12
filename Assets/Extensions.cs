using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public static class Extensions
 {
      public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
     {
         float multiplier = 1;
         for (int i = 0; i < decimalPlaces; i++)
         {
             multiplier *= 10f;
         }
         return new Vector3(
             Mathf.Round(vector3.x * multiplier) / multiplier,
             Mathf.Round(vector3.y * multiplier) / multiplier,
             Mathf.Round(vector3.z * multiplier) / multiplier);
     }

     public static Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
}
 }