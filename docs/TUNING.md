# Tuning reference

Every value the rally mod changes, mapped to its VRAGE3 editor field.
Vanilla values are approximations of the stock 5x5 wheel; the editor inspector
tooltip for each field is authoritative.

## Rally Wheel — wheel definition

| Field (inspector) | Vanilla | Rally | Why |
|---|---|---|---|
| `Friction` | 1.00 | 1.25 | more mechanical grip on loose voxel surfaces |
| `MaxSpeed` | 30 m/s | 45 m/s | rally stages are long; cap keeps-grid control sane |
| `Power` | 100% | 130% | extra torque for sandy/icy climbs |
| `Impulse` | 100% | 110% | better obstacle response |

## Rally Wheel — suspension definition

| Field (inspector) | Vanilla | Rally | Why |
|---|---|---|---|
| `MinHeight` | 0.10 m | 0.08 m | lower ride for stability |
| `MaxHeight` | 0.40 m | 0.55 m | long travel for ruts and rocks |
| `Strength` | 100% | 120% | carry speed over crests without bottoming out |
| `Stiffness` | 100% | 85% | softer springs absorb chatter |
| `Damping` | 100% | 115% | control rebound on jumps |

## Start Gate — sensor + event controller

| Field | Value |
|---|---|
| Sensor range | 4 m |
| Sensor field width | gate width (default 8 m) |
| Staging light delay | 3 s (Timer) |
| Light sequence | red 3 s → green |
| Timer output | start pulse to Timing Console |

## Checkpoint Beacon — sensor

| Field | Value |
|---|---|
| Sensor range | 6 m |
| Sensor field | omni (spherical) |
| Output | split event to Timing Console |

## Timing Console — timer

| Field | Value |
|---|---|
| Mode | count up |
| Precision | 0.1 s |
| Inputs | start (gate), split (checkpoint), stop (finish) |
| Output | stage + split times |

## Surface presets

See `Data/stage-presets.json` — gravel, sand, and ice change only friction,
stiffness, and damping so blueprints stay drop-in compatible.
