# SE2 Rally Mod — Build Guide

This guide turns the repo contents into a publishable Space Engineers 2 mod.
Blocks are pure data: a VRAGE3 prefab plus definition (`.def`) files. Gameplay logic
is available two ways: C# scripts loaded via the `-loadScripts` launch parameter
(`Scripts/SE2RallyMod/`, recommended for full timing/splits/penalties), or vanilla
automation wiring (sensor → event controller → timer → light) as a no-launch-parameter
fallback.

## 0. Prerequisites

- Space Engineers 2 (VS2.3 or newer — needs wheels, hinges, event controllers, timers, sensors)
- Space Engineers 2 Mod SDK (Steam → Library → Tools)
- Blender with the **VRAGE Tools** add-on (only if editing models in `Source/Blender/`)

## 1. Create the mod project

1. Launch **Space Engineers 2 Modding Tools** (loads the VRAGE3 Mod HUB).
2. **+ Add Mod** → Title: `SE2 Rally Mod` → OK.
3. Press **Start Editor**.
4. Keep the HUB's mod folder open in Windows Explorer; you'll mirror this repo's
   `Assets/` structure there (VRAGE3 uses a referencing system, so folder names
   are free-form — this layout just matches the vanilla taxonomy for sanity).

## 2. Rally Wheel (`Assets/Blocks/RallyWheel/`)

Right-click → **Create a Prefab** → pick the wheel template (breakable), or
right-click the vanilla wheel 5x5 prefab → **Create Partial Copy**, then:

| Editor field | Value | Source |
|---|---|---|
| Prefab name | `RallyWheel50` | — |
| Friction (Wheel Definition) | 1.25 | `Data/rally-tuning.json` |
| Max speed | 45 m/s | `Data/rally-tuning.json` |
| Suspension travel | 0.55 m | `Data/rally-tuning.json` |
| Strength | 120% of vanilla | `Data/rally-tuning.json` |
| Model | `Source/Blender/RallyWheel/rally_wheel.fbx` | import via editor |

Export the prefab + defs into `Assets/Blocks/RallyWheel/50/`.
`docs/TUNING.md` lists every field with its vanilla default and rally value.

## 3. Start Gate (`Assets/Blocks/RallyStartGate/`)

Partial-copy the vanilla sensor block, then:

1. Rename prefab to `RallyStartGate`.
2. Add a **light component** and **event controller component** (VRAGE3 allows
   multiple block kinds per prefab — no SE1-style entity-type lock-in).
3. Wire the sensor (range 4 m, width matched to gate) to:
   - event controller output A → staging lights (red)
   - event controller output B (3 s delay via a Timer) → light color green
   - event controller output C → Timing Console timer start
4. Save the sensor/event-controller/timer/definitions into
   `Assets/Blocks/RallyStartGate/50/`.

## 4. Checkpoint Beacon (`Assets/Blocks/RallyCheckpoint/`)

Partial-copy a light block + sensor (range 6 m). Sensor trigger → event
controller → Timing Console split input. Model source:
`Source/Blender/RallyCheckpoint/checkpoint_pylon.fbx`.

## 5. Timing Console (`Assets/Blocks/RallyTimingConsole/`)

Partial-copy the vanilla timer block; set it to count up and add event
controller inputs for Start (from gate) and Stop (from finish sensor). Checkpoints
send split events. Store the finished prefab under
`Assets/Blocks/RallyTimingConsole/50/`.

## 6. Rally Number Plate (`Assets/Blocks/RallyPlate/`)

Follow Keen's "Creating an Armor Block" guide with the source plates in
`Source/Blender/RallyPlate/`. Set **Block Kind** to a partial override of the
vanilla light armor block kind, then assign the rally livery material created
from `Assets/BlockMaterials/RallyLivery/` (import the PNGs, create the
`.comptex` and material `.def` in-editor).

## 7. G-Screen entries (`Data/gscreen.json`)

Follow the official "Guide: G-Screen" to add each rally block to the G-Screen
under a new `Rally` category, using the display names and icon paths listed in
`Data/gscreen.json`.

## 8. Blueprints

Build the rally car and track pieces in-game (or in editor), then save them as
blueprints into `Assets/Blueprints/`:
- `RallyCar_Gravel`, `RallyCar_Sand`, `RallyCar_Ice` (see `Data/stage-presets.json`
  for per-surface wheel/suspension settings)
- `RallyStartGate`, `RallyCheckpointPylon`, `RallyFinishGate`

## 9. Validate & publish

1. Open the **Asset Validation** window: fix every **error** (mandatory) and
   address **warnings** before publishing.
2. **Start Game** from the HUB/Editor and run a test stage end-to-end.
3. Publish from the HUB with the **Publish** button.

## Repo ↔ editor mapping

| Repo file | Editor action |
|---|---|
| `Data/rally-tuning.json` | values typed into wheel/suspension definitions |
| `Data/stage-presets.json` | per-surface blueprint settings |
| `Data/gscreen.json` | G-Screen category + entries |
| `Source/Blender/**` | import via Blender VRAGE Tools → editor asset generation |
| `Assets/**` | where the editor writes prefabs + `.def` files |
