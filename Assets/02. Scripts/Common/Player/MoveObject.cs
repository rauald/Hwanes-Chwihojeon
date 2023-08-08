using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 플레이어 및 적 공통 움직임 변수
public class MoveObject : MonoBehaviour
{
    // 한칸 총 프레임 수
    protected float walkCount;
    // 스피드
    protected float speed;
    // 현재 프레임 수
    protected int currentWalkCount;

    // 좌 우
    //protected float[] h = new float[3];
    public float[] h = new float[3];
    // 상 하
    //protected float[] v = new float[3];
    public float[] v = new float[3];

    // 좌우 동시 입력 방지
    protected int up_Value;
    protected int down_Value;
    protected int right_Value;
    protected int left_Value;

    // 방향키 중복 방지
    // 전투진입 or 맵(씬)/필드 이동시 이동을 멈춤
    // 다른 이동키를 눌러도 현재 이동을 다끝낸뒤 움직이게 만듦
    public bool canMove;
}