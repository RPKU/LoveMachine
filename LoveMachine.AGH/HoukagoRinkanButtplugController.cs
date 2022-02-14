﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LoveMachine.Core;
using UnityEngine;

namespace LoveMachine.AGH
{
    public abstract class HoukagoRinkanButtplugController : ButtplugController
    {
        private static readonly Dictionary<Bone, string> sayaBones = new Dictionary<Bone, string>
        {
            { Bone.Vagina, "HS01_cli" },
            { Bone.LeftHand, "bip01 L Finger1Nub" },
            { Bone.Mouth, "BF01_tongue01" },
            { Bone.LeftBreast, "HS_Breast_LL" },
        };

        private static readonly Dictionary<Bone, string> elenaBones = new Dictionary<Bone, string>
        {
            { Bone.Vagina, "HS01_cli_02" },
            { Bone.LeftHand, "bip01 L Finger1Nub_02" },
            { Bone.Mouth, "BF01_tongue01_02" },
            { Bone.LeftBreast, "HS_Breast_LL_02" },
        };

        protected override int HeroineCount => 1;

        protected override bool IsHardSex => true;

        protected override int AnimationLayer => 0;

        protected override bool IsHSceneInterrupted => false;

        protected override Animator GetFemaleAnimator(int girlIndex)
            => (GameObject.Find("CH01/CH0001") ?? GameObject.Find("CH02/CH0002"))
                .GetComponent<Animator>();

        protected override Dictionary<Bone, Transform> GetFemaleBones(int _girlIndex)
            => (GameObject.Find("CH01") != null ? sayaBones : elenaBones)
                .ToDictionary(kvp => kvp.Key, kvp => GameObject.Find(kvp.Value).transform);

        protected override Transform GetMaleBone()
            => GameObject.Find("BP00_tamaL").transform;

        protected override string GetPose(int girlIndex)
            => GetFemaleAnimator(girlIndex).GetCurrentAnimatorClipInfo(0)[0].clip.name + "."
                + girlIndex;

        protected override bool IsIdle(int girlIndex) => GetFemaleAnimator(girlIndex) == null;

        protected override IEnumerator UntilReady()
        {
            yield return new WaitForSecondsRealtime(5f);
        }
    }

    public class HoukagoRinkanButtplugVibrationController : HoukagoRinkanButtplugController
    {
        protected override IEnumerator Run(int girlIndex, Bone bone)
            => RunVibratorLoop(girlIndex, bone);
    }

    public class HoukagoRinkanButtplugStrokerController : HoukagoRinkanButtplugController
    {
        protected override IEnumerator Run(int girlIndex, Bone bone)
            => RunStrokerLoop(girlIndex, bone);
    }
}
