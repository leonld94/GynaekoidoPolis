using System;
using System.Collections.Generic;
using System.Text;

internal interface IWorkable
{
    /// <summary>
    /// 노동을 수행하는 Method. 그런데 산출물 값은 Simulation에서 상수로 다루려고 여기서 안함.
    /// </summary>
    /// <returns>현재 노동자 수를 반환</returns>
    public int Work();

    /// <summary>
    /// 인간 노동자 추가
    /// </summary>
    /// <returns>성공 여부 출력</returns>
    public bool AssignAnthroposWorker();

    /// <summary>
    /// 인간 노동자 감소
    /// </summary>
    /// <returns>성공 여부 출력</returns>
    public bool FireAnthroposWorker();

    /// <summary>
    /// 인형 노동자 추가
    /// </summary>
    /// <returns>성공 여부 출력</returns>
    public bool AssignGynaikoeidesWorker();

    /// <summary>
    /// 인형 노동자 감소
    /// </summary>
    /// <returns>성공 여부 출력</returns>
    public bool FireGynaikoeidesWorker();
}
