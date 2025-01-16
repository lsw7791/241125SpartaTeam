using UnityEngine;
using UnityEngine.EventSystems;

public interface IDraggable
{
    void OnPointerDown(PointerEventData eventData);
    void OnDrag(PointerEventData eventData);
    void OnPointerUp(PointerEventData eventData);
}

public abstract class UIBase : MonoBehaviour, IDraggable, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public bool CanDrag;
    public RectTransform rectTransform;
    private Vector2 offset;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        OpenProcedure();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        CloseProcedure();
    }
    public void OnclickedMainMenuBtn()
    {
        DataManager dataManager = GameManager.Instance.DataManager;

        UIManager.Instance.CloseUI<OptionUI>();

        GameManager.Instance.Player.stats.nowMapNumber = GameManager.Instance.SceneNum;

        dataManager.SaveData(GameManager.Instance.Player.stats);
        dataManager.SaveData(GameManager.Instance.Player.inventory);
        //dataManager.SaveData(GameManager.Instance.Player.equipment);
        dataManager.DataClear();

        if (GameManager.Instance.Player.stats.CurrentQuestId < 9)
        {
            QuestIcon questUI = UIManager.Instance.GetUI<QuestIcon>();

            if (questUI.mainQuestUI != null)
            {
                questUI.mainQuestUI.gameObject.SetActive(false);
            }
            questUI.Close();
        }

        GameManager.Instance.SceneNum = 25;
        UIManager.Instance.fadeManager.LoadSceneWithFade(dataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }
    protected virtual void OpenProcedure()
    { // ���� �� �߰� �������
        GameManager.Instance.Player.PlayerStateUIOpen();

    }

    protected virtual void CloseProcedure()
    { // ��Ȱ��ȭ �� �߰� ���� ����
        if(!UIManager.Instance.IsActiveUI())
        {
        GameManager.Instance.Player.PlayerStateIdle();
        }
    }

    // �巡�� ���� �� ȣ��
    public void OnPointerDown(PointerEventData eventData)
    {
        // �巡�� ���� ���� UI ��ġ��, ���콺 ��ġ�� ���̸� ����Ͽ� offset�� ����
        offset = (Vector2)rectTransform.position - eventData.position;
    }

    // �巡�� �� ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        // ���콺�� ��ġ�� offset�� ���ؼ� UI�� ��ġ�� ����ؼ� �̵�
        if (CanDrag)
        {
            rectTransform.position = eventData.position + offset;
        }

    }

    // �巡�� ���� �� ȣ��
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
