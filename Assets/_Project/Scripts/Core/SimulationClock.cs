/// <summary>
/// 시뮬레이션에서 흐르는 시간만을 표현하는 Class.
/// </summary>
public class SimulationClock
{

    // 시간 단위별로 나누어 계산할 수 있는 속성들
    // 1초에 10틱이 흐름. 게임에선 15일마다 1년이 흐르고 최종적으로 현실의 15분은 1년으로 설정하고 싶음.
    // 그러니 현실의 1분은 60초, 현실의 1분은 600틱, 600틱에 하루가 흘러야하고 즉 25틱이 1시간
    private const int TicksPerHour = 25;
    private const int HoursPerDay = 24;
    private const int DaysPerYear = 15;

    public long CurrentTick { get; private set; } = 0;
    public int Hour { get; private set; }
    public int Day { get; private set; }
    public int Year { get; private set; }

    public SimulationClock()
    {

    }

    public SimulationClock(SimulationClockData data)
    {
        CurrentTick = data.currentTick;

        Hour = (int)(CurrentTick / TicksPerHour % HoursPerDay);

        Day = (int)(CurrentTick / (TicksPerHour * HoursPerDay) % DaysPerYear);

        Year = (int)(CurrentTick / (TicksPerHour * HoursPerDay * DaysPerYear));
    }

    public SimulationClockData CreateData()
    {
        return new SimulationClockData
        {
            currentTick = CurrentTick
        };
    }

    public void Reset()
    {
        CurrentTick = 0;
        Hour = 0;
        Day = 0;
        Year = 0;
    }

    public TimeChange AdvanceTick()
    {
        CurrentTick++;

        bool hourChanged = false;
        bool dayChanged = false;
        bool yearChanged = false;

        if (CurrentTick % TicksPerHour == 0)
        {
            hourChanged = true;
            Hour++;

            if (Hour >= HoursPerDay)
            {
                Hour = 0;
                dayChanged = true;
                Day++;

                if (Day >= DaysPerYear)
                {
                    Day = 0;
                    yearChanged = true;
                    Year++;
                }
            }
        }

        return new TimeChange(
            hourChanged,
            dayChanged,
            yearChanged);
    }

    public override string ToString()
    {
        return $"{Year}년 {Day}일 {Hour}시, Tick: {CurrentTick}";
    }

    // Todo: 세이브파일에 시간 저장하기.
    // 파일을 뭐로 저장을 하지? Json으로 하나? 그럼 이 데이터들을 Json으로 파싱시키는걸로 하고 아님 string으로 저장하게 하든 뭐 나중가서 저장 구현할 때 생각해보지.
}


public readonly struct TimeChange
{
    public bool HourChanged { get; }
    public bool DayChanged { get; }
    public bool YearChanged { get; }

    public TimeChange(
        bool hourChanged,
        bool dayChanged,
        bool yearChanged)
    {
        HourChanged = hourChanged;
        DayChanged = dayChanged;
        YearChanged = yearChanged;
    }
}