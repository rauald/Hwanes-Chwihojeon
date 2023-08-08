using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    // 넘어왔으니 다시 바운드 받을 값
    [SerializeField]
    private CompositeCollider2D bound;

    // 현재 맵 이름 이름
    public string curBound;

    private void Awake()
    {
        bound = this.GetComponent<CompositeCollider2D>();
    }

    private void Start()
    {
        if (curBound == PlayerManager.instance.curMapName)
        {
            PlayerManager.instance.curMapName = curBound;
            PlayerManager.instance.curBound = bound;
            CameraManager.instance.SetBound(bound);
        }
    }
}