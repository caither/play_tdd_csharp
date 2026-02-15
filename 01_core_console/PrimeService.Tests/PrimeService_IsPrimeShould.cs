using NUnit.Framework;
using PrimeService;

namespace PrimeService.Tests;

[Ignore("暫時停用，專心寫SUT")]
[TestFixture]
public class PrimeService_IsPrimeShould
{
    private PrimeService _primeService;

    [SetUp]
    public void Setup()
    {
        _primeService = new PrimeService();
    }

    //[Test]
    //public void IsPrime_InputIs1_ReturnFalse()
    //{
    //    var result = _primeService.IsPrime(1);
    //    Assert.That(result, Is.False, "1 應該是質數");
    //}
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
    {
        var result = _primeService.IsPrime(value);
        Assert.That(result, Is.False, $"{value} 應該不是質數");
    }
}
