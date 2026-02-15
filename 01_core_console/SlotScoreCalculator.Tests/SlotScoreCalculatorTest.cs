namespace SlotScoreCalculator.Tests;

public class SlotScoreCalculatorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Lose()
    {
        SlotScoreCalculator sut = new SlotScoreCalculator();
        int win = sut.calculate();

        Assert.That(win, Is.EqualTo(0));
    }
}
