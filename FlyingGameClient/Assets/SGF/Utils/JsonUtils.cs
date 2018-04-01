using System.IO;
using Newtonsoft.Json;

namespace SGF.Utils
{
    public static class JsonUtils
    {
        /// <summary>
        /// 根据指定路径加载json文件，并序列化为指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public static T LoadJsonFromFile<T>(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                return default(T);
            }

            StreamReader sr = new StreamReader(jsonFilePath);


            if (sr == null)
            {
                return default(T);
            }
            string json = sr.ReadToEnd();
            sr.Close();
            if (json.Length > 0)
            {

                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }

        /// <summary>
        /// 将数据写入到json文件中
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <param name="obj"></param>
        public static void WriteDataToJsonFile(string jsonFilePath, object obj)
        {
            string jsonStr = JsonConvert.SerializeObject(obj);
            if (string.IsNullOrEmpty(jsonStr))
                return;

            StreamWriter writer = new StreamWriter(jsonFilePath);

            writer.Write(jsonStr);
            writer.Flush();
            writer.Close();
        }

    }
}


