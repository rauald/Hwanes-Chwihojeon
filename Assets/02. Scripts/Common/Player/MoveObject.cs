using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �÷��̾� �� �� ���� ������ ����
public class MoveObject : MonoBehaviour
{
    // ��ĭ �� ������ ��
    protected float walkCount;
    // ���ǵ�
    protected float speed;
    // ���� ������ ��
    protected int currentWalkCount;

    // �� ��
    //protected float[] h = new float[3];
    public float[] h = new float[3];
    // �� ��
    //protected float[] v = new float[3];
    public float[] v = new float[3];

    // �¿� ���� �Է� ����
    protected int up_Value;
    protected int down_Value;
    protected int right_Value;
    protected int left_Value;

    // ����Ű �ߺ� ����
    // �������� or ��(��)/�ʵ� �̵��� �̵��� ����
    // �ٸ� �̵�Ű�� ������ ���� �̵��� �ٳ����� �����̰� ����
    public bool canMove;
}