using UnityEngine;


[CreateAssetMenu(menuName = "Data/Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{

    public bool CanAirStrafe => _canAirStrafe;

    public float MaxMoveSpeed => _maxMoveSpeed;


    public float AccelerationRate => _accelerationRate;
    public float DecelerationRate => _decelerationRate;



    public float JumpForce => _jumpForce;



    [SerializeField] private bool _canAirStrafe;


    [SerializeField] private float _maxMoveSpeed;


    [SerializeField] private float _accelerationRate;
    [SerializeField] private float _decelerationRate;



    [SerializeField] private float _jumpForce;

}
