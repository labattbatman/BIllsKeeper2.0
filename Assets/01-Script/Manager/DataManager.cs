using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour
{
    const string savedBillData= "savedBillData";
    const string savedStoreData = "savedStoreData";
    const string savedCategoryData = "savedCategoryData";
    const string savedAllCategoryData = "savedAllCategoryData";
    const string savedStats = "savedStated";

    void Start()
    {
        
    }

    string OrderWordAlphabetically(string words)
    {
        string result = string.Empty;
        List<string> wordList = new List<string>();

        foreach (string w in words.Split(','))
            wordList.Add(w);

        wordList.Sort();

        for (int i = 0; i < wordList.Count; i++)
        {
            if (!string.IsNullOrEmpty(wordList[i]))
            {
                result += wordList[i];
                if (i != wordList.Count - 1)
                    result += ",";
            }
        }

        return result;
    }

    void SaveData(string dataType, string data)
    {
        Debug.Log("DataSaved: " + dataType + " " + data);
        PlayerPrefs.SetString(dataType, data);
        PlayerPrefs.Save();
    }

    string FindCategoryNameOfSave(string store)
    {
        return savedCategoryData + store;
    }

    public void EraseData()
    {
        PlayerPrefs.SetString(savedBillData, string.Empty);
        PlayerPrefs.DeleteAll();
    }

    public string GetBillData()
    {
        return PlayerPrefs.GetString(savedBillData, string.Empty);
    }

    public string GetCategorySaved(string store)
    {
        string result = PlayerPrefs.GetString(FindCategoryNameOfSave(store), string.Empty);
        result += "," + PlayerPrefs.GetString(savedAllCategoryData, string.Empty);

        string[] resultInArray = result.Split(',');
        resultInArray = resultInArray.Distinct().ToArray();
        
        result = string.Empty;
        for (int i = 0; i < resultInArray.Length; i++)
            result += resultInArray[i] + ",";
        
        return result;
    }

    public string GetStoreSaved()
    {
        return PlayerPrefs.GetString(savedStoreData, string.Empty);
    }

    public string GetSavedStats()
    {
        return PlayerPrefs.GetString(savedStats);
    }

    public void AddNewBill(string billInfo)
    {
        string billSaved = GetBillData();

        if(string.IsNullOrEmpty(billSaved))
            billSaved = "Store,Category,Amount,Date \n";

        billSaved += billInfo + "\n";
        SaveData(savedBillData, billSaved);
    }

    public void AddNewCategory(string store, string category)
    {
        string nameOfSave = FindCategoryNameOfSave(store);
        string storeCategory = PlayerPrefs.GetString(nameOfSave, string.Empty);
        
        if(!storeCategory.Contains(category.ToLower()))
        {
            storeCategory += "," + category.ToLower();
            storeCategory = OrderWordAlphabetically(storeCategory);

            SaveData(nameOfSave, storeCategory);
            AddNewCategoryToAllCategory(storeCategory);
        }
    }

    public void AddNewStore(string store)
    {
        string stores = PlayerPrefs.GetString(savedStoreData, string.Empty);

        if (!stores.Contains(store.ToLower()))
        {
            stores += "," + store.ToLower();
            stores = OrderWordAlphabetically(stores);
            SaveData(savedStoreData, stores);
        }
    }

    public void SavedNewStats(string stats)
    {
        SaveData(savedStats, stats);
    }

    void AddNewCategoryToAllCategory(string category)
    {
        string allCategory = PlayerPrefs.GetString(savedAllCategoryData, string.Empty);
        if (!allCategory.Contains(category.ToLower()))
        {
            allCategory += "," + category.ToLower();
            allCategory = OrderWordAlphabetically(allCategory);

            SaveData(savedAllCategoryData, allCategory);
        }
    }

}