using UnityEngine;
using System.Linq;      // �ޥ� LinQ �d�߻y�� API - �d��}�C���

public class LearnLiQ : MonoBehaviour
{
    public int[] scores = { 10, 80, 60, 30, 70, 99, 77, 1, 0 };

    public int[] result;
    public int[] resultEqualThan60;

    private void Start()
    {
        // �ˬd���S�� 0 ��
        // �H�ڹF Lambda ²�� C# 3.0 ���᪺²�g�覡

        // �ˬd scores �� ���S�� ���Ƭ� 0 ����
        // x �N�W��
        // => �]�w����
        scores.Where(x => x == 0);
        
        // �ˬd���S���j�󵥩� 60 ��
        resultEqualThan60 = scores.Where(x => x >= 60).ToArray();
    }
}
