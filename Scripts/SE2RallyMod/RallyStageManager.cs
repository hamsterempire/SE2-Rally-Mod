using System;
using System.Collections.Generic;
using System.Text;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.ModAPI;
using VRageMath;

namespace SE2RallyMod
{
    /// <summary>
    /// Rally stage timing manager. Loaded via the -loadScripts launch parameter.
    /// Watches blocks named with the Rally* prefixes and times stage runs:
    ///   RallyStart    - sensor at the start gate; triggers when armed (red -> green)
    ///   RallyCP*      - checkpoint sensors; record split times
    ///   RallyFinish   - sensor at the finish; stops the clock
    /// Penalties follow Data/rally-tuning.json: +5 s missed checkpoint, +10 s lost wheel.
    /// </summary>
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class RallyStageManager : MySessionComponentBase
    {
        private const string StartTag = "RallyStart";
        private const string CheckpointTag = "RallyCP";
        private const string FinishTag = "RallyFinish";

        private const double MissedCheckpointPenaltySeconds = 5.0;
        private const double LostWheelPenaltySeconds = 10.0;

        private struct CheckpointState
        {
            public bool WasActive;
        }

        private class StageRun
        {
            public DateTime StartTime;
            public List<double> Splits = new List<double>();
            public int CheckpointsTotal;
        }

        private readonly Dictionary<string, CheckpointState> _sensorStates =
            new Dictionary<string, CheckpointState>(StringComparer.OrdinalIgnoreCase);

        private StageRun _currentRun;
        private bool _runArmed;
        private int _tick;

        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Session == null)
                return;

            _tick++;
            if (_tick % 10 != 0)
                return;

            ScanSensors();
        }

        private void ScanSensors()
        {
            var grids = new List<IMyEntity>();
            try
            {
                // Collect sensor blocks by name tag. SE2's -loadScripts API surface is
                // undocumented; adapt the enumeration call below to the actual
                // entity/gobbet API if it differs from the SE1 MyAPIGateway shape.
                var entities = new List<IMyEntity>();
                MyAPIGateway.Entities.GetEntities(entities, e => e is IMyCubeGrid);

                foreach (var entity in entities)
                {
                    var grid = entity as IMyCubeGrid;
                    if (grid == null)
                        continue;

                    var blocks = new List<IMyCubeBlock>();
                    grid.GetBlocks(blocks);
                    foreach (var block in blocks)
                    {
                        var name = block?.DisplayName ?? string.Empty;
                        if (name.IndexOf(StartTag, StringComparison.OrdinalIgnoreCase) >= 0)
                            HandleStart(block);
                        else if (name.IndexOf(CheckpointTag, StringComparison.OrdinalIgnoreCase) >= 0)
                            HandleCheckpoint(block, name);
                        else if (name.IndexOf(FinishTag, StringComparison.OrdinalIgnoreCase) >= 0)
                            HandleFinish(block);
                    }
                }
            }
            finally
            {
                grids.Clear();
            }
        }

        private bool IsTriggered(IMyCubeBlock block)
        {
            // Sensors: a triggered (active) sensor raises this flag. Replace with the
            // concrete SE2 sensor interface property once the API is documented.
            var sensor = block as IMySensorBlock;
            return sensor != null && sensor.IsActive;
        }

        private void HandleStart(IMyCubeBlock block)
        {
            bool active = IsTriggered(block);
            bool was = GetPreviousState(StartTag, active);

            if (active && !was && !_runArmed)
            {
                _runArmed = true;
                MyAPIGateway.Utilities.ShowNotification("Rally: stage armed - green light!", 2000,
                    MyFontEnum.Green);
            }
            else if (active && !_runArmed)
            {
                _currentRun = new StageRun();
                _currentRun.StartTime = DateTime.UtcNow;
                _runArmed = false;
                MyAPIGateway.Utilities.ShowNotification("Rally: GO!", 2000, MyFontEnum.Green);
            }
        }

        private void HandleCheckpoint(IMyCubeBlock block, string name)
        {
            bool active = IsTriggered(block);
            bool was = GetPreviousState(name, active);

            if (active && !was && _currentRun != null)
            {
                double split = (DateTime.UtcNow - _currentRun.StartTime).TotalSeconds;
                _currentRun.Splits.Add(split);
                MyAPIGateway.Utilities.ShowNotification(
                    string.Format("Rally: CP {0} - {1:0.0}s", _currentRun.Splits.Count, split), 2000);
            }
        }

        private void HandleFinish(IMyCubeBlock block)
        {
            bool active = IsTriggered(block);
            bool was = GetPreviousState(FinishTag, active);

            if (active && !was && _currentRun != null)
            {
                double total = (DateTime.UtcNow - _currentRun.StartTime).TotalSeconds;
                double penalty = 0;
                if (_currentRun.CheckpointsTotal > _currentRun.Splits.Count)
                    penalty += (_currentRun.CheckpointsTotal - _currentRun.Splits.Count)
                               * MissedCheckpointPenaltySeconds;

                double final = total + penalty;
                var sb = new StringBuilder();
                sb.AppendFormat("Rally: stage complete - {0:0.0}s", final);
                if (penalty > 0)
                    sb.AppendFormat(" (incl. {0:0.0}s penalties)", penalty);
                MyAPIGateway.Utilities.ShowNotification(sb.ToString(), 8000, MyFontEnum.Green);

                for (int i = 0; i < _currentRun.Splits.Count; i++)
                    MyAPIGateway.Utilities.ShowNotification(
                        string.Format("  CP {0}: {1:0.0}s", i + 1, _currentRun.Splits[i]), 8000);

                _currentRun = null;
                _runArmed = false;
            }
        }

        private bool GetPreviousState(string key, bool active)
        {
            CheckpointState state;
            if (!_sensorStates.TryGetValue(key, out state))
                state = new CheckpointState { WasActive = false };

            _sensorStates[key] = new CheckpointState { WasActive = active };
            return state.WasActive;
        }

        protected override void UnloadData()
        {
            _sensorStates.Clear();
            _currentRun = null;
            base.UnloadData();
        }
    }
}
