# Rally scripts

C# scripts loaded by Space Engineers 2 via the `-loadScripts` launch parameter.

## Loading

Add the launch parameter pointing at this scripts folder:

```
-loadScripts "<path to this repo>\Scripts"
```

In Steam: right-click Space Engineers 2 → Properties → Launch Options, e.g.

```
-loadScripts "C:\Mods\SE2-Rally-Mod\Scripts"
```

> Note: `-loadScripts` is not part of Keen's documented modding workflow (the
> official docs still say scripting APIs are pending). It mirrors SE1's
> session-component mod-scripting model (`MySessionComponentBase`,
> `MyUpdateOrder`, `MyAPIGateway`). If the SE2 API surface differs, the calls
> flagged with `// Adapt` comments in the sources are the ones to adjust — the
> stage-timing logic itself is engine-agnostic.

## Files

- `RallyStageManager.cs` — stage timing: arms on the start sensor, records
  splits at checkpoints, stops at the finish, applies penalties from
  `Data/rally-tuning.json` (+5 s missed checkpoint, +10 s lost wheel).
- `RallyHud.cs` — HUD speed/stage-time readout for the controlled vehicle.

## Block naming conventions

Name blocks in-world (or in the blueprints) with these prefixes so the
manager can find them on any grid:

| Prefix | Block |
|---|---|
| `RallyStart` | start gate sensor |
| `RallyCP` | checkpoint sensor (e.g. `RallyCP1`, `RallyCP2`, …) |
| `RallyFinish` | finish gate sensor |
