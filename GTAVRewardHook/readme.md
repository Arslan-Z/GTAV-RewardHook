# 代码详解

## [Convert.cs](Convert.cs)

将 `Detail` 类以及 GTAV 的`Vector3`,`Entity`,`Vehicle`,`Ped` 转换为相应的 POJO 类，这么做主要是因为 GTAV 的`Vector3`,`Entity`,`Vehicle`,`Ped`类对象无法转换成 Json 字符串，

- `ConvertVector3` --- 将 `GTA` 的 `Vector3` 类转换成 `Vector3POJO`
- `ConvertEntity` --- 将 `GTA` 的 `Entity` 类转换成 `EntityPOJO`
- `ConvertVehicle` --- 将 `GTA` 的 `Vehicle` 类转换成 `VehiclePOJO`
- `ConvertPed` --- 将 `GTA` 的 `Ped` 类转换成 `PedPOJO`
- `ToJson` 将 `DetailPOJO`对象转换成 `Json` 字符串以备传输
- `ConvertDetail` --- 将 `Detail` 类转换成 `DetailPOJO`

## [Detail.cs](Detail.cs)

所有我们需要的游戏数据都是 `Detail` 类的静态属性，选择静态属性是为了在 `GameTracker` 的 `MainTick` 函数在两次运行之间持有数据

## [DetailPOJO.cs](DetailPOJO.cs)

`Detail` 类 的 POJO 类，属性一直，使用单例模式

## [EntityPOJO.cs](EntityPOJO.cs)

`GAT.Entity`类的 POJO 类，选取了`GAT.Entity`类的部分属性
`GAT.Entity`类表示游戏中的物体

## [GameTracker.cs](GameTracker.cs)

继承 `GTA` 的 `Script` 类，是脚本运行入口
主要函数

- `GameTracker` 构造函数，初始化 `Nancy` 服务器，指定要循环执行的函数(`MainTick`)，指定键盘事件处理函数
- `MainTick` 要循环执行的函数,每个指定事件执行一次
- `OnKeyUp` 键盘事件处理函数
  - > `e.Control && e.KeyCode == Keys.T` 表示同时按下 CTRL 和 T
- `SetTimeNoon` 设置游戏时间为中午
- `RandomTelePort` 随机传送到附近的位置(用于卡住或落水时脱困)
- `CreateVehicleBeforePlayer` 在玩家面前创造一辆汽车，同时玩家进入
- `BackToStartPosition` 回到起点
- `GoToEndPostion` 去往终点
- `ForwardBackSide` 车头调转
- `StrongCar` 汽车增强(不爆胎，不被打，不通缉)
- `DrawLine` 在游戏指定位置画线条
- `VehicleLock` 锁车(防止游戏过程中被 NPC 拖出去)
- `RestoreVehicleHealth` 汽车损坏到一定程度自动修复
- `DeactiveCam` 重置视角(回复原始视角)
- `DrawTouching` 所有触碰的物体/行人/车辆都画上线条
- `CheckAndClearWantedLevel` 如果被通缉就清除
- `TeleportIfInWater` 如果落水就传送到附近一个位置
- `ActiveCam` 激活俯瞰视角

## [PedPOJO.cs](PedPOJO.cs)

`GAT.Ped`类的 POJO 类，选取了`GAT.Ped`类的部分属性;

`GAT.Ped`类表示游戏中的人

## [Server.cs](Server.cs)

`Nancy`服务器的启动类，指定运行的端口

## [ServiceModule.cs](ServiceModule.cs)

负责两个 URL 的 GET 请求处理;

- `/test` 测试网络是否联通
- `/data` 游戏数据

## [Utils.cs](Utils.cs)

- `GetNextPositionOnStreet` 获取街道的下一个位置
- `DrivingEvent` 驾驶过程的事件，包括逆行、在人行道上行驶、撞人、撞车
- `GetWeather` 获取天气
- `TelePort` 传送到指定位置
- `NearBys` 附近的信息，包括附近行人/车辆/物体
- `IsPedInjured` 玩家是否受伤
- `IsPedInAnyVehicle` 玩家是否在车里
- `IsPlayerInWater` 是否落水

## [Vector3POJO.cs](Vector3POJO.cs)

`GAT.Vector3`类的 POJO 类;

`GAT.Vector3`类表示三维向量

## [VehiclePOJO.cs](VehiclePOJO.cs)

`GAT.Vehicle`类的 POJO 类，选取了`GAT.Vehicle`类的部分属性;

`GAT.Vehicle`类表示游戏中的交通工具(车辆/飞机/船)
