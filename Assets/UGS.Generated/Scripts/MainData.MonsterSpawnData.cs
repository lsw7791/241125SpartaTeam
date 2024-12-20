
/*     ===== Do not touch this. Auto Generated Code. =====    */
/*     If you want custom code generation modify this => 'CodeGeneratorUnityEngine.cs'  */
using GoogleSheet.Protocol.v2.Res;
using GoogleSheet.Protocol.v2.Req;
using UGS;
using System;
using UGS.IO;
using GoogleSheet;
using System.Collections.Generic;
using System.IO;
using GoogleSheet.Type;
using System.Reflection;
using UnityEngine;


namespace MainData
{
    [GoogleSheet.Attribute.TableStruct]
    public partial class MonsterSpawnData : ITable
    { 

        public delegate void OnLoadedFromGoogleSheets(List<MonsterSpawnData> loadedList, Dictionary<int, MonsterSpawnData> loadedDictionary);

        static bool isLoaded = false;
        static string spreadSheetID = "1Daaes6kJu0aN1dc6gYH59jZtRFLFk24qEeAlCETDpTw"; // it is file id
        static string sheetID = "1298053461"; // it is sheet id
        static UnityFileReader reader = new UnityFileReader();

/* Your Loaded Data Storage. */
    
        public static Dictionary<int, MonsterSpawnData> MonsterSpawnDataMap = new Dictionary<int, MonsterSpawnData>();  
        public static List<MonsterSpawnData> MonsterSpawnDataList = new List<MonsterSpawnData>();   

        /// <summary>
        /// Get MonsterSpawnData List 
        /// Auto Load
        /// </summary>
        public static List<MonsterSpawnData> GetList()
        {{
           if (isLoaded == false) Load();
           return MonsterSpawnDataList;
        }}

        /// <summary>
        /// Get MonsterSpawnData Dictionary, keyType is your sheet A1 field type.
        /// - Auto Load
        /// </summary>
        public static Dictionary<int, MonsterSpawnData>  GetDictionary()
        {{
           if (isLoaded == false) Load();
           return MonsterSpawnDataMap;
        }}

    

/* Fields. */

		public System.Int32 id;
		public System.Collections.Generic.List<Int32> MonsterID;
		public System.String MonsterPosition;
  

#region fuctions


        public static void Load(bool forceReload = false)
        {
            if(isLoaded && forceReload == false)
            {
#if UGS_DEBUG
                 Debug.Log("MonsterSpawnData is already loaded! if you want reload then, forceReload parameter set true");
#endif
                 return;
            }

            string text = reader.ReadData("MainData"); 
            if (text != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadSpreadSheetResult>(text);
                CommonLoad(result.jsonObject, forceReload); 
                if(!isLoaded)isLoaded = true;
            }
      
        }
 

        public static void LoadFromGoogle(System.Action<List<MonsterSpawnData>, Dictionary<int, MonsterSpawnData>> onLoaded, bool updateCurrentData = false)
        {      
                IHttpProtcol webInstance = null;
    #if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    webInstance = UnityEditorWebRequest.Instance as IHttpProtcol;
                }
                else 
                {
                    webInstance = UnityPlayerWebRequest.Instance as IHttpProtcol;
                }
    #endif
    #if !UNITY_EDITOR
                     webInstance = UnityPlayerWebRequest.Instance as IHttpProtcol;
    #endif
          
 
                var mdl = new ReadSpreadSheetReqModel(spreadSheetID);
                webInstance.ReadSpreadSheet(mdl, OnError, (data) => {
                    var loaded = CommonLoad(data.jsonObject, updateCurrentData); 
                    onLoaded?.Invoke(loaded.list, loaded.map);
                });
        }

               


