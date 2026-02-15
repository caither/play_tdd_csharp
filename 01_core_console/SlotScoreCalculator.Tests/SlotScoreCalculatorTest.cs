namespace SlotScoreCalculator.Tests;

public class SlotScoreCalculatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TwoLines()
    {
        // 轉出兩條線
        var wheels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "4" }
        };
        var sut = new SlotScoreCalculator(wheels
            );

        //斷言：10 塊錢賠 40 倍，贏得得分應為 400
        int win = sut.calculate(10);
        Assert.That(win, Is.EqualTo(400));
    }

    [Test]
    public void OneLine()
    {
        // 轉出一條線
        var wheels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "3", "4" }
        };
        var sut = new SlotScoreCalculator(wheels
            );

        //斷言：10 塊錢賠 10 倍，贏得得分應為 100
        int win = sut.calculate(10);
        Assert.That(win, Is.EqualTo(100));
    }

    [Test]
    public void Lose()
    {
        // 轉的結果沒中
        var wheels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "2", "3", "4" }
        };
        var sut = new SlotScoreCalculator(wheels
            );

        //沒有中獎，賠率為 0，所以贏得的分數也是 0
        int win = sut.calculate(10);
        Assert.That(win, Is.EqualTo(0));
    }
}
