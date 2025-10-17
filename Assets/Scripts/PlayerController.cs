using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1f, 15f)] private float _speed = 7f;
    private Vector2 _moveInput;
    [HideInInspector] public Vector2 moveDirection;
    private PhotonView _photonView;
    public bool playerReady = false;
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (!_photonView.IsMine || !GameManager.Instance.playingGame) return;

        transform.Translate(Vector2.up * _speed * Time.deltaTime * _moveInput);
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        if(_moveInput.y != 0)
            moveDirection = _moveInput;
    }
    public void OnReady(InputAction.CallbackContext ctx)
    {
        if(!_photonView.IsMine || playerReady) return;

        if (ctx.performed)
            GameManager.Instance.photonView.RPC("RPC_PlayerReady", RpcTarget.AllBuffered, null);
    }
    public void OnStart(InputAction.CallbackContext ctx)
    {
        if (!_photonView.IsMine || GameManager.Instance.playingGame) return;

        if (ctx.performed)
        {
            // Solo el MasterClient puede iniciar la partida
            if (PhotonNetwork.IsMasterClient && GameManager.Instance.playersReady >= 2)
            {
                GameManager.Instance.photonView.RPC("RPC_StartGame", RpcTarget.AllBuffered, null);
            }
        }
    }
}
