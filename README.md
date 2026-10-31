# SE2 Rally Mod

A rally mod for **Space Engineers 2** — turn the unified 25 cm grid, wheels, hinges,
timers, sensors, and event controllers into a planetary rally racing experience.

> Requires Space Engineers 2 (VS2.3 "Drive, Automate & Detonate" or newer) and the
> **Space Engineers 2 Mod SDK** (VRAGE3 Mod HUB + Mod Editor).

## What's in the mod

| Feature | Description |
|---|---|
| Rally Wheel | High-grip, long-travel wheel preset tuned for loose planetary surfaces |
| Rally Stage Start Gate | Sensor-gated start line with event-controller staging lights |
| Rally Checkpoint Beacon | Light + sensor checkpoint pylon for timing runs |
| Rally Timing Console | Event-controller based stage timer (start/finish splits) |
| Rally Number Plate | Light armor plate variant with rally livery color preset |
| Stage Presets | Three ready-to-load vehicle setups: Gravel, Sand, Ice |

Each block is a VRAGE3 **prefab** (component container) plus its **definition** (`.def`)
data files. SE2 has no scripting API yet, so all logic uses vanilla automation blocks
(sensor → event controller → timer/light), packaged as copy-paste-able blueprints.

## Repository layout

```
Assets/
  Blocks/            Rally block prefabs + defs (created via VRAGE3 Mod Editor)
  BlockMaterials/    Rally livery materials (built in editor from Blender source)
  Blueprints/        Blueprint .sbc-style source for the rally car & track pieces
Data/
  rally-tuning.json  Reference values used when tuning each block in the editor
  gscreen.json       G-Screen (toolbar) category entries for the rally blocks
  stage-presets.json Wheel/suspension presets per surface type
Source/
  Blender/           .blend + .fbx sources for models (LODs, collisions, mount points)
docs/
  BUILD.md           Step-by-step: turning this repo into a publishable SE2 mod
  RACE_RULES.md      Suggested rally stage rules and timing flow
  TUNING.md          How each value in rally-tuning.json maps to editor fields
```

**Important:** VRAGE3 `.def`/prefab files are serialized editor assets — they cannot be
hand-edited like SE1 `.sbc` XML. The files under `Assets/` are the target structure the
Mod Editor generates; `docs/BUILD.md` walks through creating each one, and
`Data/*.json` holds every value you need to type into the inspector.

## Quick start

1. Install the **Space Engineers 2 Mod SDK** from Steam (Library → Tools).
2. Launch it, press **+ Add Mod** in the VRAGE3 Mod HUB, title it `SE2 Rally Mod`.
3. Follow `docs/BUILD.md` — it maps every file in this repo to an editor action.
4. Test with **Start Game** from the HUB, then **Publish** to the Steam Workshop.

## Racing flow (in-game)

1. Place the **Start Gate**; the sensor arms the staging lights.
2. Green light → go; the gate's event controller starts the **Timing Console** timer.
3. Cross each **Checkpoint Beacon** for split times.
4. Finish sensor stops the timer and prints your stage time.

See `docs/RACE_RULES.md` for the full ruleset and track-building guide.

## License

MIT — see [LICENSE](LICENSE).
