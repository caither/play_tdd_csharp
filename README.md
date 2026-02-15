# play_tdd_csharp

以 C# 練習 TDD（測試驅動開發）的範例專案，主要放置核心邏輯與對應測試。

## 專案目標

- 使用小型題目練習 Red-Green-Refactor 流程
- 建立可重複執行的單元測試習慣
- 將商業邏輯與測試專案分離，保持結構清楚

## 專案結構

```text
01_core_console/
  PrimeService/
  PrimeService.Tests/
  SlotScoreCalculator/
  SlotScoreCalculator.Tests/
play_tdd_csharp.slnx
```

- `PrimeService`：質數判斷邏輯（已實作）
- `PrimeService.Tests`：`PrimeService` 的 NUnit 測試（已建立基本案例）
- `SlotScoreCalculator`：拉霸分數計算邏輯（目前為骨架）
- `SlotScoreCalculator.Tests`：`SlotScoreCalculator` 的 NUnit 測試（目前為基本範本）

## 技術與測試框架

- .NET：`net10.0`
- 測試框架：`NUnit`
- 測試執行：`Microsoft.NET.Test.Sdk` + `NUnit3TestAdapter`
- 覆蓋率工具：`coverlet.collector`

## 如何執行測試

在專案根目錄執行：

```bash
dotnet test play_tdd_csharp.slnx
```

## 目前狀態

- `PrimeService`：已有質數判斷實作與基礎測試
- `SlotScoreCalculator`：待實作核心邏輯與完整測試案例
