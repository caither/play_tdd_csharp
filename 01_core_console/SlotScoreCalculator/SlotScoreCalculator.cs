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

        //加總中獎行數
        int sumOfSameLines = 0;

        for (int i = 0; i < 3; i++)
        {
            var distinctSymbols =
                wheels.Select(wheel => wheel[i]).Distinct().ToList();

            //每一條線中獎
            if (distinctSymbols.Count == 1)
            {
                sumOfSameLines += 1;
            }

            //設定中獎賠率
            if (sumOfSameLines == 0) { odd = 0; }
            else if (sumOfSameLines == 1) { odd = 10; }
            else if (sumOfSameLines == 2) { odd = 40; }
            else
            {
                throw new InvalidOperationException("TBD");
            }
        }
        return odd * bet;
    }
}
