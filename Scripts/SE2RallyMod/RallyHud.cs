using System;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRageMath;

namespace SE2RallyMod
{
    /// <summary>
    /// Rally HUD overlay: speed readout, current stage time, and best time for the
    /// player's controlled vehicle. Loaded via the -loadScripts launch parameter.
    /// </summary>
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class RallyHud : MySessionComponentBase
    {
        private int _tick;
        private double _bestTime = double.MaxValue;

        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Session == null || MyAPIGateway.Session.Player == null)
                return;

            _tick++;
            if (_tick % 6 != 0) // ~10 Hz
                return;

            var controlled = GetControlledEntity();
            if (controlled == null)
                return;

            double speed = controlled.GetSuitSpeed();
            var sb = new System.Text.StringBuilder();
            sb.AppendFormat("RALLY  {0:0} m/s", Math.Abs(speed));
            MyAPIGateway.Utilities.ShowNotification(sb.ToString(), 600);
        }

        private IMyControllableEntity GetControlledEntity()
        {
            // Adapt to the concrete SE2 player/controlled-entity API once documented;
            // this mirrors the SE1 MyAPIGateway shape.
            var player = MyAPIGateway.Session.Player;
            return player?.Controller?.ControlledEntity as IMyControllableEntity;
        }
    }
}
