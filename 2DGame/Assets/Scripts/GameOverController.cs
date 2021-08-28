using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �C����������G
/// 1. �����Ҧ��Ǫ���Ĳ�o�ǰe��
/// 2. ���a���`
/// </summary>
public class GameOverController : MonoBehaviour
{
    [Header("�����e���ʵe����")]
    public Animator aniFinal;
    [Header("�������D")]
    public Text textFainalTitla;
    [Header("�C���ӧQ�P���Ѥ�r")]
    // �r�ꤺ������ \n
    [TextArea(1, 3)]
    public string stringWin = "�A�w�g���\�����Ҧ��Ǫ��A\n�i�H���e�~��F�C";
    [TextArea(1, 3)]
    public string stringLose = "�D�ԥ���...\n�A�D�Ԥ@���a�I";
    [Header("���s�P���}���s")]
    public KeyCode kcReplay = KeyCode.R;
    public KeyCode kcQuit = KeyCode.Q;

    /// <summary>
    /// �O�_�C������
    /// </summary>
    private bool isGameOver;

    private void Update()
    {
        Replay();
        Quit();
    }

    /// <summary>
    /// ���s�C��
    /// </summary>
    private void Replay()
    {
        if (isGameOver && Input.GetKeyDown(kcReplay)) SceneManager.LoadScene("�C������");
    }

    /// <summary>
    /// ���}�C��
    /// </summary>
    private void Quit()
    {
        if (isGameOver && Input.GetKeyDown(kcQuit)) Application.Quit();
    }

    /// <summary>
    /// ��ܹC�������e��
    /// 1. �]�w���C������
    /// 2. �Ұʰʵe - �H�J
    /// 3. �P�_�ӧQ�Υ��Ѩç�s���D
    /// </summary>
    /// <param name="win">�O�_���</param>
    public void ShowGameOverView(bool win)
    {
        isGameOver = true;
        aniFinal.enabled = true;

        if (win) textFainalTitla.text = stringWin;
        else textFainalTitla.text = stringLose;
    }

}
