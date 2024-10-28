using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    public string GetInteractPrompt();
    public void Oninteract();
}
public class ItemObject : MonoBehaviour, Iinteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
       string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void Oninteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
