using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private  LayerMask _groundLayers;


    [SerializeField] private PlayerMovementData _playerMovementData;

    #region COMPONENTS

    [SerializeField] private Rigidbody2D _playerRigidbody;

    [SerializeField] private Transform _groundCheckTF;

    #endregion

    #region VECTORS
    private Vector2 _moveVector;

    #endregion

    #region STATES
    
    private bool _isInAir;

    #endregion

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.03f);


    private void Start()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D playerRigidbody))
        {
            _playerRigidbody = playerRigidbody;
        }
        else
        {
            Debug.LogWarning("Player's Rigidbody is not attached!");
        }
    }

    private void Update()
    {
        #region  INPUT HANDLER
        _moveVector.x = Input.GetAxisRaw("Horizontal");
        #endregion

        Jump();
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        if (CheckCanWalk())
        {
            float targetSpeed = _moveVector.x * _playerMovementData.MaxMoveSpeed;



            float accelerationAmount = (Mathf.Abs(targetSpeed) > 0.01f) ? (_playerMovementData.AccelerationRate / Time.fixedDeltaTime) / _playerMovementData.MaxMoveSpeed : (_playerMovementData.DecelerationRate / Time.fixedDeltaTime) / _playerMovementData.MaxMoveSpeed;

            float speedDifference = targetSpeed - _playerRigidbody.velocity.x;
            float movement = speedDifference * accelerationAmount;

            _playerRigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
    }



    private void Jump()
    {
        if (_isInAir && _playerRigidbody.velocity.y < 0)
        {
            _isInAir = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!CheckIsInAir())
            {
                _isInAir = true;

                float force = _playerMovementData.JumpForce;
                _playerRigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }
        }
    }

    #region CHECK METHODS

    private bool CheckCanWalk()
    {
        if (CheckIsInAir() && !_playerMovementData.CanAirStrafe)
        {
            return false;
        }

        return true;
    }

    private bool CheckIsInAir()
    {
        if (Physics2D.OverlapBox(_groundCheckTF.position, _groundCheckSize, 0, _groundLayers))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion


    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckTF.position, _groundCheckSize);
    }
    #endregion
}
