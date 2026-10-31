# VRAGE3 editor assets

The VRAGE3 Mod Editor writes prefabs, definitions (`.def`), materials
(`.comptex`) and model files here. These are serialized editor assets and
cannot be hand-edited — create them by following `docs/BUILD.md`.

Expected content once built:

- `Blocks/RallyWheel/50/` — Rally Wheel prefab + wheel/suspension defs
- `Blocks/RallyStartGate/50/` — start gate prefab + sensor/event/light defs
- `Blocks/RallyCheckpoint/50/` — checkpoint pylon prefab + defs
- `Blocks/RallyTimingConsole/50/` — timing console prefab + timer defs
- `Blocks/RallyPlate/50/` — armor plate composite (`.comparm`) + light/heavy variants
- `BlockMaterials/RallyLivery/` — livery material `.def` + textures
- `Blueprints/` — in-game blueprints: RallyCar_Gravel / Sand / Ice, gates, pylons

Tuning values for every definition live in `Data/rally-tuning.json` and
`Data/stage-presets.json`; G-Screen entries in `Data/gscreen.json`.
