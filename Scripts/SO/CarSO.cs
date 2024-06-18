using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CarType
{
    Taxi,
    SportsCar,
    Police,
    WaterCar
}

[System.Serializable]
public class CarAbility
{
    public float value;
}

[CreateAssetMenu(fileName = "Car", menuName = "New Car")]
public class CarSO : ScriptableObject
{
    [Header("Info")]
    public string carName;
    public CarType type;
    public GameObject carPrefab;
    public float moveSpeed = 1.0f;
    public float destroyDis = 13f;

}
