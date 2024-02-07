using System;
using BepInEx;
using BepInEx.Configuration;

namespace LoveMachine.Core.Config
{
    internal static class TCodeDeviceConfig
    {
        public static ConfigEntry<string> PortName { get; private set; }
        public static ConfigEntry<int> BaudRate { get; private set; }
        public static ConfigEntry<int> ReconncetTime { get; private set; }
        public static ConfigEntry<bool> SmoothMotion { get; private set; }
        public static ConfigEntry<float> LengthRealism { get; private set; }
        
        internal static void Initialize(BaseUnityPlugin plugin)
        {
            int order = 1000;
            const string tCodeSettingsTitle = "T-Code Device Settings";
            PortName = plugin.Config.Bind(
                section: tCodeSettingsTitle,
                key: "Serial Port Name",
                defaultValue: "COM3",
                new ConfigDescription(
                    "Name of serial port.",
                    tags: new ConfigurationManagerAttributes { Order = --order }));
            BaudRate = plugin.Config.Bind(
                section: tCodeSettingsTitle,
                key: "Baud Rate",
                defaultValue: 115200,
                new ConfigDescription(
                    "Baud rate of t-code device",
                    tags: new ConfigurationManagerAttributes { Order = --order }));
            ReconncetTime = plugin.Config.Bind(
                section: tCodeSettingsTitle,
                key: "Reconncet Time",
                defaultValue: 10,
                new ConfigDescription(
                    "Reconncet time of t-code device",
                    tags: new ConfigurationManagerAttributes { Order = --order }));
            SmoothMotion = plugin.Config.Bind(
                section: tCodeSettingsTitle,
                key: "Smooth Motion",
                defaultValue: true,
                new ConfigDescription(
                    "Smooth the motion of t-code device",
                    tags: new ConfigurationManagerAttributes { Order = --order }));
            LengthRealism = plugin.Config.Bind(
                section: tCodeSettingsTitle,
                key: "Length Realism",
                defaultValue: 0.5f,
                new ConfigDescription(
                    "Length realism of t-code device",
                    tags: new ConfigurationManagerAttributes { Order = --order }));
        }
    }
}