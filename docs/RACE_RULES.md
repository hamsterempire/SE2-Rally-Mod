# Rally stage rules & track building

## Stage timing flow

1. **Staging** — the driver sits at the Start Gate. The gate sensor detects the
   vehicle, staging lights go red for 3 seconds, then green.
2. **Start** — green light fires the start event; the Timing Console begins
   counting up.
3. **Checkpoints** — each Checkpoint Beacon the vehicle crosses registers a
   split on the Timing Console. A stage needs at least 2 checkpoints.
4. **Finish** — the finish sensor stops the timer. Stage time = timer value.
   Penalty: +5 s per checkpoint missed, +10 s per wheel lost.

## Building a stage

- Pick a route across a planet surface (Verdure grass/dirt and Kemik regolith
  both work well) — 500 m to 2 km is a good first stage.
- Mark the route with Rally Number Plates so drivers can read the stage.
- Place Checkpoint Beacons on the racing line, roughly every 200–400 m.
- Place the Start Gate on flat ground; give ~50 m of run-up before turn 1.
- Place the finish gate on a long straight so drivers don't overshoot.

## Classes

| Class | Constraint |
|---|---|
| Stock | vanilla blocks only, Rally Wheel preset from this mod allowed |
| Open | anything from this mod; no limits on wheels or power |
| Classic | no event-controller assists; timing by the gate console only |

## Multiplayer / hotseat

- Run one vehicle at a time against the clock; rally is a time-trial format,
  which keeps it singleplayer-friendly and multiplayer-optional.
- For hotseat, keep the stage short (< 1 min) and reset the timer between runs.
