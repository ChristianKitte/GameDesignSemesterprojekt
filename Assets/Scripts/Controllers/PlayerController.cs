using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform CameraFollow;

    private Rigidbody _rigidboy = null;

    [SerializeField] private PlayerInput _input;

    private Vector3 _playerMoveInput = Vector3.zero;

    private Vector3 _playerLookInput = Vector3.zero;
    private Vector3 _previousPlayerLookInput = Vector3.zero; //Input des vorherigen Frames
    private float _cameraPitch = 0.0f;
    [SerializeField] private float _playerLookInputLerpTime = 0.35f;

    [Header("Movement")] [SerializeField] private float _movementMultiplier = 30.0f;
    [SerializeField] private float _rotationSpeedMultiplier = 180.0f;
    [SerializeField] private float _pitchSpeedMultiplier = 180.0f;

    private void Awake()
    {
        _rigidboy = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _playerLookInput = GetLookInput();
        PlayerLook();
        PitchCamera();

        _playerMoveInput = GetMoveInput();
        PlayerMove();

        _rigidboy.AddRelativeForce(_playerMoveInput, ForceMode.Force);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(_input.MoveInput.x, 0.0f, _input.MoveInput.y);
    }

    private void PlayerMove()
    {
        var rigidbodyMass = _rigidboy.mass;

        _playerMoveInput = new Vector3(
            _playerMoveInput.x * _movementMultiplier * rigidbodyMass,
            _playerMoveInput.y,
            _playerMoveInput.z * _movementMultiplier * rigidbodyMass);
    }

    private Vector3 GetLookInput()
    {
        _previousPlayerLookInput = _playerLookInput;
        _playerLookInput = new Vector3(
            _input.LookInput.x, (_input.InvertMouseY ? -_input.LookInput.y : _input.LookInput.y), 0.0f);

        // Lerp kann für eine sanfte Bewegung sorgen, indem er zwischen zwei Punkten und einer Zeit interpoliert
        return Vector3.Lerp(
            _previousPlayerLookInput, _playerLookInput * Time.deltaTime, _playerLookInputLerpTime);
    }


    private void PlayerLook()
    {
        _rigidboy.rotation = Quaternion.Euler(
            0.0f, _rigidboy.rotation.eulerAngles.y + (_playerLookInput.x * _rotationSpeedMultiplier), 0.0f);
    }

    private void PitchCamera()
    {
        _cameraPitch += _playerLookInput.y * _pitchSpeedMultiplier;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -4.0f, 89.9f);

        CameraFollow.rotation = Quaternion.Euler(
            _cameraPitch, CameraFollow.eulerAngles.y, CameraFollow.eulerAngles.z);
    }
}