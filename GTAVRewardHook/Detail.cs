using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;

namespace GTAVRewardHook
{
    /// <summary>
    /// 游戏数据
    /// </summary>
    class Detail
    {
        /// <summary>
        /// 玩家
        /// </summary>
        public static Player player;
        /// <summary>
        /// 脚本上次执行的时间
        /// </summary>
        public static DateTime lastTime = DateTime.Now;
        /// <summary>
        /// 玩家的车辆
        /// </summary>
        public static Vehicle car;
        /// <summary>
        /// 距离上次逆行时间
        /// </summary>
        public static int time_since_player_drove_against_traffic;
        /// <summary>
        /// 距离上次行驶在人行道上的时间
        /// </summary>
        public static int time_since_player_drove_on_pavement;
        /// <summary>
        /// 距离上次撞人的时间
        /// </summary>
        public static int time_since_player_hit_ped;
        /// <summary>
        /// 距离上次撞车的时间
        /// </summary>
        public static int time_since_player_hit_vehicle;
        /// <summary>
        /// 附近的车辆
        /// </summary>
        public static List<Vehicle> near_by_vehicles = new List<Vehicle>();
        /// <summary>
        /// 附近的碰撞过的车辆
        /// </summary>
        public static List<Vehicle> near_by_touching_vehicles = new List<Vehicle>();
        /// <summary>
        /// 附近的行人
        /// </summary>
        public static List<Ped> near_by_peds = new List<Ped>();
        /// <summary>
        /// 附近的物体
        /// 不包括固定的物体，比如建筑，电线杆
        /// 包括可损毁的物体，比如铁丝网，座椅，路灯，垃圾箱等等
        /// </summary>
        public static List<Prop> near_by_props = new List<Prop>();
        /// <summary>
        /// 附近碰撞过的行人
        /// </summary>
        public static List<Ped> near_by_touching_peds = new List<Ped>();
        /// <summary>
        /// 附近碰撞过的物体
        /// </summary>
        public static List<Prop> near_by_touching_props = new List<Prop>();
        /// <summary>
        /// 街道的下一个位置
        /// </summary>
        public static Vector3 next_position_on_street;
        /// <summary>
        /// 半径，用于定义“附近的物体”等中的附近
        /// </summary>
        public static float radius = 20.0f;
        /// <summary>
        /// 玩家是否受伤
        /// </summary>
        public static bool is_ped_injured = false;
        /// <summary>
        /// 玩家是否在车里
        /// </summary>
        public static bool is_ped_in_any_vehicle = false;
        /// <summary>
        /// 玩家是否在水里
        /// </summary>
        public static bool is_player_in_water = false;
        /// <summary>
        /// 当前的天气类型
        /// </summary>
        public static string current_weather_type;
        /// <summary>
        /// 是否在游戏屏幕上展示当前位置
        /// </summary>
        public static bool showPostion;
        /// <summary>
        /// 车辆是否在道路上（不包括人行道和道路中间的绿化）
        /// </summary>
        public static bool onRoad = true;
        /// <summary>
        /// 起始点
        /// </summary>
        public static Vector3 StartPosition = new Vector3(-1419.786f, -424.3837f, 35.82669f);
        /// <summary>
        /// 终点
        /// </summary>
        public static Vector3 EndPosition = new Vector3(-1308.431f, -358.9514f, 36.30401f);
        //public static Vector3 EndPosition = new Vector3(-1201.835f, -296.28059f, 37.75566f);
        //public static Vector3 EndPosition = new Vector3(-1314.658f, -491024f, -32.84103f);

    }
}