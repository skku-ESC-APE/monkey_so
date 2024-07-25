using UnityEngine;
using System.Collections.Generic;


/// DialogueComponent Ŭ������ Ư�� ��������Ʈ�� �����ϴ� ��ȭ �����̳� ����
/// DialogueManager�� ��ȣ�ۿ��Ͽ� �÷��̾��� ��������Ʈ�� ���� �˸´� ��ȭ �����̳� ����

[System.Serializable]
public class DialogueCondition
{
    public Sprite sprite; // ���� ��������Ʈ
    public DialogueContainer container; // ��������Ʈ�� ���ε� ��ȭ �����̳�
}

public class DialogueComponent : MonoBehaviour
{
    public List<DialogueCondition> dialogueConditions; // ��������Ʈ�� ��ȭ �����̳��� ����Ʈ

    public DialogueContainer GetContainerForSprite(Sprite playerSprite)
    {
        foreach (var condition in dialogueConditions)
        {
            if (condition.sprite == playerSprite)
            {
                return condition.container;
            }
        }
        return null;
    }
}
