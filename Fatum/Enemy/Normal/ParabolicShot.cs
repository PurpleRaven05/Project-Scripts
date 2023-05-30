using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParabolicShot{
    
    //tiro parabólico controlando todo el recorrido
    public static Vector3 Parabola(Vector3 startPoint, Vector3 endPoint, float height, float time){
        Func<float, float> f = x => -4*height*x*x+4*height*x;

        var mid = Vector3.Lerp(startPoint, endPoint, time);
        UnityEngine.Debug.Log( "Resultado: "+Mathf.Lerp(startPoint.y, endPoint.y, time));
        Vector3 parabolicShot = new Vector3(mid.x, f(time)+ Mathf.Lerp(startPoint.y, endPoint.y, time), mid.z);

        return parabolicShot;
    }
}
