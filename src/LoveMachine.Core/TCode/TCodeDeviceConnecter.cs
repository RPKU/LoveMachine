using System.IO;
using System.IO.Ports;
using LoveMachine.Core.Config;
using LoveMachine.Core.PlatformSpecific;
using UnityEngine;

namespace LoveMachine.Core.TCode
{
    internal class TCodeDeviceConnecter : CoroutineHandler
    {
        private SerialPort serialPort;
        private float timeSinceLastAttempt = 0f;
        
        private void Start() => Open();
        
        private void Update()
        {
            if (IsConnected) return;
            timeSinceLastAttempt += Time.deltaTime; // 累加距离上次尝试的时间

            if (!(timeSinceLastAttempt >= TCodeDeviceConfig.ReconncetTime.Value)) return;
            TryConnect(); // 尝试重新连接
            timeSinceLastAttempt = 0f; // 重置计时器
        }

        private void TryConnect()
        {
            Logger.LogDebug(!Open()
                ? "Failed to connect to the device, will retry in 10 seconds."
                : "Connected to the device.");
        }

        private void OnDestroy()
        {
            Close();
        }

        // 打开串行端口以连接设备
        private bool Open()
        {
            try
            {
                serialPort = new SerialPort(TCodeDeviceConfig.PortName.Value, TCodeDeviceConfig.BaudRate.Value);
                serialPort.Open();
                return serialPort.IsOpen;
            }
            catch (IOException ex)
            {
                // 处理连接错误，例如打印错误日志
                Logger.LogError($"Error connecting to device: {ex.Message}");
                return false;
            }
        }

        // 关闭串行端口的连接
        public void Close()
        {
            if (IsConnected)
            {
                serialPort.Close();
            }
        }

        // 向设备发送指令
        public void SendCommand(string command)
        {
            if (IsConnected)
            {
                serialPort.WriteLine(command);
            }
            else
            {
                Logger.LogWarning("Serial port is not open. Please connect to the device first.");
            }
        }
        
        public void Connect()
        {
            Close(); // close previous connection just in case
            Open();
        }
        
        public bool IsConnected => serialPort != null && serialPort.IsOpen;
    }
}