using UnityEngine;

[CreateAssetMenu(fileName = "SnapGridData", menuName = "ScriptableObjects/SnapGridData", order = 1)]
public class SnapGridData : ScriptableObject
{
    [Tooltip("Snap grid size in Unity Units")]
    public float snapDistanceInUnityUnits = 5.0f;
}
