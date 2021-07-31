using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0,50)]
    public float speed = 10.5f;
    [Header("跳躍高度"), Range(0, 3000)]
    public float jump = 100;
    [Header("血量"), Range(0, 200)]
    public float HP = 100;
    [Header("是否在地板上"), Tooltip("是否在地板上")]
    public bool isGround;

    //私人欄位為不顯示
    //開啟屬性面板除錯模式 Debug 可以看到私人欄位
    private AudioSource Aud;
    private Rigidbody2D rig;
    private Animator ani;
    #endregion

    #region 事件
    private void Start()
    {
        //GetComponet<類型>() 泛型方法，可以指定任何類型
        //作用：取得此物件的2D剛體元件
        rig = GetComponent<Rigidbody2D>();
    }
    
    //一秒約執行 60 次
    private void Update()
    {
        GetPlayerInputHorizontal();
        TurnDirection();
        Jump();
    }

    //固定更新事件
    //一秒固定執行 50 次，官方建議有使用道物理 API 要在此事件內執行
    private void FixedUpdate()
    {
        Move(hValue);
    }
    #endregion

    #region 方法
    /// <summary>
    /// 玩家水平輸入值
    /// </summary>
    private float hValue;
        
    /// <summary>
    /// 取得玩家輸入水平軸向值：左與右 A、D、左、右
    /// </summary>
    private void GetPlayerInputHorizontal() 
    {
        //水平值 = 輸入.取得軸向(軸向名稱)
        //作用：取得玩家按下水平按鍵的值，按右為1，按左為-1，沒按為0
        hValue = Input.GetAxis("Horizontal");
        //print("玩家水平值" + hValue);

    }

    [Header("重力"), Range(0.1f, 10)]
    public float gravity = 1;

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="horizontal">左右數值</param>
    private void Move(float horizontal)
    {
        // 區域變數:在方法內的欄位，有區域性，僅限於此方法內存取
        //transform 此物件的 Transform 變形元件
        //posMove = 角色當前座標+ 玩家輸入的水平值
        //Time.fixedDeltaTime 指 1/50 秒
        Vector2 posMove = transform.position + new Vector3(horizontal, -gravity, 0) * speed * Time.fixedDeltaTime;
        // 剛體.移動座標(要前往的座標)
        rig.MovePosition(posMove);
    }

    /// <summary>
    /// 旋轉方向：處理角色面向問題，按右角度 0，按左角度 180
    /// </summary>
    private void TurnDirection()
    {

        // 如果 玩家按 D 就將角度設定為 0, 0, 0
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = Vector3.zero;
        }
        // 如果 玩家按 A 就將角度設定為 0, 180, 0
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        // 如果 玩家 按下 空白建 角色就往上跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(new Vector2(0, jump));
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attake()
    {

    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">造成的傷害</param>
    public void Damage(float damage)
    {

    }

    /// <summary>
    /// 受傷
    /// </summary>
    private void Dead()
    {

    }

    /// <summary>
    /// 吃道具
    /// </summary>
    /// <param name="propName">吃到的道具名稱</param>
    private void EatProp(string propName)
    {

    }
    #endregion
}
