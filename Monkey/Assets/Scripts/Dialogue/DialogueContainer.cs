using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueContainer : ScriptableObject
{
    public List<DialogueLine> lines; // ��ȭ�� �� ���� �����ϴ� ����Ʈ
}
