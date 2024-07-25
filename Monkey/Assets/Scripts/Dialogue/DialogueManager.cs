using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// DialogueManager Ŭ������ ��ȭ UI�� �����ϰ� ��ȭ�� �����մϴ�.
/// �� Ŭ������ DialogueComponent, DialogueContainer�� ��ȣ�ۿ��Ͽ� ��ȭ�� �����ϰ� �����մϴ�.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text characterNameText;
    [SerializeField] private Image characterImage1;
    [SerializeField] private Image characterImage2;
    [SerializeField] private FadeManager fadeManager;
    private DialogueContainer dialogueContainer;
    private int currentLineIndex = 0;

    private void Start()
    {
        dialoguePanel.SetActive(false); // �ʱ⿡�� ��ȭ �г��� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            ShowNextLine(); // ��ȭ�� ���� ���̰� Enter Ű�� ������ ���� ��ȭ ���� ǥ��
        }
    }

    /// <summary>
    /// ��ȭ�� �غ��մϴ�.
    /// </summary>
    /// <param name="playerSprite">�÷��̾��� ��������Ʈ</param>
    /// <param name="dialogueComponent">DialogueComponent �ν��Ͻ�</param>
    public void PrepareDialogue(Sprite playerSprite, DialogueComponent dialogueComponent)
    {
        dialogueContainer = dialogueComponent.GetContainerForSprite(playerSprite);
        if (dialogueContainer != null)
        {
            currentLineIndex = 0;
        }
        else
        {
            Debug.LogError("No suitable Dialogue Container found for the given sprite.");
        }
    }

    /// <summary>
    /// ��ȭ�� �����մϴ�.
    /// </summary>
    /// <param name="playerSprite">�÷��̾��� ��������Ʈ</param>
    /// <param name="dialogueComponent">DialogueComponent �ν��Ͻ�</param>
    public void StartDialogue(Sprite playerSprite, DialogueComponent dialogueComponent)
    {
        dialogueContainer = dialogueComponent.GetContainerForSprite(playerSprite);
        if (dialogueContainer != null)
        {
            Debug.Log("Starting dialogue with container: " + dialogueContainer.name);
            currentLineIndex = 0;
            dialoguePanel.SetActive(true);
            ShowNextLine();
        }
        else
        {
            Debug.LogError("No suitable Dialogue Container found for the given sprite.");
        }
    }

    /// <summary>
    /// ���� ��ȭ ���� ǥ���մϴ�.
    /// </summary>
    private void ShowNextLine()
    {
        if (dialogueContainer == null || dialogueText == null || characterNameText == null)
        {
            Debug.LogError("One or more references are null.");
            return;
        }

        if (currentLineIndex < dialogueContainer.lines.Count)
        {
            DialogueLine line = dialogueContainer.lines[currentLineIndex];
            if (line == null)
            {
                Debug.LogError($"Dialogue Line at index {currentLineIndex} is null.");
                return;
            }

            characterNameText.text = line.characterName;
            dialogueText.text = line.text;

            // ù ��° ĳ���� �̹��� ����
            if (characterImage1 != null)
            {
                if (line.characterImage1 != null)
                {
                    characterImage1.sprite = line.characterImage1;
                    characterImage1.enabled = true;
                }
                else
                {
                    characterImage1.enabled = false;
                }
            }

            // �� ��° ĳ���� �̹��� ����
            if (characterImage2 != null)
            {
                if (line.characterImage2 != null)
                {
                    characterImage2.sprite = line.characterImage2;
                    characterImage2.enabled = true;
                }
                else
                {
                    characterImage2.enabled = false;
                }
            }

            currentLineIndex++;
        }
        else
        {
            EndDialogue(); // ��ȭ�� ������ ��ȭ�� ����
        }
    }

    /// <summary>
    /// ��ȭ�� �����մϴ�.
    /// </summary>
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");
        if (fadeManager != null)
        {
            fadeManager.LoadSceneWithFade("GameScene");
        }
    }
}
