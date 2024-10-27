using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; // CharacterManager.cs의 인스턴스에 연결되어 함수처리함
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}

