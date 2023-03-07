﻿using System.Collections;
using UnityEngine;

namespace LoveMachine.Core
{
    public sealed class RotatorController : ClassicButtplugController
    {
        private bool clockwise = true;

        protected override bool IsDeviceSupported(Device device) => device.IsRotator;

        protected override IEnumerator HandleAnimation(Device device, WaveInfo waveInfo)
        {
            float strokeTimeSecs = GetAnimationTimeSecs(device) / waveInfo.Frequency;
            for (int i = 0; i < waveInfo.Frequency - 1; i++)
            {
                HandleCoroutine(DoRotate(device, strokeTimeSecs));
                yield return new WaitForSecondsRealtime(strokeTimeSecs);
            }
            yield return HandleCoroutine(DoRotate(device, strokeTimeSecs));
            if (UnityEngine.Random.value <= RotatorConfig.RotationDirectionChangeChance.Value)
            {
                clockwise = !clockwise;
            }
        }

        protected override IEnumerator HandleOrgasm(Device device)
        {
            client.RotateCmd(device, 1f, clockwise);
            yield return new WaitForSecondsRealtime(game.MinOrgasmDurationSecs);
            yield return WaitWhile(() => game.IsOrgasming(device.Settings.GirlIndex));
            client.StopDeviceCmd(device);
        }

        private IEnumerator DoRotate(Device device, float strokeTimeSecs)
        {
            float downStrokeTimeSecs = strokeTimeSecs / 2f;
            float downSpeed = Mathf.Lerp(0.3f, 1f, 0.4f / strokeTimeSecs) *
                RotatorConfig.RotationSpeedRatio.Value;
            float upSpeed = downSpeed * 0.8f;
            client.RotateCmd(device, downSpeed, clockwise);
            yield return new WaitForSecondsRealtime(downStrokeTimeSecs);
            client.RotateCmd(device, upSpeed, !clockwise);
        }
    }
}