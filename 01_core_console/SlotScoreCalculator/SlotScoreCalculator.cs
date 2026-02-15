using System.ComponentModel;

namespace SlotScoreCalculator;

public class SlotScoreCalculator
{
    private List<List<string>> wheels;

    public SlotScoreCalculator(List<List<string>> wheels)
    {
        this.wheels = wheels;
    }

    public int Calculate(int bet)
    {
        int odd = GetOdd();
        return odd * bet;
    }

    private int GetOdd()
    {
        int lines = GetLines();
        return GetOddFromLines(lines);
    }

    private static int GetOddFromLines(int lines)
    {
        //預設賠率為 0 倍
        int odd = 0;

        //設定中獎賠率
        if (lines == 0) { odd = 0; }
        else if (lines == 1) { odd = 10; }
        else if (lines == 2) { odd = 40; }
        else
        {
            throw new InvalidOperationException("TBD");
        }
        return odd;
    }

    private int GetLines()
    {
        //加總中獎行數
        int lines = 0;

        for (int i = 0; i < 3; i++)
        {
            var distinctSymbols =
                wheels.Select(wheel => wheel[i]).Distinct().ToList();

            //每一條線中獎
            if (distinctSymbols.Count == 1)
            {
                lines += 1;
            }
        }

        return lines;
    }
}
