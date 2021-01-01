using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : Transformation
{
   public Vector3 scale;

   private bool counter = true;

   public override Matrix4x4 Matrix
   {
      get {
         Matrix4x4 matrix = new Matrix4x4();
         matrix.SetRow(0, new Vector4(scale.x, 0f,0f, 0f));
         matrix.SetRow(1, new Vector4(0f, scale.y,0f, 0f));
         matrix.SetRow(2, new Vector4(0f, 0f,scale.x, 0f));
         matrix.SetRow(3, new Vector4(0f, 0f,0f, 1f));
         return matrix;
      }
   }

   public void Update()
   {
      if (counter)
      {
         scale.x += 0.01f;
         scale.y += 0.01f;
         scale.z += 0.01f;
      }
      else
      {
         scale.x -= 0.01f;
         scale.y -= 0.01f;
         scale.z -= 0.01f;
      }

      if (scale.x >= 2)
      {
         counter = false;
      }
      if (scale.x <= 1)
      {
         counter = true;
      }
   }
}
