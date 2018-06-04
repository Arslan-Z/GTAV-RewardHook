using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAVRewardHook
{
    class Convert
    {
        /// <summary>
        /// 把 GTA.Math.Vector 对象 转换成 Vector3POJO 对象
        /// </summary>
        /// <param name="vec">GTA.Math.Vector 对象 </param>
        /// <returns>Vector3POJO 对象</returns>
        public static Vector3POJO ConvertVector3(Vector3 vec)
        {
            Vector3POJO pojo = new Vector3POJO();
            pojo.X = vec.X;
            pojo.Y = vec.Y;
            pojo.Z = vec.Z;
            return pojo;
        }

        /// <summary>
        /// 把 GTA.Entity 对象 转换成 EntityPOJO 对象
        /// </summary>
        /// <param name="entity">GTA.Entity 对象</param>
        /// <returns>EntityPOJO 对象</returns>
        public static EntityPOJO ConvertEntity(Entity entity)
        {
            EntityPOJO pojo = new EntityPOJO();
            pojo.IsOnScreen = entity.IsOnScreen;
            pojo.Model = entity.Model.Hash.ToString();
            pojo.Position = ConvertVector3(entity.Position);
            pojo.RightVector = ConvertVector3(entity.RightVector);
            pojo.Rotation = ConvertVector3(entity.Rotation);
            pojo.ForwardVector = ConvertVector3(entity.ForwardVector);
            pojo.HeightAboveGround = entity.HeightAboveGround;
            pojo.Velocity = ConvertVector3(entity.Velocity);
            return pojo;
        }

        /// <summary>
        /// 把 GTA.Vehicle 对象 转换成 VehiclePOJO 对象
        /// </summary>
        /// <param name="car">GTA.Vehicle 对象</param>
        /// <returns>VehiclePOJO 对象</returns>
        public static VehiclePOJO ConvertVehicle(Vehicle car)
        {
            VehiclePOJO pojo = new VehiclePOJO();
            pojo.RightHeadLightBroken = car.RightHeadLightBroken;
            pojo.LeftHeadLightBroken = car.LeftHeadLightBroken;
            pojo.LightsOn = car.LightsOn;
            pojo.EngineRunning = car.EngineRunning;
            pojo.SearchLightOn = car.SearchLightOn;
            pojo.IsOnAllWheels = car.IsOnAllWheels;
            pojo.IsStoppedAtTrafficLights = car.IsStoppedAtTrafficLights;
            pojo.IsStopped = car.IsStopped;
            pojo.IsDriveable = car.IsDriveable;
            pojo.IsConvertible = car.IsConvertible;
            pojo.IsFrontBumperBrokenOff = car.IsFrontBumperBrokenOff;
            pojo.IsRearBumperBrokenOff = car.IsRearBumperBrokenOff;
            pojo.IsDamaged = car.IsDamaged;
            pojo.Speed = car.Speed;
            pojo.BodyHealth = car.BodyHealth;
            pojo.MaxBraking = car.MaxBraking;
            pojo.MaxTraction = car.MaxTraction;
            pojo.EngineHealth = car.EngineHealth;
            pojo.SteeringScale = car.SteeringScale;
            pojo.Health = car.Health;
            pojo.MaxHealth = car.MaxHealth;
            pojo.SteeringAngle = car.SteeringAngle;
            pojo.WheelSpeed = car.WheelSpeed;
            pojo.Acceleration = car.Acceleration;
            pojo.FuelLevel = car.FuelLevel;
            pojo.CurrentRPM = car.CurrentRPM;
            pojo.CurrentGear = car.CurrentGear;
            pojo.HighGear = car.HighGear;
            pojo.Position = ConvertVector3(car.Position);
            pojo.RightVector = ConvertVector3(car.RightVector);
            pojo.Rotation = ConvertVector3(car.Rotation);
            pojo.ForwardVector = ConvertVector3(car.ForwardVector);
            pojo.HeightAboveGround = car.HeightAboveGround;
            pojo.Velocity = ConvertVector3(car.Velocity);
            return pojo;
        }

        /// <summary>
        /// 把 GTA.Ped 对象 转换成 PedPOJO 对象
        /// </summary>
        /// <param name="ped">GTA.Ped 对象</param>
        /// <returns>PedPOJO 对象</returns>
        public static PedPOJO ConvertPed(Ped ped)
        {
            PedPOJO pojo = new PedPOJO();
            pojo.IsHuman = ped.IsHuman;
            pojo.IsPlayer = ped.IsPlayer;
            pojo.IsOnScreen = ped.IsOnScreen;
            pojo.Position = ConvertVector3(ped.Position);
            pojo.RightVector = ConvertVector3(ped.RightVector);
            pojo.Rotation = ConvertVector3(ped.Rotation);
            pojo.ForwardVector = ConvertVector3(ped.ForwardVector);
            pojo.Velocity = ConvertVector3(ped.Velocity);
            return pojo;
        }

        /// <summary>
        /// DetailPOJO 对象序列化 为 json字符串
        /// ser.RecursionLimit = 6 设置递归深度为 6
        /// </summary>
        /// <returns></returns>
        public static string ToJson()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.RecursionLimit = 6;
            DetailPOJO pojo = DetailPOJO.Instance();
            string json = ser.Serialize(pojo);
            return json;
        }

        /// <summary>
        /// Detail 对象转换为 DetailPOJO 对象
        /// </summary>
        /// <returns></returns>
        public static DetailPOJO ConvertDetail()
        {
            DetailPOJO pojo = DetailPOJO.Instance();
            pojo.car = ConvertVehicle(Detail.car);
            pojo.charactor = ConvertPed(Detail.player.Character);
            pojo.time_since_player_drove_against_traffic = Detail.time_since_player_drove_against_traffic;
            pojo.time_since_player_drove_on_pavement = Detail.time_since_player_drove_on_pavement;
            pojo.time_since_player_hit_ped = Detail.time_since_player_hit_ped;
            pojo.time_since_player_hit_vehicle = Detail.time_since_player_hit_vehicle;
            pojo.startPosition = ConvertVector3(Detail.StartPosition);
            pojo.endPosition = ConvertVector3(Detail.EndPosition);
            pojo.near_by_vehicles = new List<VehiclePOJO>();
            for (int i = 0; i < Detail.near_by_vehicles.Count; i++)
            {
                pojo.near_by_vehicles.Add(ConvertVehicle(Detail.near_by_vehicles[i]));
            }
            pojo.near_by_peds = new List<PedPOJO>();
            for (int i = 0; i < Detail.near_by_peds.Count; i++)
            {
                pojo.near_by_peds.Add(ConvertPed(Detail.near_by_peds[i]));
            }
            pojo.near_by_props = new List<EntityPOJO>();
            for (int i = 0; i < Detail.near_by_props.Count; i++)
            {
                pojo.near_by_props.Add(ConvertEntity(Detail.near_by_props[i]));
            }
            pojo.near_by_touching_peds = new List<PedPOJO>();
            for (int i = 0; i < Detail.near_by_touching_peds.Count; i++)
            {
                pojo.near_by_touching_peds.Add(ConvertPed(Detail.near_by_touching_peds[i]));
            }
            pojo.near_by_touching_props = new List<EntityPOJO>();
            for (int i = 0; i < Detail.near_by_touching_props.Count; i++)
            {
                pojo.near_by_touching_props.Add(ConvertEntity(Detail.near_by_touching_props[i]));
            }
            pojo.near_by_touching_vehicles = new List<VehiclePOJO>();
            for (int i = 0; i < Detail.near_by_touching_vehicles.Count; i++)
            {
                pojo.near_by_touching_vehicles.Add(ConvertVehicle(Detail.near_by_touching_vehicles[i]));
            }
            pojo.next_position_on_street = ConvertVector3(Detail.next_position_on_street);
            pojo.forward_vector3 = ConvertVector3(Detail.car.ForwardVector);
            pojo.radius = Detail.radius;
            pojo.is_ped_injured = Detail.is_ped_injured;
            pojo.is_ped_in_any_vehicle = Detail.is_ped_in_any_vehicle;
            pojo.onRoad = Detail.onRoad;
            pojo.is_player_in_water = Detail.is_player_in_water;
            return pojo;
        }
    }
}