using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartSO", menuName = "PartSO")]
public class PartSO : ScriptableObject
{
    [SerializeField] public GameObject gameObj;
    [SerializeField] public string partName;
    [SerializeField] public string type;
    [SerializeField] public string[] slot;
    [SerializeField] public float weight;
    [SerializeField] public float price;
    [SerializeField] public float power;
    [SerializeField] public float tempMin;
    [SerializeField] public float tempMax;
    [SerializeField] public string description;
}
