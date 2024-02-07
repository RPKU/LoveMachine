using System;
using System.Collections;
using LoveMachine.Core.Buttplug;
using LoveMachine.Core.Game;
using UnityEngine;

namespace LoveMachine.Core.Controller
{
    internal sealed class TcodeController : ClassicButtplugController
    {
        public override bool IsDeviceSupported(Device device) => device.IsTcode;

        protected override IEnumerator HandleAnimation(Device device, StrokeInfo strokeInfo) =>
            DoConstrict(device, strokeInfo);

        protected override IEnumerator HandleOrgasm(Device device)
        {
            var strokeInfo = new StrokeInfo
            {
                Pos = new Vector3(0, 0, 0),
                Rot = new Vector3(0, 0, 0)
            };
            return DoConstrict(device, strokeInfo);
        }

        protected override void HandleLevel(Device device, float level, float durationSecs)
        { }
        
        private IEnumerator DoConstrict(Device device, StrokeInfo strokeInfo)
        {
            float l0 = strokeInfo.Pos.y;
            float l1 = strokeInfo.Pos.x;
            float l2 = strokeInfo.Pos.z;
            float r0 = strokeInfo.Rot.y;
            float r1 = strokeInfo.Rot.x;
            float r2 = strokeInfo.Rot.z;
            
            Connecter.SendCommand(String.Format("L0{0:N2} L1{0:N2} L2{0:N2} R0{0:N2} R1{0:N2} R2{0:N2}", l0, l1, l2, r0, r1, r2));
            yield return new WaitForSecondsRealtime(1000f / device.Settings.UpdatesHz);
        }
    }
}