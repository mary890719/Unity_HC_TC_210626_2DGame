using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 遊戲結束控制器：
/// 1. 擊殺所有怪物並觸發傳送門
/// 2. 玩家死亡
/// </summary>
public class GameOverController : MonoBehaviour
{
    [Header("結束畫面動畫元件")]
    public Animator aniFinal;
    [Header("結束標題")]
    public Text textFainalTitla;
    [Header("遊戲勝利與失敗文字")]
    // 字串內的換行 \n
    [TextArea(1, 3)]
    public string stringWin = "你已經成功擊殺所有怪物，\n可以往前繼續了。";
    [TextArea(1, 3)]
    public string stringLose = "挑戰失敗...\n再挑戰一次吧！";
    [Header("重新與離開按鈕")]
    public KeyCode kcReplay = KeyCode.R;
    public KeyCode kcQuit = KeyCode.Q;

    /// <summary>
    /// 是否遊戲結束
    /// </summary>
    private bool isGameOver;

    /// <summary>
    /// 顯示遊戲結束畫面
    /// 1. 設定為遊戲結束
    /// 2. 啟動動畫 - 淡入
    /// 3. 判斷勝利或失敗並更新標題
    /// </summary>
    /// <param name="win">是否獲勝</param>
    public void ShowGameOverView(bool win)
    {
        isGameOver = true;
        aniFinal.enabled = true;

        if (win) textFainalTitla.text = stringWin;
        else textFainalTitla.text = stringLose;
    }

}
