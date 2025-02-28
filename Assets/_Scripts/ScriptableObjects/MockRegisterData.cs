using DTOs.Firebase;
using UnityEngine;

[CreateAssetMenu(fileName = "MockRegisterData", menuName = "ScriptableObjects/MockRegisterData", order = 1)]
public class MockRegisterData : ScriptableObject
{
    [SerializeField] private RegisterDto registerMockData;
    
    public RegisterDto RegisterMockData => registerMockData;
}
