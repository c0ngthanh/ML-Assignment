using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GemSO : ScriptableObject {
    public enum Type{
        ATK,
        DEF,
        HEAL,
        ENERGY,
        ELEMENT
    }
    public string gemName;
    public Sprite sprite;
    public Type gemType;

}
