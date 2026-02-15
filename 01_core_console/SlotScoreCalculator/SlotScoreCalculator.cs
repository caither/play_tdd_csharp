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
        //設定中獎賠率
        var odds = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 10 },
            { 2, 40 },
            { 3, 100 }
        };

        // 對標 odds.get(lines) & catch Objects.isNull(odd)
        if (!odds.TryGetValue(lines, out int odd))
        {
            throw new InvalidOperationException($"Unsupported lines: {lines}");
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
