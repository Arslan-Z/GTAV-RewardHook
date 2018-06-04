using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;

namespace GTAVRewardHook
{
    class DetailPOJO
    {   /// <summary>
        /// 基本上和Detail的属性一一对应
        /// </summary>
        private static DetailPOJO singleton = null;
        private DetailPOJO()
        {

        }
        public static DetailPOJO Instance()
        {
            if (singleton == null)
            {
                singleton = new DetailPOJO();
            }
            return singleton;
        }
        public PedPOJO charactor;
        public VehiclePOJO car;
        public int time_since_player_drove_against_traffic;
        public int time_since_player_drove_on_pavement;
        public int time_since_player_hit_ped;
        public int time_since_player_hit_vehicle;
        public float distance_to_way_point;
        public bool use_tele_port;
        public List<VehiclePOJO> near_by_vehicles;
        public List<PedPOJO> near_by_peds;
        public List<EntityPOJO> near_by_props;
        public List<VehiclePOJO> near_by_touching_vehicles;
        public List<PedPOJO> near_by_touching_peds;
        public List<EntityPOJO> near_by_touching_props;
        public Vector3POJO next_position_on_street;
        public Vector3POJO forward_vector3;
        public float radius;
        public bool is_ped_injured;
        public bool is_ped_in_any_vehicle;
        public bool is_player_in_water;
        public bool onRoad;
        public Vector3POJO startPosition;
        public Vector3POJO endPosition;
    }
}