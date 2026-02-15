using System.ComponentModel;

namespace SlotScoreCalculator;

public class SlotScoreCalculator
{
    private List<List<string>> wheels;
    private readonly Random random;

    //設定中獎賠率
    private static readonly IReadOnlyDictionary<int, int> Odds = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 10 },
            { 2, 40 },
            { 3, 100 }
        };

    public SlotScoreCalculator(List<List<string>> wheels, Random random)
    {
        this.wheels = wheels;
        this.random = random;
    }

    public int Calculate(int bet)
    {
        List<List<string>> screen = wheels;

        // --用screen代表wheels隨機輪轉結果--

        int odd = GetOdd(screen);
        return odd * bet;
    }

    private int GetOdd(List<List<string>> screen)
    {
        int lines = GetLines(screen);
        return GetOddFromLines(lines);
    }

    private static int GetOddFromLines(int lines)
    {

        // 對標 odds.get(lines) & catch Objects.isNull(odd)
        if (!Odds.TryGetValue(lines, out int odd))
        {
            throw new InvalidOperationException($"Unsupported lines: {lines}");
        }

        return odd;
    }

    private int GetLines(List<List<string>> screen)
    {
        //加總中獎行數
        int lines = 0;

        for (int i = 0; i < 3; i++)
        {
            var distinctSymbols =
                screen.Select(wheel => wheel[i]).Distinct().ToList();

            //每一條線中獎
            if (distinctSymbols.Count == 1)
            {
                lines += 1;
            }
        }

        return lines;
    }
}
