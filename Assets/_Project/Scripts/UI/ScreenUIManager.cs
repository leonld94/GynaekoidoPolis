using System;
using UnityEngine;
using static SimulationController;

public class ScreenUIManager : MonoBehaviour
{
    public SimulationClock Clock { get; set; }
    public GameState GameState { get; set; }


    [Header("UIPanel")]
    [SerializeField] private TopUIBarScript TopUIBar;
    [SerializeField] private LeftDownUIPanelScript LeftDownUIPanel;
    [SerializeField] private RightDownUIPanelScript RightDownUIPanel;
    [SerializeField] private EndingPanel EndingPanel;

    ////TopUIBar			                    Hour(식량, 자재, 인형)    Button(인형)
    ////LeftDownUIPanel	    Day(추위강도)		Hour(사망/분해)           Button(인형)
    ////RightDownUIPanel	Day(D-Day)			Hour(시간)
    ////EndingPanel								                                        별도호출
    ////LorePanel														                별도호출

    public void Initialize(SimulationClock clock, GameState gameState)
    {
        Clock = clock;
        GameState = gameState;
        TopUIBar.Initialize(gameState);
        LeftDownUIPanel.Initialize(gameState);
    }

    // 1. 날짜
    // 필요정보: 날짜; SimulationClock
    public void DayUIRefresh()
    {
        RightDownUIPanel.RescueDayUIRefresh(GameState.RescueArrivalDay - Clock.Day);
        LeftDownUIPanel.HazardLevelUIRefresh();
    }


    // 1. 자원 실제 생산/소비
    // 2. 인구 사망/분해
    // 필요정보: 자원값; GameState, 인구값; GameState
    public void HourUIRefresh()
    {

        TopUIBar.HourUIRefresh();
        LeftDownUIPanel.PopUIRefresh();
        RightDownUIPanel.TimeUIRefresh(Clock.Day, Clock.Hour);

    }

    public void PlusButtonClicked()
    {
        // 저장된 인형이 있다면
        if (0 < GameState.InactiveGynaikoeideisCount)
        {
            //Debug.Log("Plus Done");

            // 비활성화 감소
            // 활성화 증가
            GameState.InactiveGynaikoeideisCount--;
            GameState.ActiveGynaikoeideisCount++;

            LeftDownUIPanel.GynaikoeideisUIRefresh();
            TopUIBar.GynaikoeideisUIRefresh();
        }
    }

    public void MinusButtonClicked()
    {
        // 수용량에 여유가 있다면
        if (GameState.InactiveGynaikoeideisCount < GameState.InactiveGynaikoeideisStorageCapacity)
        {
            // 일하지 않는 인형이 있다면
            if (GameState.WorkingGynaikoeideisCount < GameState.ActiveGynaikoeideisCount)
            {
                //Debug.Log("Minus Done");

                // 비활성화 증가
                // 활성화 감소
                GameState.InactiveGynaikoeideisCount++;
                GameState.ActiveGynaikoeideisCount--;

                LeftDownUIPanel.GynaikoeideisUIRefresh();
                TopUIBar.GynaikoeideisUIRefresh();
            }
        }
    }

    // 주기: 버튼이 눌릴 때 => 즉 호출 경우가 버튼 눌릴 때인거잖아?
    // 1. 노동자 변화
    // 2. 자재 예측 요구량/생산량
    // 필요정보: 노동자값; GameState, 자원값; GameState
    public void GeneralUIRefresh()
    {

    }

    public void ShowVictoryUI()
    {
        // 1. GameState 정보를 가지고 패널 채우기
        int anthropoiNum = GameState.LivingAnthropoiCount;
        int gynaikoeideisNum = GameState.ActiveGynaikoeideisCount + GameState.InactiveGynaikoeideisCount;

        // 2. 패널 띄우기
        EndingPanel.ShowVictoryPanel(anthropoiNum, gynaikoeideisNum);
    }

    public void ShowFailureUI()
    {
        // 1. GameState 정보를 가지고 패널 채우기
        int livedDay = Clock.Day + Clock.Year * 15;
        int DDay = GameState.RescueArrivalDay - livedDay;

        // 2. 패널 띄우기
        EndingPanel.ShowFailurePanel(livedDay, DDay);
    }

    public void TimeSwitchChanged(bool isPaused, TimeSpeed gameSpeed)
    {
        //Debug.Log("ScreenUIManager.TimeSwitchCahged() Called");

        RightDownUIPanel.TimeSwitchChanged(isPaused, gameSpeed);
    }
}
