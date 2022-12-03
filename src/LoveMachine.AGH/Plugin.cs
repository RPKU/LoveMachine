﻿using BepInEx;
using LoveMachine.Core;

namespace LoveMachine.AGH
{
    [BepInPlugin(CoreConfig.GUID, CoreConfig.PluginName, CoreConfig.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            this.Initialize<HoukagoRinkanChuudokuGame>(Logger);
            Hooks.InstallHooks();
        }
    }
}