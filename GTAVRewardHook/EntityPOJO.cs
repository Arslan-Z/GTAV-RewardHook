using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA.Math;
using GTA;
namespace GTAVRewardHook
{
    class EntityPOJO
    {
        public bool IsOnScreen { get; set; }
        public Vector3POJO Position { get; set; }
        public Vector3POJO RightVector { get; set; }
        public Vector3POJO Rotation { get; set; }
        public Vector3POJO ForwardVector { get; set; }
        public float HeightAboveGround { get; set; }
        public string Model { get; set; }
        public Vector3POJO Velocity { get; set; }
    }
}
