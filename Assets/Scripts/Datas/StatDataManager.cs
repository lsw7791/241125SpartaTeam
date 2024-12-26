using MainData;

public class StatDataManager : PlayerStatData
{
    public PlayerStatData GetData(int id)
    {
        if (PlayerStatDataMap.ContainsKey(id))
        {
            return PlayerStatDataMap[id];
        }
        return null;
    }
}
