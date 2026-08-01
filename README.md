# CronExpressionFluent

A fluent, type-safe builder API for constructing cron expressions in .NET — no more hand-typing positional strings and hoping the fifth field means what you think it means.

```csharp
var cron = CronExpression.Create()
    .EveryMinutes(5)
    .Build();
// "*/5 * * * *"
```

Supports both the **five-field Unix** format and the **six-field Quartz** format, through a single consistent API. Method names read like a sentence, IntelliSense tells you what's valid next, and invalid values fail immediately with a message that names the field, not just "malformed expression."

[![NuGet](https://img.shields.io/badge/nuget-CronExpressionFluent-blue)](https://www.nuget.org/packages/CronExpressionFluent)
[![License: MIT](https://img.shields.io/badge/license-MIT-green)](#license)
[![.NET Standard 2.1](https://img.shields.io/badge/.NET%20Standard-2.1-purple)](#requirements)

---

## Why

A cron string like `0 30 9 ? * MON-FRI` is compact, portable, and nearly opaque. The syntax is positional — swap two fields and you get a different but *still valid* expression, so a typo doesn't fail loudly, it just fires at the wrong time, or never fires at all. Dialect differences make it worse: Quartz needs a `?` placeholder in exactly one of two fields, day-of-week numbering isn't consistent across tools, and modifiers like `L`, `W`, and `#` mean nothing without the manual open.

CronExpressionFluent replaces the string with a chain of named methods:

```csharp
// Instead of this...
var cron = "0 30 9 ? * MON-FRI";

// ...you write this:
var cron = CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).AtMinute(30).AtHour(9)
    .OnWeekdays()
    .Build();
```

The `?` placeholder above was never typed by the developer — it was inserted automatically because `OnWeekdays()` claimed the day-of-week field. See [Quartz day-of-month / day-of-week resolution](#quartz-day-of-month--day-of-week-resolution).

---

## Table of contents

- [Install](#install)
- [Quick start](#quick-start)
- [Unix vs. Quartz](#unix-vs-quartz)
- [Field reference](#field-reference)
  - [Second (Quartz only)](#second-quartz-only)
  - [Minute](#minute)
  - [Hour](#hour)
  - [Day of month](#day-of-month)
  - [Month](#month)
  - [Day of week](#day-of-week)
- [Quartz day-of-month / day-of-week resolution](#quartz-day-of-month--day-of-week-resolution)
- [Presets](#presets)
- [Error handling](#error-handling)
- [More examples](#more-examples)
- [Known limitations](#known-limitations)
- [Requirements](#requirements)
- [License](#license)

---

## Install

```bash
dotnet add package CronExpressionFluent
```

## Quick start

```csharp
using CronExpressionFluent;
using CronExpressionFluent.Enums;

// Unix format (5 fields) — the default
string everyFiveMinutes = CronExpression.Create()
    .EveryMinutes(5)
    .Build();
// "*/5 * * * *"

// Quartz format (6 fields, seconds + special modifiers)
string weekdayMornings = CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).AtMinute(30).AtHour(9)
    .OnWeekdays()
    .Build();
// "0 30 9 ? * MON-FRI"
```

`CronBuilder` also overrides `ToString()`, so you can pass the builder itself anywhere a string is expected without calling `.Build()` explicitly.

## Unix vs. Quartz

Pick the format at creation time; the builder exposes only what's valid for it.

```csharp
CronExpression.Create()                      // Unix, 5 fields
CronExpression.Create(CronFormat.Quartz)     // Quartz, 6 fields
```

| | Unix (5-field) | Quartz (6-field) |
|---|---|---|
| Fields | minute hour day-of-month month day-of-week | second minute hour day-of-month month day-of-week |
| Seconds | not available | `AtSecond`, `EverySecond`, `EverySeconds`, `BetweenSeconds` |
| `L`, `W`, `LW`, `#` modifiers | not available | available |
| `?` placeholder | not applicable | resolved automatically |

Calling a Quartz-only method (`AtSecond`, `OnLastDayOfMonth`, `OnWeekdayNearestTo`, `OnLastWeekdayOfMonth`, `OnNthWeekdayOfMonth`, `OnLastWeekday`, …) on a Unix builder throws immediately:

```csharp
CronExpression.Create().AtSecond(0).Build();
// InvalidOperationException:
// "AtSecond() is only available in Quartz format.
//  Use CronExpression.Create(CronFormat.Quartz) to enable it."
```

## Field reference

Every method returns the builder, so calls chain freely and in any order.

### Second (Quartz only)

Defaults to `0` — a wildcard seconds field would fire sixty times a minute, which is essentially never what's intended.

| Method | Produces | Example |
|---|---|---|
| `AtSecond(int)` | a fixed second | `.AtSecond(30)` → `30` |
| `EverySecond()` | `*` | fires every second |
| `EverySeconds(int step)` | `*/n` | `.EverySeconds(15)` → `*/15` |
| `BetweenSeconds(int, int)` | `start-end` | `.BetweenSeconds(0, 30)` → `0-30` |

### Minute

| Method | Produces | Example |
|---|---|---|
| `AtMinute(int)` | a fixed minute | `.AtMinute(30)` → `30` |
| `EveryMinute()` | `*` | fires every minute |
| `EveryMinutes(int step)` | `*/n` | `.EveryMinutes(5)` → `*/5` |
| `BetweenMinutes(int, int)` | `start-end` | `.BetweenMinutes(0, 15)` → `0-15` |
| `AtMinutes(params int[])` | comma list | `.AtMinutes(0, 15, 30, 45)` → `0,15,30,45` |

### Hour

| Method | Produces | Example |
|---|---|---|
| `AtHour(int)` | a fixed hour | `.AtHour(9)` → `9` |
| `EveryHour()` | `*` | fires every hour |
| `EveryHours(int step)` | `*/n` | `.EveryHours(4)` → `*/4` |
| `BetweenHours(int, int)` | `start-end` | `.BetweenHours(9, 17)` → `9-17` |
| `AtHours(params int[])` | comma list | `.AtHours(9, 13, 17)` → `9,13,17` |

### Day of month

| Method | Produces | Notes |
|---|---|---|
| `OnDay(int)` | a fixed day | `1`–`31` |
| `OnDays(params int[])` | comma list | e.g. `1,15` |
| `EveryDay()` | `*` | |
| `OnLastDayOfMonth()` | `L` | Quartz only |
| `OnWeekdayNearestTo(int day)` | `nW` | Quartz only, e.g. `.OnWeekdayNearestTo(15)` → `15W` |
| `OnLastWeekdayOfMonth()` | `LW` | Quartz only |

### Month

| Method | Produces | Example |
|---|---|---|
| `InMonth(Month)` | 3-letter abbreviation | `.InMonth(Month.March)` → `MAR` |
| `InMonths(params Month[])` | comma list | `.InMonths(Month.March, Month.June)` → `MAR,JUN` |
| `BetweenMonths(Month, Month)` | `start-end` | `.BetweenMonths(Month.March, Month.September)` → `MAR-SEP` |
| `EveryMonth()` | `*` | |
| `InMonthNumber(int)` | numeric | `1`–`12`, when the enum isn't convenient |
| `InMonthNumbers(params int[])` | comma list | numeric equivalents |

### Day of week

`CronDayOfWeek` follows standard cron convention: `Sunday = 0` … `Saturday = 6`.

| Method | Produces | Notes |
|---|---|---|
| `OnWeekday(CronDayOfWeek)` | 3-letter abbreviation | e.g. `MON` |
| `OnWeekdays()` | `MON-FRI` | |
| `OnWeekends()` | `SAT,SUN` | |
| `OnDaysOfWeek(params CronDayOfWeek[])` | comma list | e.g. `MON,WED,FRI` |
| `BetweenDaysOfWeek(CronDayOfWeek, CronDayOfWeek)` | `start-end` | |
| `OnNthWeekdayOfMonth(CronDayOfWeek, int nth)` | `DAY#n` | Quartz only, `nth` in `1`–`5`, e.g. `MON#2` |
| `OnLastWeekday(CronDayOfWeek)` | `DAYL` | Quartz only, e.g. `FRIL` |
| `OnDayOfWeekNumber(int)` | numeric `0`–`6` | Unix-style numeric day |
| `BetweenDayOfWeekNumbers(int, int)` | numeric range | |

## Quartz day-of-month / day-of-week resolution

Quartz requires that exactly one of the day-of-month and day-of-week fields carry a value, with the other set to the `?` placeholder — a rule with no Unix equivalent and a common source of parse errors.

The builder tracks whether each of the two fields was explicitly set, and at `Build()` time:

- If only day-of-week was set → day-of-month is set to `?`.
- If only day-of-month was set → day-of-week is set to `?`.
- If neither was set → day-of-week defaults to `?` (day-of-month stays `*`).

```csharp
CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).AtMinute(0).AtHour(10)
    .OnNthWeekdayOfMonth(CronDayOfWeek.Monday, 2)
    .Build();
// "0 0 10 ? * MON#2"   ← the "?" was never typed by hand
```

See [Known limitations](#known-limitations) for the one case this doesn't currently cover.

## Presets

`CronExpression` also exposes ready-made shortcuts for common Unix schedules, returning the finished string directly — no `.Build()` needed:

| Method | Result |
|---|---|
| `EveryMinute()` | `* * * * *` |
| `EveryNMinutes(int n)` | `*/n * * * *` |
| `Hourly()` | `0 * * * *` |
| `HourlyAt(int minute)` | `minute * * * *` |
| `Daily()` | `0 0 * * *` |
| `DailyAt(int hour, int minute = 0)` | `minute hour * * *` |
| `Weekly(CronDayOfWeek day)` | `0 0 * * day` |
| `Monthly()` | `0 0 1 * *` |
| `Yearly()` | `0 0 1 1 *` |

```csharp
CronExpression.DailyAt(9, 30);        // "30 9 * * *"
CronExpression.Weekly(CronDayOfWeek.Monday); // "0 0 * * MON"
```

## Error handling

Every field method validates its own input the moment it receives it, so the exception fires on the line that caused it, naming the field and the value:

```csharp
CronExpression.Create().AtMinute(75).Build();
// InvalidCronFieldException:
// "Invalid value for cron field 'Minute': Value 75 is out of range [0, 59]."

CronExpression.Create().EveryMinutes(0).Build();
// InvalidCronFieldException:
// "Invalid value for cron field 'Minute': Step value must be positive, got 0."

CronExpression.Create(CronFormat.Quartz).OnNthWeekdayOfMonth(CronDayOfWeek.Monday, 6).Build();
// InvalidCronFieldException:
// "Invalid value for cron field 'DayOfWeek': Nth value must be 1-5, got 6."

CronExpression.Create().AtSecond(0).Build();
// InvalidOperationException:
// "AtSecond() is only available in Quartz format.
//  Use CronExpression.Create(CronFormat.Quartz) to enable it."
```

`InvalidCronFieldException` (a subclass of `ArgumentException`) exposes `FieldName` for programmatic handling.

## More examples

```csharp
// Every 15 minutes during business hours, weekdays only
var cron = CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).EveryMinutes(15).BetweenHours(9, 17)
    .OnWeekdays()
    .Build();
// "0 */15 9-17 ? * MON-FRI"

// Last weekday of the month at 18:00
var cron = CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).AtMinute(0).AtHour(18)
    .OnLastWeekdayOfMonth()
    .Build();
// "0 0 18 LW * ?"

// March through September, Monday/Wednesday/Friday at 06:30
var cron = CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).AtMinute(30).AtHour(6)
    .BetweenMonths(Month.March, Month.September)
    .OnDaysOfWeek(CronDayOfWeek.Monday, CronDayOfWeek.Wednesday, CronDayOfWeek.Friday)
    .Build();
// "0 30 6 ? MAR-SEP MON,WED,FRI"

// Nearest weekday to the 15th of every month
var cron = CronExpression.Create(CronFormat.Quartz)
    .AtSecond(0).AtMinute(0).AtHour(8)
    .OnWeekdayNearestTo(15)
    .Build();
// "0 0 8 15W * ?"
```

## Known limitations

- **Quartz day-field conflict isn't rejected.** If both a day-of-month value *and* a day-of-week value are explicitly set on a Quartz builder (e.g. `.OnDay(15).OnWeekdays()`), `Build()` currently returns a string with both fields populated instead of throwing — Quartz's own parser will reject that expression, but this library doesn't catch it first. Avoid setting both fields on the same Quartz builder until this is addressed; a cross-field validation pass at `Build()` time is the planned fix.
- No `Parse(string)` for round-tripping an existing cron string into a builder.
- No `Describe()` for a human-readable rendering of the built expression.
- No support for other dialects (AWS EventBridge, Kubernetes CronJob) yet.

Issues and PRs welcome for any of the above.

## Requirements

- .NET Standard 2.1 (compatible with .NET 5+, .NET Core 3.0+, Mono, Xamarin, Unity)
- No external dependencies

## License

MIT