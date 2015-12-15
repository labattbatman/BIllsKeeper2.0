using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StatManager : MonoBehaviour {

    public struct BillInfo
    {
        public string store;
        public string category;
        public float price;
        public string date;
    }

    public class CategoryTotal
    {
        public string category;
        public float amount;
    }

    DataManager dataManager;
    List<BillInfo> allBillInfo;
    List<CategoryTotal> allCategoryTotal;
    
    // Use this for initialization
    void Start ()
    {
        dataManager = GameObject.Find("Main Camera").GetComponent<DataManager>();      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AnalazeInfoBill()
    {
        string billsInfo = dataManager.GetBillData();

        allBillInfo = new List<BillInfo>();
        allCategoryTotal = new List<CategoryTotal>();

        string[] separetedBillsInfo = billsInfo.Split('\n');

        for(int i = 1; i < separetedBillsInfo.Length; i++)
        {
            if (string.IsNullOrEmpty(separetedBillsInfo[i]))
                continue;

            string[] values = separetedBillsInfo[i].Split(',');
            Debug.Log(separetedBillsInfo[i]);
            BillInfo billInfo;
            billInfo.store = values[0];
            billInfo.category = values[1];
            billInfo.price = float.Parse(values[2]);
            billInfo.date = values[3];

            allBillInfo.Add(billInfo);
        }

        for (int i = 0; i < allBillInfo.Count; i++)
        {
            bool isBillRegistred = false;
            for (int j = 0; j < allCategoryTotal.Count; j++)
            {
                if (allBillInfo[i].category == allCategoryTotal[j].category)
                {
                    allCategoryTotal[j].amount += allBillInfo[i].price;
                    isBillRegistred = true;
                }
            }

            if (!isBillRegistred)
            {
                CategoryTotal newCategoryAmount = new CategoryTotal();
                newCategoryAmount.category = allBillInfo[i].category;
                newCategoryAmount.amount = allBillInfo[i].price;

                allCategoryTotal.Add(newCategoryAmount);
            }

        }
        var test = from element in allCategoryTotal
                   orderby element.amount
                   select element;

        List<CategoryTotal> allCategoryTotalInOrder = new List<CategoryTotal>();
        foreach (CategoryTotal element in test)
            allCategoryTotalInOrder.Add(element);

        CategoryTotalInFile(allCategoryTotalInOrder, allBillInfo);
    }

    void CategoryTotalInFile(List<CategoryTotal> categoryTotal, List<BillInfo> billInfo)
    {
        string result = "\n" + "\n";

        for (int i = 0; i < categoryTotal.Count; i++)
        {
            result += categoryTotal[i].category + "," + categoryTotal[i].amount + "\n";
        }
        /*
        for (int i = 0; i < billInfo.Count; i++)
        {
            result += billInfo[i].store + "," + billInfo[i].category + "," + billInfo[i].price + "," + billInfo[i].date + "\n";
        }*/

        Debug.Log(result);
        dataManager.AddNewBill(result);
    }
}
