using System;
namespace PrimeService;

/// <summary>
/// 提供整數是否為質數的判斷功能。
/// </summary>
public class PrimeService
{
    /// <summary>
    /// 判斷指定整數是否為質數。
    /// </summary>
    /// <param name="candidate">要判斷的整數。</param>
    /// <returns>
    /// 若 <paramref name="candidate"/> 為質數則回傳 <c>true</c>；否則回傳 <c>false</c>。
    /// </returns>
    public bool IsPrime(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }
        if (candidate == 2)
        {
            return true;
        }

        if (candidate % 2 == 0)
        {
            return false;
        }

        var limit = (int)Math.Sqrt(candidate);
        for (var divisor = 3; divisor <= limit; divisor += 2)
        {
            if (candidate % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }

}
