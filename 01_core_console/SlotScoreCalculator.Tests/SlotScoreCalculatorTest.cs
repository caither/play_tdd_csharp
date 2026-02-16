using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace SlotScoreCalculator.Tests;

[TestFixture]
public class SlotScoreCalculatorTests
{
    private Mock<Random> _randomMock;

    [SetUp]
    public void Setup()
    {
        _randomMock = new Mock<Random>();
    }

    [Test]
    public void ThreeLines()
    {
        // 模擬隨機數產生器，讓它每次都回傳 0，這樣就能確保每條線都會轉出 "A"
        _randomMock.Setup(r => r.Next(It.IsAny<int>()))
            .Returns(0);

        // 轉出三條線
        var reels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" }
        };
        var sut = new SlotScoreCalculator(reels
            , _randomMock.Object);

        //斷言：10 塊錢賠 100 倍，贏得得分應為 1000
        int win = sut.Calculate(10);
        Assert.That(win, Is.EqualTo(1000));
    }

    [Test]
    public void TwoLines()
    {
        // 模擬隨機數產生器，讓它每次都回傳 0 (不轉動)
        _randomMock.Setup(r => r.Next(It.IsAny<int>()))
    .Returns(0);

        // 轉出兩條線
        var reels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "4" }
        };
        var sut = new SlotScoreCalculator(reels, _randomMock.Object
            );

        //斷言：10 塊錢賠 40 倍，贏得得分應為 400
        int win = sut.Calculate(10);
        Assert.That(win, Is.EqualTo(400));
    }

    [Test]
    public void OneLine()
    {
        // 模擬隨機數產生器，讓它每次都回傳 0 (不轉動)
        _randomMock.Setup(r => r.Next(It.IsAny<int>()))
    .Returns(1);

        // 轉出一條線
        var reels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "3", "4" }
        };
        var sut = new SlotScoreCalculator(reels, _randomMock.Object
            );

        //斷言：10 塊錢賠 10 倍，贏得得分應為 100
        int win = sut.Calculate(10);
        Assert.That(win, Is.EqualTo(100));
    }

    [Test]
    public void Lose()
    {
        // 模擬隨機數產生器，讓它每次都回傳 0 (不轉動)
        _randomMock.Setup(r => r.Next(It.IsAny<int>()))
    .Returns(2);

        // 轉的結果沒中
        var reels = new List<List<string>>
        {
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "A", "2", "3" },
                new() { "2", "3", "4" }
        };
        var sut = new SlotScoreCalculator(reels, _randomMock.Object
            );

        //沒有中獎，賠率為 0，所以贏得的分數也是 0
        int win = sut.Calculate(10);
        Assert.That(win, Is.EqualTo(0));
    }
}
