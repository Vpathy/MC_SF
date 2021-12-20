using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Shape", menuName = "SF/Shape/Create", order = 1)]
public class ShapeScriptableObjects : ScriptableObject
{
    [SerializeField]
    public Shape Shape;
    public int value;
}
