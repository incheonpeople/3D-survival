using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this; // CharacterManager.cs�� �ν��Ͻ��� ����Ǿ� �Լ�ó����
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}

