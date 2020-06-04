using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

using QRTools.UI;
using QRTools.Inputs;

using Sirenix.OdinInspector;
using DG.Tweening;

namespace CrabMaga
{
    public class LeaderToken : UIElement
    {
        public PlayerData playerData = default;

        public bool isSelected = false;
        public InputTouch inputTouchToken = default;
        public LeaderEffectEvent leaderEffectEvent = default;
        public PoolingManager poolingManager = default;
        public AP_GameManager gameManager = default;

        public Sprite ultiSprite = default;
        public Sprite noneSprite = default;
        public RectTransform mbackground = default;

        public UnityEvent onRelease = new UnityEvent();

        enum StateToken { UNIT, ULTI, NONE}
        StateToken stateToken = StateToken.UNIT;

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        bool havntUseUlt = false;

        Vector2 pos = new Vector2();

        public Image thumbnail = default;

        public Sprite dragBck, btnBck;

        public Image feedbackImage = default;
        public float timerbeforeFeedback = 5f;

        public UnityEvent onTuto;

        bool canUseUlt = true;
        public void LeaderUlt()
        {
            if (canUseUlt)
            {
                onTuto?.Invoke();
                gameManager.leaderOnBattle.UsePassif();
                StartCoroutine(CoolDown());
            }                
        }

        //public override void Init()
        //{
        //    base.Init();

        //    inputTouchToken.onTouchEnter.AddListener(OnInput);
        //    inputTouchToken.onTouchMoving.AddListener(IsSelected);
        //    inputTouchToken.onTouchEnd.AddListener(OnRelease);

        //    rectTransform = GetComponent<RectTransform>();

        //    m_Raycaster = GetComponent<GraphicRaycaster>();
        //    m_EventSystem = GetComponent<EventSystem>();

        //    pos = rectTransform.anchoredPosition;

        //    thumbnail.sprite = playerData.leader_slot?.thumbnailToken;
        //}

        //private void Start()
        //{
        //    if (AP_GameManager.Instance.levelData.levelTuto == true)
        //    {
        //        StartCoroutine(FeedbackImage());
        //    }
        //}

        //public void OnInput()
        //{
        //    m_PointerEventData = new PointerEventData(m_EventSystem);
        //    m_PointerEventData.position = Input.mousePosition;

        //    List<RaycastResult> results = new List<RaycastResult>();

        //    m_Raycaster.Raycast(m_PointerEventData, results);

        //    if (results.Count == 0)
        //    {
        //        return;
        //    }

        //    foreach (RaycastResult result in results)
        //    {
        //        if (result.gameObject.GetComponent<LeaderToken>())
        //        {
        //            isSelected = true;
        //            StopCoroutine(FeedbackImage());

        //            feedbackImage.DOColor(
        //                new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, 0f),
        //                1f
        //                );

        //            if (stateToken == StateToken.ULTI)
        //                havntUseUlt = true;
        //        }
        //    }
        //}

        //public void UseUlt()
        //{
        //    if (stateToken == StateToken.ULTI)
        //    {
        //        if (havntUseUlt)
        //        {
        //            GetComponent<Image>().sprite = noneSprite;
        //            gameManager.leaderOnBattle.UsePassif();
        //            StartCoroutine(CloseToken());
        //            stateToken = StateToken.NONE;
        //            isSelected = false;
        //            onRelease.Invoke();
        //        }
        //    }
        //}

        //public void IsSelected()
        //{
        //    switch (stateToken)
        //    {
        //        case StateToken.UNIT:
        //            if (isSelected)
        //            {
        //                rectTransform.localPosition = new Vector2(
        //                        inputTouchToken.InputCurrentPosition.x - Screen.width,
        //                        inputTouchToken.InputCurrentPosition.y);
        //            }
        //            break;
        //        case StateToken.ULTI:
        //            break;
        //        case StateToken.NONE:
        //            break;
        //    }
        //}

        //public void OnRelease()
        //{
        //    switch (stateToken)
        //    {
        //        case StateToken.UNIT:
        //            if (isSelected)
        //            {
        //                if (Vector2.Distance(rectTransform.anchoredPosition, mbackground.anchoredPosition) < 200)
        //                {
        //                    rectTransform.anchoredPosition = pos;
        //                    isSelected = false;
        //                    return;
        //                }

        //                poolingManager.InvokeLeader(
        //                    new Vector3(
        //                        inputTouchToken.RayPoint.x,
        //                        0,
        //                        gameManager.castleToDefend.transform.position.z));

        //                thumbnail.sprite = playerData.leader_slot?.thumbnailTokenUlt;
        //                background.sprite = btnBck;

        //                Debug.Log("release");

        //                GetComponent<Image>().sprite = ultiSprite;
        //                stateToken = StateToken.ULTI;
        //                rectTransform.anchoredPosition = pos;
        //                isSelected = false;
        //            }
        //            break;
        //        case StateToken.ULTI:

        //            break;
        //        case StateToken.NONE:
        //            break;
        //    }

        //    onRelease.Invoke();
        //}

        //IEnumerator CloseToken()
        //{
        //    thumbnail.sprite = playerData.leader_slot?.thumbnailTokenUlt;
        //    yield return new WaitForSeconds(4f);
        //    thumbnail.sprite = playerData.leader_slot?.thumbnailTokenNone;
        //    yield break;
        //}

        public float timer = 4;
        public Image cooldown = default;

        public IEnumerator CoolDown()
        {
            cooldown.gameObject.SetActive(true);
            cooldown.fillAmount = 1;
            canUseUlt = false;

            DOTween.To(
                () => cooldown.fillAmount,
                (x) => cooldown.fillAmount = x,
                0f,
                timer
                );

            yield return new WaitForSeconds(timer);

            canUseUlt = true;
            //ResetToken();

            yield break;
        }

        //IEnumerator FeedbackImage()
        //{
        //    yield return new WaitForSeconds(timerbeforeFeedback);

        //    feedbackImage.DOColor(
        //        new Color(feedbackImage.color.r, feedbackImage.color.g, feedbackImage.color.b, 1f),
        //        1f
        //        );

        //    yield break;
        //}

        //[Button]
        //private void ResetToken()
        //{
        //    cooldown.gameObject.SetActive(false);
        //    thumbnail.sprite = playerData.leader_slot?.thumbnail;
        //    stateToken = StateToken.UNIT;
        //    background.sprite = dragBck;
        //}
    }
}