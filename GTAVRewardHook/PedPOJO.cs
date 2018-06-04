using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GTAVRewardHook
{
    class PedPOJO
    {
        /// <summary>
        /// 是否是人类
        /// </summary>
        public bool IsHuman { get; set; }
        /// <summary>
        /// 是否是站立的(不在车里)
        /// </summary>
        /// <returns></returns>
        public bool IsOnFoot { get; set; }
        /// <summary>
        /// 是否是玩本人
        /// </summary>
        public bool IsPlayer { get; set; }
        /// <summary>
        /// 是否出现在屏幕上
        /// </summary>
        public bool IsOnScreen { get; set; }
        /// <summary>
        /// 当前位置坐标
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
        /// 速度向量
        /// </summary>
        public Vector3POJO Velocity { get; set; }
    }
}
