using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;


public class Data
{
    public long curMoney;
    public long clickPower;
    public long totalAutoPower;

    public string[] clickName;
    public long[,] clickValues;

    public string[] autoName;
    public long[,] autoValues;
}


public class SaveAndLoad : MonoBehaviour
{
    public void Save()
    {
        Debug.Log("저장 시도");

        // 생성
        Data data = new Data();

        // 정보를 담아준다.
        data.curMoney = GameManager.Instance.CurMoney;
        data.clickPower = GameManager.Instance.ClickPower;
        data.totalAutoPower = GameManager.Instance.TotalAutoPower;

        List<AutoClicker> clicks = GameManager.Instance.clickUpgrade;
        data.clickName = new string[clicks.Count];
        data.clickValues = new long[clicks.Count, 3];
        for (int i = 0; i < clicks.Count; i++)
        {
            data.clickName[i] = clicks[i].itemSO._name;
            data.clickValues[i, 0] = clicks[i].CheckValue();
            data.clickValues[i, 1] = clicks[i].CheckUpgrade();
            data.clickValues[i, 2] = clicks[i].CheckPrice();
        }

        List<AutoClicker> autos = GameManager.Instance.autoClickers;
        data.autoName = new string[autos.Count];
        data.autoValues = new long[autos.Count, 3];
        for (int i = 0; i < autos.Count; i++)
        {
            data.autoName[i] = autos[i].itemSO._name;
            data.autoValues[i, 0] = autos[i].CheckValue();
            data.autoValues[i, 1] = autos[i].CheckUpgrade();
            data.autoValues[i, 2] = autos[i].CheckPrice();
        }

        FileStream saveStream = new FileStream(Application.dataPath + "/save.json", FileMode.OpenOrCreate);

        string jsonSaveData = JsonConvert.SerializeObject(data);

        Debug.Log(jsonSaveData);

        byte[] saveData = Encoding.UTF8.GetBytes(jsonSaveData);

        saveStream.Write(saveData, 0, saveData.Length);
        saveStream.Close();

        Debug.Log("저장 완료!");
    }

    public void Load()
    {
        // 방어 코드
        string filePath = Application.dataPath + "/save.json";
        if (File.Exists(filePath))
        {
            Debug.Log("데이터를 불러오는 중..");

            FileStream loadStream = new FileStream(filePath, FileMode.Open);
            byte[] loadData = new byte[loadStream.Length];
            loadStream.Read(loadData, 0, loadData.Length);
            loadStream.Close();
            string jsonLoadData = Encoding.UTF8.GetString(loadData);

            Data data = JsonConvert.DeserializeObject<Data>(jsonLoadData);

            GameManager.Instance.CurMoney = data.curMoney;
            GameManager.Instance.ClickPower = data.clickPower;
            GameManager.Instance.TotalAutoPower = data.totalAutoPower;

            foreach (AutoClicker auto in ShopManager.Instance.forSave)
            {
                for (int i = 0; i < data.clickName.Length; i++)
                {
                    if (auto.itemSO._name == data.clickName[i])
                    {
                        auto.LoadData(data.clickValues[i, 0], data.clickValues[i, 1], data.clickValues[i, 2]);
                    }
                }
                for (int i = 0; i < data.autoName.Length; i++)
                {
                    if (auto.itemSO._name == data.autoName[i])
                    {
                        auto.LoadData(data.autoValues[i, 0], data.autoValues[i, 1], data.autoValues[i, 2]);
                    }
                }
            }

            Debug.Log("데이터 호출 완료!");
        }
        else
        {
            Debug.Log("파일이 없습니다..");
        }
    }
}
