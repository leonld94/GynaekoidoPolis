using UnityEngine;

public interface ISelectable
{
    /// <summary>
    /// 선택 가능임을 표지하는 외곽 모서리 발광  함수
    /// </summary>
    public void HighlightOn();
    public void HighlightOff();

    /// <summary>
    /// 선택됨을 표지하는 외곽 모서리 발광 함수 (위랑 두께가 다르던 할듯)
    /// </summary>
    public void SelectedHighlight();

    /// <summary>
    /// 선택 시 소유하고 있는 패널을 띄워줌
    /// </summary>
    public void ShowUIPanel();


    public void HideUIPanel();
}

public interface ISelectableBuilding : ISelectable
{
    public BuildingState SelectableBuildingState { get; }
}
