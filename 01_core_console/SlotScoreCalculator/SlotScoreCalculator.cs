using System.ComponentModel;

namespace SlotScoreCalculator;

public class SlotScoreCalculator
{
    private List<List<string>> reels;
    private readonly Random random;

    //設定中獎賠率
    private static readonly IReadOnlyDictionary<int, int> Odds = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 10 },
            { 2, 40 },
            { 3, 100 }
        };

    public SlotScoreCalculator(List<List<string>> reels, Random random)
    {
        this.reels = reels;

        // 為了測試方便，將 Random 注入到建構子中，這樣就可以在測試中模擬隨機數產生器的行為
        this.random = random;
    }

    public int Calculate(int bet)
    {
        var screen = new List<List<string>>();

        // 用screen代表reels隨機輪轉結果
        foreach (var reel in reels)
        {
            if (reel == null) throw new ArgumentNullException("reel 不能為空");

            // 將輪帶複製兩遍，避免 nextPosition 接近末尾時取 3 個符號越界
            // [A,B,C] => [A,B,C,A,B,C]
            var extendedWheel = reel.Concat(reel).ToList();
            if (extendedWheel.Count < 3) throw new InvalidOperationException("每個 reel 必須至少包含 3 個符號");

            // Next(max) 回傳 0..max-1
            int nextPosition = random.Next(reel.Count);

            // 對應 Java: wheel.subList(nextPosition, nextPosition + 3)
            var column = extendedWheel.GetRange(nextPosition, 3);

            screen.Add(column);

        }

        // 將轉動結果輸出到 console，方便觀察
        //RenderScreen(screen);

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
                screen.Select(reel => reel[i]).Distinct().ToList();

            //每一條線中獎
            if (distinctSymbols.Count == 1)
            {
                lines += 1;
            }
        }

        return lines;
    } // GetLines()

    // =============================
    // Screen 視覺化輸出
    // =============================
    private void RenderScreen(List<List<string>> screen)
    {
        Console.WriteLine("=== SLOT SCREEN ===");

        // row 0~2（上、中、下）
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < screen.Count; col++)
            {
                Console.Write($"[{screen[col][row]}]");
            }
            Console.WriteLine();
        }

        Console.WriteLine("===================");
        Console.WriteLine();
    }
}
