using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    const string savedBillData= "savedBillData";
    const string savedStoreData = "savedStoreData";
    const string savedCategoryData = "savedCategoryData";

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
            result += wordList[i];
            if (i != wordList.Count - 1)
                result += ",";
        }

        return result;
    }

    void SaveData(string dataType, string data)
    {
        Debug.Log("DataSaved: " + dataType + " " + data);
        PlayerPrefs.SetString(dataType, data);
    }

    string FindCategoryNameOfSave(string store)
    {
        return savedCategoryData + store;
    }

    public void EraseData()
    {
        PlayerPrefs.SetString(savedBillData, string.Empty);
    }

    public string GetBillData()
    {
        return PlayerPrefs.GetString(savedBillData, string.Empty);
    }

    public string GetCategorySaved(string store)
    {
        return "blab,fdsaf,dfdaf,eee";
        return PlayerPrefs.GetString(FindCategoryNameOfSave(store), string.Empty);
    }

    public string GetStoreSaved()
    {
        return PlayerPrefs.GetString(savedStoreData, string.Empty);
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

        if(!storeCategory.Contains(store.ToLower()))
        {
            storeCategory += store.ToLower() + ",";
            storeCategory = OrderWordAlphabetically(storeCategory);

            SaveData(nameOfSave, storeCategory);
        }
    }

    public void AddNewStore(string store)
    {
        string stores = PlayerPrefs.GetString(savedStoreData, string.Empty);

        if (!stores.Contains(store.ToLower()))
        {
            stores += store.ToLower() + ",";
            stores = OrderWordAlphabetically(stores);

            SaveData(savedStoreData, stores);
        }
    }

    //RAJOUTER LES NOUVEAUX STORE + CATEGORIES
}