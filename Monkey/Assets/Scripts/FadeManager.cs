using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// FadeManager Ŭ������ �� ��ȯ �� ���̵� ��/�ƿ� ȿ���� �����մϴ�.
/// �� Ŭ������ DialogueManager�� ��ȣ�ۿ��Ͽ� ��ȭ ���� ���Ŀ� ���̵� ȿ���� �����մϴ�.
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private bool isStartScene = false;
    [SerializeField] private bool isForestScene = false;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private DialogueComponent dialogueComponent;
    private bool isFading = false;
    private string sceneToLoad;

    private void Start()
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.gameObject.SetActive(true);
            if (!isStartScene)
            {
                fadeAnimator.SetTrigger("FadeIn");
            }
        }
        else
        {
            Debug.LogError("Fade Animator is not assigned.");
        }

        if (isForestScene && dialogueManager != null)
        {
            dialogueManager.PrepareDialogue(playerSprite, dialogueComponent);
        }
    }

    private void Update()
    {
        if (isStartScene && Input.GetKeyDown(KeyCode.Return) && !isFading)
        {
            LoadSceneWithFade("ForestScene");
        }
    }

    /// <summary>
    /// ���̵� �ƿ� ȿ���� �Բ� ���� �ε��մϴ�.
    /// </summary>
    /// <param name="sceneName">�ε��� �� �̸�</param>
    public void LoadSceneWithFade(string sceneName)
    {
        if (!isFading)
        {
            sceneToLoad = sceneName;
            if (fadeAnimator != null)
            {
                fadeAnimator.SetTrigger("FadeOut");
                isFading = true;
            }
            else
            {
                Debug.LogError("Fade Animator is not assigned.");
            }
        }
    }

    /// <summary>
    /// ���̵� ���� �Ϸ�Ǿ��� �� ȣ��˴ϴ�.
    /// </summary>
    public void OnFadeInComplete()
    {
        Debug.Log("Fade In Complete");
        if (isForestScene && dialogueManager != null)
        {
            dialogueManager.StartDialogue(playerSprite, dialogueComponent);
        }
    }

    /// <summary>
    /// ���̵� �ƿ��� �Ϸ�Ǿ��� �� ȣ��˴ϴ�.
    /// </summary>
    public void OnFadeOutComplete()
    {
        Debug.Log("Fade Out Complete");
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        isFading = false;
    }
}
