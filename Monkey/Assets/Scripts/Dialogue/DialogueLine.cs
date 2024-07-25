using UnityEngine;

/// <summary>
/// DialogueLine Ŭ������ ��ȭ�� �� ���� ��Ÿ���ϴ�.
/// �� Ŭ������ DialogueContainer���� ���˴ϴ�.
/// </summary>

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/DialogueLine")]
public class DialogueLine : ScriptableObject
{
    public string characterName; // ��ȭ�ϴ� ĳ������ �̸�
    public string text; // ��ȭ �ؽ�Ʈ
    public Sprite characterImage1; // ù ��° ĳ���� �̹���
    public Sprite characterImage2; // �� ��° ĳ���� �̹���
}
