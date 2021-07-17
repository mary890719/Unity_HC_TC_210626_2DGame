using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0,1000)]
    public float Movespeed = 10.5f;
    [Header("跳躍高度"), Range(0, 3000)]
    public int JumpHeight = 100;
    [Header("血量"), Range(0, 200)]
    public float HP = 100;
    [Header("是否在地板上"), Tooltip("是否在地板上")]
    public bool isGround;

    public AudioSource Aud;
    public Rigidbody2D rig;
    public Animator ani;
    #endregion
}