    public static (List<MonsterSpawnData> list, Dictionary<int, MonsterSpawnData> map) CommonLoad(Dictionary<string, Dictionary<string, List<string>>> jsonObject, bool forceReload){
            Dictionary<int, MonsterSpawnData> Map = new Dictionary<int, MonsterSpawnData>();
            List<MonsterSpawnData> List = new List<MonsterSpawnData>();     
            TypeMap.Init();
            FieldInfo[] fields = typeof(MonsterSpawnData).GetFields(BindingFlags.Public | BindingFlags.Instance);
            List<(string original, string propertyName, string type)> typeInfos = new List<(string, string, string)>(); 
            List<List<string>> rows = new List<List<string>>();
            var sheet = jsonObject["MonsterSpawnData"];

            foreach (var column in sheet.Keys)
            {
                string[] split = column.Replace(" ", null).Split(':');
                         string column_field = split[0];
                string   column_type = split[1];

                typeInfos.Add((column, column_field, column_type));
                          List<string> typeValues = sheet[column];
                rows.Add(typeValues);
            }

          // 실제 데이터 로드
                    if (rows.Count != 0)
                    {
                        int rowCount = rows[0].Count;
                        for (int i = 0; i < rowCount; i++)
                        {
                            MonsterSpawnData instance = new MonsterSpawnData();
                            for (int j = 0; j < typeInfos.Count; j++)
                            {
                                try
                                {
                                    var typeInfo = TypeMap.StrMap[typeInfos[j].type];
                                    //int, float, List<..> etc
                                    string type = typeInfos[j].type;
                                    if (type.StartsWith(" < ") && type.Substring(1, 4) == "Enum" && type.EndsWith(">"))
                                    {
                                         Debug.Log("It's Enum");
                                    }

                                    var readedValue = TypeMap.Map[typeInfo].Read(rows[j][i]);
                                    fields[j].SetValue(instance, readedValue);

                                }
                                catch (Exception e)
                                {
                                    if (e is UGSValueParseException)
                                    {
                                        Debug.LogError("<color=red> UGS Value Parse Failed! </color>");
                                        Debug.LogError(e);
                                        return (null, null);
                                    }

                                    //enum parse
                                    var type = typeInfos[j].type;
                                    type = type.Replace("Enum<", null);
                                    type = type.Replace(">", null);

                                    var readedValue = TypeMap.EnumMap[type].Read(rows[j][i]);
                                    fields[j].SetValue(instance, readedValue); 
                                }
                              
                            }
                            List.Add(instance); 
                            Map.Add(instance.id, instance);
                        }
                        if(isLoaded == false || forceReload)
                        { 
                            MonsterSpawnDataList = List;
                            MonsterSpawnDataMap = Map;
                            isLoaded = true;
                        }
                    } 
                    return (List, Map); 
}


 

        public static void Write(MonsterSpawnData data, System.Action<WriteObjectResult> onWriteCallback = null)
        { 
            TypeMap.Init();
            FieldInfo[] fields = typeof(MonsterSpawnData).GetFields(BindingFlags.Public | BindingFlags.Instance);
            var datas = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                string writeRule = null;
                if(type.IsEnum)
                {
                    writeRule = TypeMap.EnumMap[type.Name].Write(fields[i].GetValue(data));
                }
                else
                {
                    writeRule = TypeMap.Map[type].Write(fields[i].GetValue(data));
                } 
                datas[i] = writeRule; 
            }  
           
#if UNITY_EDITOR
if(Application.isPlaying == false)
{
                UnityPlayerWebRequest.Instance.WriteObject(new WriteObjectReqModel(spreadSheetID, sheetID, datas[0], datas), OnError, onWriteCallback);

}
else
{
            UnityPlayerWebRequest.Instance.WriteObject(new  WriteObjectReqModel(spreadSheetID, sheetID, datas[0], datas), OnError, onWriteCallback);

}
#endif

#if !UNITY_EDITOR
   UnityPlayerWebRequest.Instance.WriteObject(new  WriteObjectReqModel(spreadSheetID, sheetID, datas[0], datas), OnError, onWriteCallback);

#endif
        } 
          

 


#endregion

#region OdinInsepctorExtentions
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button("UploadToSheet")]
    public void Upload()
    {
        Write(this);
    }
 
    
#endif


 
#endregion
    public static void OnError(System.Exception e){
         UnityGoogleSheet.OnTableError(e);
    }
 
    }
}
        