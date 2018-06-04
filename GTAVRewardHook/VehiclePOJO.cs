using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVRewardHook
{
    class VehiclePOJO
    {
        /// <summary>
        /// 是否在屏幕上
        /// </summary>
        public bool IsOnScreen { get; set; }
        /// <summary>
        /// 当前位置
        /// </summary>
        public Vector3POJO Position { get; set; }
        /// <summary>
        /// 右方向向量
        /// </summary>
        public Vector3POJO RightVector { get; set; }
        /// <summary>
        /// 旋转向量
        /// </summary>
        public Vector3POJO Rotation { get; set; }
        /// <summary>
        /// 前方向向量
        /// </summary>
        public Vector3POJO ForwardVector { get; set; }
        /// <summary>
        /// 高出地面的距离
        /// </summary>
        public float HeightAboveGround { get; set; }
        /// <summary>
        /// 速度向量
        /// </summary>
        public Vector3POJO Velocity { get; set; }
        /// <summary>
        /// 前灯是否坏了
        /// </summary>
        public bool RightHeadLightBroken { get; set; }
        /// <summary>
        /// 尾灯是否坏了
        /// </summary>
        public bool LeftHeadLightBroken { get; set; }
        /// <summary>
        /// 灯是否亮着
        /// </summary>
        public bool LightsOn { get; set; }
        /// <summary>
        /// 引擎是否启动
        /// </summary>
        public bool EngineRunning { get; set; }
        /// <summary>
        /// 探照灯是否亮着
        /// </summary>
        public bool SearchLightOn { get; set; }
        /// <summary>
        /// ？
        /// </summary>
        public bool IsOnAllWheels { get; set; }
        /// <summary>
        /// 是否正停在红绿灯前
        /// </summary>
        public bool IsStoppedAtTrafficLights { get; set; }
        /// <summary>
        /// 是否停下了
        /// </summary>
        public bool IsStopped { get; set; }
        /// <summary>
        /// 是否可驾驶
        /// </summary>
        public bool IsDriveable { get; set; }
        /// <summary>
        /// ？
        /// </summary>
        public bool IsConvertible { get; set; }
        /// <summary>
        /// 前保险杠是否坏了
        /// </summary>
        public bool IsFrontBumperBrokenOff { get; set; }
        /// <summary>
        /// 后保险杠是否坏了
        /// </summary>
        public bool IsRearBumperBrokenOff { get; set; }
        /// <summary>
        /// 是否损坏
        /// </summary>
        public bool IsDamaged { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 车身健康状况
        /// </summary>
        public float BodyHealth { get; set; }
        /// <summary>
        /// 健康状况
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// 最大健康值
        /// </summary>
        public int MaxHealth { get; set; }
        /// <summary>
        /// 最大刹车
        /// </summary>
        public float MaxBraking { get; set; }
        /// <summary>
        /// 最大牵引
        /// </summary>
        public float MaxTraction { get; set; }
        /// <summary>
        /// 引擎健康值
        /// </summary>
        public float EngineHealth { get; set; }
        /// <summary>
        /// 转角
        /// </summary>
        public float SteeringScale { get; set; }
        /// <summary>
        /// 汽车转角角度
        /// </summary>
        public float SteeringAngle { get; set; }

        /// <summary>
        /// 车轮转速
        /// </summary>
        public float WheelSpeed { get; set; }
        /// <summary>
        /// 加速度 -1 0 1
        /// </summary>
        public float Acceleration { get; set; }
        /// <summary>
        /// 油箱油量 应该一直是满的
        /// </summary>
        public float FuelLevel { get; set; }
        /// <summary>
        /// 当前每分钟转速
        /// </summary>
        public float CurrentRPM { get; set; }
        /// <summary>
        /// ？
        /// </summary>
        public int CurrentGear { get; set; }
        /// <summary>
        /// ?
        /// </summary>
        public int HighGear { get; set; }

    }
}
