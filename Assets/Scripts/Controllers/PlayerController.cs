using System;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Transform CameraFollow;

    private Rigidbody _rigidboy = null;

    [FormerlySerializedAs("_input")] [SerializeField] private PlayerInput2 input2;

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
        return new Vector3(input2.MoveInput.x, 0.0f, input2.MoveInput.y);
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
            input2.LookInput.x, (input2.InvertMouseY ? -input2.LookInput.y : input2.LookInput.y), 0.0f);

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
        _cameraPitch = Mathf.Clamp(_cameraPitch, -4.0f, 45.0f);

        CameraFollow.rotation = Quaternion.Euler(
            _cameraPitch, CameraFollow.eulerAngles.y, CameraFollow.eulerAngles.z);
    }
}