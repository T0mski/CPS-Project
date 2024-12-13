using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu]
public class ArraySO : ScriptableObject
{
    [SerializeField]
    private string[] _questions;
    

    public string[] Questions
    {
        get { return _questions; }
        set { _questions = value; }
    }
}
