# Model sources

Blender sources imported into the VRAGE3 Mod Editor via the Blender VRAGE
Tools add-on. Per block you need: NonFractured / Fractured / Deformed variants,
LODs (use the editor's Model Calculator for triangle budgets), collision
meshes, and mount point coverage > 50% of the 25 cm grid cell.

- `Blender/RallyWheel/` — rally wheel 50 cm rim + tyre (tread pattern adds grip fiction)
- `Blender/RallyCheckpoint/` — checkpoint pylon (flag + base)
- `Blender/RallyPlate/` — light armor plate with number decal area

Assign materials from the SE2 Decal & Material library or the mod's own
`RallyLivery` material so the editor auto-maps them by name.
