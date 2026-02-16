# play_tdd_csharp

使用 C# 進行 TDD 練習的範例專案，包含兩組核心範例與對應 NUnit 測試。

## 專案現況摘要（2026-02-16）

- Solution：`play_tdd_csharp.slnx`
- Target Framework：`net10.0`
- 測試框架：`NUnit`（搭配 `Microsoft.NET.Test.Sdk`、`NUnit3TestAdapter`）
- 模擬套件：`Moq`（用於 `SlotScoreCalculator.Tests`）
- 主要範例：
  - `PrimeService`：質數判斷
  - `SlotScoreCalculator`：老虎機賠率計算

## 目錄結構

```text
01_core_console/
  PrimeService/
    PrimeService.cs
    PrimeService.csproj
  PrimeService.Tests/
    PrimeService_IsPrimeShould.cs
    PrimeService.Tests.csproj
  SlotScoreCalculator/
    SlotScoreCalculator.cs
    SlotScoreCalculator.csproj
  SlotScoreCalculator.Tests/
    SlotScoreCalculatorTest.cs
    SlotScoreCalculator.Tests.csproj
play_tdd_csharp.slnx
README.md
```

## 功能說明

### PrimeService

- 程式位置：`01_core_console/PrimeService/PrimeService.cs`
- 提供 `IsPrime(int candidate)`：
  - 小於 2 回傳 `false`
  - 2 回傳 `true`
  - 偶數回傳 `false`
  - 其餘以奇數除數檢查至 `sqrt(candidate)`

### SlotScoreCalculator

- 程式位置：`01_core_console/SlotScoreCalculator/SlotScoreCalculator.cs`
- `Calculate(int bet)` 依中線數套用賠率：
  - 0 線 -> 0 倍
  - 1 線 -> 10 倍
  - 2 線 -> 40 倍
  - 3 線 -> 100 倍
- 回傳值為 `賠率 * bet`

## 測試現況（2026-02-16）

執行指令：

```bash
dotnet test play_tdd_csharp.slnx
```

目前測試內容：

- `SlotScoreCalculator.Tests`：4 個案例（`ThreeLines`、`TwoLines`、`OneLine`、`Lose`）
- `PrimeService.Tests`：`PrimeService_IsPrimeShould` 目前被 `[Ignore("暫時停用，專心寫SUT")]` 停用，整個 fixture 會被略過

## 開發需求

- 安裝 .NET SDK 10（可由 `TargetFramework: net10.0` 推知）
- 建議使用：
  - Visual Studio 2022 / VS Code
  - `dotnet` CLI

## 快速開始

```bash
dotnet restore play_tdd_csharp.slnx
dotnet test play_tdd_csharp.slnx
```

