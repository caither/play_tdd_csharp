using System.ComponentModel;

namespace SlotScoreCalculator;

public class SlotScoreCalculator
{
    private List<List<string>> wheels;

    public SlotScoreCalculator(List<List<string>> wheels)
    {
        this.wheels = wheels;
    }

    public int calculate(int bet)
    {
        //預設賠率為 0 倍
        int odd = 0;
        for (int i = 0; i < 3; i++)
        {
            var distinctSymbols =
                wheels.Select(wheel => wheel[i]).Distinct().ToList();
            if (distinctSymbols.Count == 1)
            {
                //一條線中獎，獲得 40 倍賠率
                odd = 40;
                break;
            }
        }
        return odd * bet;
    }
}
