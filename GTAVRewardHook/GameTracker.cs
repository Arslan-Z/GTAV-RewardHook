using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAVRewardHook
{
    /// <summary>
    /// 脚本
    /// </summary>
    public class GameTrackerScript : Script
    {
        private Thread serverThread;
        public GameTrackerScript()
        {
            serverThread = new Thread(new ThreadStart(Server.StartHost));
            serverThread.Start(); // 启动nancy服务器
            Interval = 1; // Tick 执行间隔，毫秒
            Tick += MainTick; // 每隔 Interval 毫秒执行一次 MainTick，ManTick理解为回调函数
            KeyUp += OnKeyUp; // 绑定KeyUp事件， OnKeyUp也是回调函数
            KeyDown += OnKeyDown;
        }
        void OnKeyDown(object sender, KeyEventArgs e)
        {

        }
        void OnKeyUp(Object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.K)
            {
                // ctrl + k, 在Player面前创建一个汽车，同时将玩家放入汽车
                // 似乎有 bug
                CreateVehicleBeforePlayer();
            }
            else if (e.Control && e.KeyCode == Keys.R)
            { //修复汽车
                Detail.car.Repair();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            { // 设置时间为中午
                SetTimeNoon();
            }
            else if (e.Control && e.KeyCode == Keys.T)
            { // 随机传送到附近的位置
                RandomTelePort(15, true);
            }
            else if (e.Control && e.KeyCode == Keys.B)
            { // 在游戏界面上展示当前位置坐标
                Detail.showPostion = !Detail.showPostion;
            }
            else if (e.Control && e.KeyCode == Keys.C)
            { // 传送到终点
                GoToEndPostion();
            }
            else if (e.Control && e.KeyCode == Keys.J)
            { // 取消垂直视角
                DeactiveCam();
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            { //开启垂直视角
                ActiveCam();
            }
            else if (e.KeyCode == Keys.H)
            { // 回到起点
                BackToStartPosition();
                Random ran = new Random();
                double x = ran.NextDouble();
                if (x < 0.4)
                { //随机摆放汽车的方向
                    ForwardBackSide();
                }
            }

        }

        /// <summary>
        /// 设置游戏时间为中午
        /// </summary>
        public void SetTimeNoon()
        { // 设置时间为 10：00：00
            Function.Call(Hash.SET_CLOCK_TIME, 10, 0, 0);
        }

        /// <summary>
        /// 随机传送
        /// </summary>
        /// <param name="radius">范围</param>
        /// <param name="randomVehicleBackSize">随机摆放汽车的方向</param>
        public void RandomTelePort(int radius, bool randomVehicleBackSize)
        {
            Random ran = new Random();
            double x = ran.NextDouble();
            double y = ran.NextDouble();
            double z = ran.NextDouble();
            Vector3 random_vec = new Vector3((float)x, (float)y, (float)z).Normalized; //随机方向
            Utils.TelePort(Detail.next_position_on_street + random_vec * radius, true);
            if (randomVehicleBackSize)
            {
                int ran_choise = ran.Next(1, 5);
                if (ran_choise == 1)
                { // 20% 的概率车头调转
                    ForwardBackSide();
                }
            }
        }

        /// <summary>
        /// 在玩家面前创造一辆汽车，同时玩家进入
        /// </summary>
        void CreateVehicleBeforePlayer()
        {
            Vector3 position = Detail.player.Character.Position;
            Vector3 forward = Detail.player.Character.ForwardVector;
            // 在前方 3 个单位出创造汽车
            Vehicle vehicle = World.CreateVehicle(VehicleHash.Comet4, position + forward * 3);
            // 玩家进入汽车
            Detail.player.Character.SetIntoVehicle(vehicle, VehicleSeat.Driver);
        }

        /// <summary>
        /// 回到起点
        /// </summary>
        void BackToStartPosition()
        {
            Utils.TelePort(Detail.StartPosition, true);
        }

        /// <summary>
        /// 去往终点
        /// </summary>
        void GoToEndPostion()
        {
            Utils.TelePort(Detail.EndPosition, true);
        }

        /// <summary>
        /// 车头调转
        /// </summary>
        void ForwardBackSide()
        {
            Detail.car.Rotation = new Vector3(Detail.car.Rotation.X, Detail.car.Rotation.Y, -Detail.car.Rotation.Z);
        }

        /// <summary>
        /// 汽车增强
        /// </summary>
        public void StrongCar()
        {
            // 所有人忽略玩家，这样不会因为蹭了车被拖出来打
            Function.Call(Hash.SET_EVERYONE_IGNORE_PLAYER, Detail.player.Handle, true);
            // 警察忽略玩家，不会被逮捕
            Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Detail.player, true);
            // 车辆不会损毁，即Health属性不会降低
            //Function.Call(Hash.SET_VEHICLE_CAN_BREAK, Detail.car.Handle, false);
            // 轮胎不会爆胎
            //Function.Call(Hash.SET_VEHICLE_TYRES_CAN_BURST, Detail.car.Handle, false);
            // 轮胎不会损坏
            Function.Call(Hash.SET_VEHICLE_WHEELS_CAN_BREAK, Detail.car.Handle, false);
            // 汽车车身牢固？
            //Function.Call(Hash.SET_VEHICLE_HAS_STRONG_AXLES, Detail.car.Handle, true);
            // 汽车牢不可破 ？
            //Function.Call(Hash.SET_ENTITY_INVINCIBLE, Detail.car.Handle, true);
            //BOOL bulletProof, BOOL fireProof, BOOL explosionProof, BOOL collisionProof, BOOL meleeProof, BOOL p6, BOOL p7, BOOL drownProof
            //Function.Call(Hash.SET_ENTITY_PROOFS, true, true, true, false, true, false, false, true);
        }

        /// <summary>
        /// 画线条
        /// </summary>
        /// <param name="point">线条起始点</param>
        /// <param name="direction">线条方向</param>
        /// <param name="length">线条长度</param>
        /// <param name="color">线条颜色</param>
        public void DrawLine(Vector3 point, Vector3 direction, float length, Color color)
        {
            Vector3 to = point + direction.Normalized * length;
            Function.Call(Hash.DRAW_LINE, point.X, point.Y, point.Z, to.X, to.Y, to.Z, color.R, color.G, color.B, 255);
        }

        /// <summary>
        /// 画上方向线条
        /// </summary>
        public void DrawUp()
        {
            DrawLine(Detail.car.Position, Detail.car.UpVector, 5, Color.Orange);
        }
        /// <summary>
        /// 画旋转向量线条
        /// </summary>
        public void DrawRotation()
        {
            DrawLine(Detail.car.Position, Detail.car.Rotation, 5, Color.Red);
        }
        /// <summary>
        /// 画前方向线条
        /// </summary>
        public void DrawForward()
        {
            DrawLine(Detail.car.Position, Detail.car.ForwardVector, 5, Color.Green);
        }
        /// <summary>
        /// 画右方向线条
        /// </summary>
        public void DrawRight()
        {
            DrawLine(Detail.car.Position, Detail.car.RightVector, 5, Color.Blue);
        }
        /// <summary>
        /// 锁住车
        /// </summary>
        /// <param name="status">车辆状态</param>
        /// 0 - CARLOCK_NONE
        /// 1 - CARLOCK_UNLOCKED
        /// 2 - CARLOCK_LOCKED(locked)
        /// 3 - CARLOCK_LOCKOUT_PLAYER_ONLY
        /// 4 - CARLOCK_LOCKED_PLAYER_INSIDE(can get in, can't leave)
        public void VehicleLock(int status)
        {

            if (Function.Call<int>(Hash.GET_VEHICLE_DOOR_LOCK_STATUS, Detail.car) != status)
            {
                Function.Call(Hash.SET_VEHICLE_DOORS_LOCKED, Detail.car, status);
            }

        }
        /// <summary>
        /// 回复车辆健康状态
        /// </summary>
        public void RestoreVehicleHealth()
        {
            if (Detail.car.Health < Detail.car.MaxHealth * 5 / 6)
            {
                Detail.car.Repair();
                Detail.car.Health = Detail.car.MaxHealth;
            }
        }
        /// <summary>
        /// 回复玩家健康状态
        /// 一般用不到，玩家在车里不会撞死
        /// </summary>
        public void RestorePlayerHealth()
        {
            if (Detail.player.Character.Health < Detail.player.Character.MaxHealth / 2)
            {
                Detail.player.Character.Health = Detail.player.Character.MaxHealth;
            }
        }
        /// <summary>
        /// 重置视角(回复原始视角)
        /// </summary>
        public void DeactiveCam()
        {
            World.RenderingCamera = null;
        }

        /// <summary>
        /// 所有触碰的物体/行人/车辆都画上线条
        /// </summary>
        public void DrawTouching()
        {
            for (int i = 0; i < Detail.near_by_touching_props.Count; i++)
            {
                Prop prop = Detail.near_by_touching_props[i];
                Vector3 from = prop.Position - prop.ForwardVector * 3;
                Vector3 to = prop.Position + prop.ForwardVector * 3;
                Function.Call(Hash.DRAW_LINE, from.X, from.Y, from.Z, to.X, to.Y, to.Z, 255, 0, 0, 255);
            }
            for (int i = 0; i < Detail.near_by_touching_peds.Count; i++)
            {
                Ped ped = Detail.near_by_touching_peds[i];
                Vector3 from = ped.Position - ped.ForwardVector * 3;
                Vector3 to = ped.Position + ped.ForwardVector * 3;
                Function.Call(Hash.DRAW_LINE, from.X, from.Y, from.Z, to.X, to.Y, to.Z, 255, 0, 0, 255);
            }
            for (int i = 0; i < Detail.near_by_touching_vehicles.Count; i++)
            {
                Vehicle vehicle = Detail.near_by_touching_vehicles[i];
                Vector3 from = vehicle.Position - vehicle.ForwardVector * 3;
                Vector3 to = vehicle.Position + vehicle.ForwardVector * 3;
                Function.Call(Hash.DRAW_LINE, from.X, from.Y, from.Z, to.X, to.Y, to.Z, 255, 0, 0, 255);
            }
        }
        /// <summary>
        /// 如果被通缉就清除
        /// </summary>
        public void CheckAndClearWantedLevel()
        {
            if (Detail.player.WantedLevel > 0)
            {
                Detail.player.WantedLevel = 0;
            }
        }
        /// <summary>
        /// 如果落水就传送到附近一个位置
        /// </summary>
        public void TeleportIfInWater()
        {
            if (Detail.is_player_in_water)
            {
                UI.Notify("your are in water");
                RandomTelePort(15, true);
            }
        }

        /// <summary>
        /// 激活俯瞰视角
        /// </summary>
        public void ActiveCam()
        {
            World.DestroyAllCameras(); // 清除其他视角
            // 构建一个视角
            //new Vector3 (280f, 0f, -45f) 表示视角悬着的角度，280f
            Camera leftCam = World.CreateCamera(Detail.car.Position + new Vector3(0, 0, 20f), new Vector3(280f, 0f, -45f), GameplayCamera.FieldOfView);
            leftCam.IsActive = true;
            // 设置为当前使用的视角
            World.RenderingCamera = leftCam;
        }

        public void MainTick(Object sender, EventArgs args)
        {
            try
            {
                SetTimeNoon(); // 设置时间为中午
                Detail.player = Game.Player; // 当前玩家
                Utils.IsPedInAnyVehicle(); // 玩家是否在车辆中
                if (Detail.showPostion)
                { // 在游戏界面上展示当前位置坐标
                    UI.Notify(Detail.player.Character.Position.ToString());
                }

                if (Detail.is_ped_in_any_vehicle)
                { //如果玩家在车辆中
                    Detail.car = Game.Player.Character.CurrentVehicle; // 玩家的当前车辆
                    Vector3 pos = Detail.car.Position; // 当前位置
                    Detail.onRoad = Function.Call<bool>(Hash.IS_POINT_ON_ROAD, pos.X, pos.Y, pos.Z, 0); //是否在道路上
                    Detail.car.MaxSpeed = 40f; // 车辆最大速度
                    if (Detail.car.EngineRunning == false)
                    { // 如果引擎熄火，就重启，常见于落水之后
                        Detail.car.EngineRunning = true;
                    }
                    ActiveCam(); //设置俯瞰视角
                    StrongCar(); // 车辆增强
                    Utils.IsPedInjured(); // 玩家是否受伤
                    Utils.IsPlayerInWater(); // 玩家是否在水里
                    RestoreVehicleHealth(); // 如果车辆健康值低到一定水平就回复健康
                }
                TeleportIfInWater(); // 如果在水里就传送到附近位置
                Utils.DrivingEvent(); // 获取当前的驾驶事件
                Utils.NearBys(); // 获取附近的车辆/行人/物体
                Utils.GetNextPositionOnStreet(); // 获取街道的下一个位置
                CheckAndClearWantedLevel(); // 清除通缉
                Convert.ConvertDetail(); // 将Detail 中的数据交给 DetailPOJO
            }
            catch (Exception e)
            {   // 如果出错了，就站是错误的游戏界面
                // 如果玩家不在车辆，那么有错误信息是正常的
                UI.Notify(e.StackTrace + DateTime.Now.ToString());
            }

        }
    }
}