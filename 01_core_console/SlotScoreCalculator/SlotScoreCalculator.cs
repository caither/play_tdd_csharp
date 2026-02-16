using System.ComponentModel;

namespace SlotScoreCalculator;

public class SlotScoreCalculator
{
    private readonly List<List<string>> _reels;
    private readonly Random _random;
    private readonly PayTable _payTable;

    public SlotScoreCalculator(List<List<string>> reels, Random random, PayTable payTable)
    {
        _reels = reels;

        // 為了測試方便，將 Random 注入到建構子中，這樣就可以在測試中模擬隨機數產生器的行為
        _random = random;

        _payTable = payTable;
    }

    public int Calculate(int bet)
    {

        List<List<string>> screen = GetScreen();

        // 將轉動結果輸出到 console，方便觀察
        //RenderScreen(screen);

        int odd = _payTable.GetOdd(screen);
        return odd * bet;
    }

    private List<List<string>> GetScreen()
    {
        // 對每個 reel 進行轉動，取出螢幕上顯示的 3 符號
        // 對應 Java: reels.stream().map(reel -> { ... }
        List<List<string>> screen = _reels.Select(
            reel =>
            {
                int reelSize = reel.Count;

                if (reel == null) throw new ArgumentNullException("reel 不能為空", nameof(_reels));
                if (reelSize == 0) throw new ArgumentException("reel 不能為空", nameof(_reels));

                // Next(max) 回傳 0..max-1
                int nextPosition = _random.Next(reelSize);

                // 避免 nextPosition 接近末尾時取 3 個符號越界
                // 改掉將輪帶複製兩遍的做法，直接在取符號時使用模運算來繞回輪帶的開頭
                return new List<string>
                    {
                        reel[(nextPosition + 0) % reelSize],
                        reel[(nextPosition + 1) % reelSize],
                        reel[(nextPosition + 2) % reelSize]
                    };
            }
        ).ToList();
        return screen;
    }

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
} // class SlotScoreCalculator

public class PayTable
{
    //設定中獎賠率
    private static readonly IReadOnlyDictionary<int, int> Odds = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 10 },
            { 2, 40 },
            { 3, 100 }
        };

    public int GetOdd(List<List<string>> screen)
    {
        int lines = GetLines(screen);
        return GetOddFromLines(lines);
    }

    private int GetLines(List<List<string>> screen)
    {
        if (screen == null) throw new ArgumentNullException(nameof(screen));
        if (screen.Count == 0) return 0;

        //加總中獎行數
        int lines = 0;

        for (int i = 0; i < 3; i++)
        {
            int rowIndex = i;

            var distinctSymbols = screen
                .Select(column => column[rowIndex])
                .ToHashSet();

            //每一條線中獎
            if (distinctSymbols.Count == 1)
            {
                lines++;
            }
        }

        return lines;
    }

    private int GetOddFromLines(int lines)
    {
        // 對標 odds.get(lines) & catch Objects.isNull(odd)
        if (!Odds.TryGetValue(lines, out int odd))
        {
            throw new InvalidOperationException($"Unsupported lines: {lines}");
        }

        return odd;
    }
} //class PayTable


