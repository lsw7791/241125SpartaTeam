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
    { // 생성 시 추가 변경사항
        GameManager.Instance.Player.PlayerStateUIOpen();

    }

    protected virtual void CloseProcedure()
    { // 비활성화 시 추가 변경 사항
        if(!UIManager.Instance.IsActiveUI())
        {
        GameManager.Instance.Player.PlayerStateIdle();
        }
    }

    // 드래그 시작 시 호출
    public void OnPointerDown(PointerEventData eventData)
    {
        // 드래그 시작 시의 UI 위치와, 마우스 위치의 차이를 계산하여 offset을 저장
        offset = (Vector2)rectTransform.position - eventData.position;
    }

    // 드래그 중 호출
    public void OnDrag(PointerEventData eventData)
    {
        // 마우스의 위치에 offset을 더해서 UI의 위치를 계속해서 이동
        if (CanDrag)
        {
            rectTransform.position = eventData.position + offset;
        }

    }

    // 드래그 종료 시 호출
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
