using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GTA;
using GTA.Math;
using Nancy;

namespace GTAVRewardHook
{
    public class ServiceModule : NancyModule
    {
        public ServiceModule()
        {
            //测试用
            Get["/test"] = (x => "ok");

            // 获取游戏数据
            Get["/data"] = _ =>
            {
                DetailPOJO pojo = DetailPOJO.Instance(); // 存储当前数据的对象
                string json = Convert.ToJson(); // 转换为Json字符串
                return json;
            };
        }

    }

}