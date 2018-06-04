using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Web.Script.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAVRewardHook
{
    class Utils
    {

        public static void GetNextPositionOnStreet()
        {
            Detail.next_position_on_street = World.GetNextPositionOnStreet(Detail.player.Character.Position);
        }
        /// <summary>
        /// 驾驶事件
        /// time_since_player_drove_against_traffic 上次逆行距今时间
        /// time_since_player_drove_on_pavement 上次在人行道上行驶距今时间
        /// time_since_player_hit_ped  上次撞人距今时间
        /// time_since_player_hit_vehicle 上次撞车距今时间
        /// </summary>
        public static void DrivingEvent()
        {
            Detail.time_since_player_drove_against_traffic = Function.Call<int>(Hash.GET_TIME_SINCE_PLAYER_DROVE_AGAINST_TRAFFIC);
            Detail.time_since_player_drove_on_pavement = Function.Call<int>(Hash.GET_TIME_SINCE_PLAYER_DROVE_ON_PAVEMENT);
            Detail.time_since_player_hit_ped = Function.Call<int>(Hash.GET_TIME_SINCE_PLAYER_HIT_PED);
            Detail.time_since_player_hit_vehicle = Function.Call<int>(Hash.GET_TIME_SINCE_PLAYER_HIT_VEHICLE);
        }
        /// <summary>
        /// 获取当前天气
        /// </summary>
        public static void GetWeather()
        {
            Detail.current_weather_type = Function.Call<String>(Hash._GET_CURRENT_WEATHER_TYPE);
        }
        /// <summary>
        /// 传送到指定位置
        /// </summary>
        /// <param name="positon">传送目标位置</param>
        /// <param name="withVehicle">连着车一起传送</param>
        public static void TelePort(Vector3 positon, bool withVehicle)
        {
            if (Detail.car.Exists() && withVehicle)
            {   //传送车辆
                Detail.car.Position = positon + Detail.player.Character.ForwardVector * 10;
                // 传送玩家
                Detail.player.Character.Position = positon;
                // 等待传送生效。应该是可有可无的
                Thread.Sleep(1 * 1000);
                // 把玩家放进车里
                Function.Call(Hash.SET_PED_INTO_VEHICLE, Detail.player.Character, Detail.car, -1);
            }
            else
            {   //只传送玩家
                Detail.player.Character.Position = positon;
                Thread.Sleep(1 * 1000);
            }
        }
        /// <summary>
        /// 获取附近车辆/行人/物体；
        /// 获取碰撞的车辆/行人/物体；
        /// </summary>
        public static void NearBys()
        {
            Detail.near_by_vehicles = new List<Vehicle>(World.GetNearbyVehicles(Detail.player.Character, Detail.radius));
            Detail.near_by_touching_vehicles.Clear();

            for (int i = 0; i < Detail.near_by_vehicles.Count; i++)
            {
                Vehicle vehicle = Detail.near_by_vehicles[i];
                bool touching = vehicle.IsTouching(Detail.car);
                if (touching)
                {
                    Detail.near_by_touching_vehicles.Add(vehicle);
                }
            }

            Detail.near_by_peds = new List<Ped>(World.GetNearbyPeds(Detail.player.Character, Detail.radius));

            Detail.near_by_touching_peds.Clear();
            for (int i = 0; i < Detail.near_by_peds.Count; i++)
            {
                Ped ped = Detail.near_by_peds[i];
                bool touching = ped.IsTouching(Detail.car);
                if (touching)
                {
                    Detail.near_by_touching_peds.Add(ped);
                }
            }

            Detail.near_by_props = new List<Prop>(World.GetNearbyProps(Detail.player.Character.Position, Detail.radius));
            Detail.near_by_touching_props.Clear();

            for (int i = 0; i < Detail.near_by_props.Count; i++)
            {
                Prop prop = Detail.near_by_props[i];
                bool touching = prop.IsTouching(Detail.car);
                if (touching)
                {
                    Detail.near_by_touching_props.Add(prop);
                }
            }
        }
        /// <summary>
        /// 玩家是否受伤
        /// </summary>
        public static void IsPedInjured()
        {
            Detail.is_ped_injured = Function.Call<bool>(Hash.IS_PED_INJURED, Detail.player.Character);
        }
        /// <summary>
        /// 玩家是否在车里
        /// </summary>
        public static void IsPedInAnyVehicle()
        {
            Detail.is_ped_in_any_vehicle = Function.Call<bool>(Hash.IS_PED_IN_ANY_VEHICLE, Detail.player.Character);
        }
        /// <summary>
        /// 玩家是否在水里
        /// </summary>
        public static void IsPlayerInWater()
        {
            Detail.is_player_in_water = Function.Call<bool>(Hash.IS_ENTITY_IN_WATER, Detail.player.Character);
        }

        // public static List<int> TRAFFIC_SIGNAL_HASHES = new List<int>
        // {
        //     0x3E2B73A4, // prop_traffic_01a
        //     0x336E5E2A, // prop_traffic_01b
        //     -0x271456DE, // prop_traffic_01d
        //     -0x2B8D60B0, // prop_traffic_02a
        //     0x272244B2, // prop_traffic_02b
        //     0x33986EAE, // prop_traffic_03a
        //     -0x5B8AE9EF // prop_traffic_lightset_01
        // };
    }
}