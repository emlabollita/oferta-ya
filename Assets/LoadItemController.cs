using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadItemController : MonoBehaviour
{

    public CategoryItems categoryItems;
    public ProductItems productItems;

    void Start()
    {
        StartCoroutine(GetJson("http://squaar.com/ofertaya/categories.json"));
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void SetCategoryId(int id) {
        Debug.Log(id);
    }

    IEnumerator GetJson(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log("JSON: " + json);

            categoryItems = JsonUtility.FromJson<CategoryItems>(json);

            for (int i = 0; i < categoryItems.Categories.Length; i++)
            {
                if (categoryItems.Categories[i].augmented)
                {
                    VideosARItem arItem = new VideosARItem(categoryItems.Categories[i].id, categoryItems.Categories[i].urlvideo, categoryItems.Categories[i].name, categoryItems.Categories[i].relatedProduct);
                    //GameObject.Find("Scripts").GetComponent<ApplicationController>().videosArItemList.Add(arItem);
                }


            }

            StartCoroutine(LoadJsonProducts("http://squaar.com/ofertaya/products.json"));
            
        }
    }

    IEnumerator LoadJsonProducts(string url)
    {

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            Debug.Log("JSON: " + json);

            productItems = JsonUtility.FromJson<ProductItems>(json);
            Debug.Log(productItems.Products.Length);
            for (int i = 0; i < productItems.Products.Length; i++)
            {
                Debug.Log("JSON-VALUES-PRODUCTS: " + productItems.Products[i].id);
            }
        }

    }

    [Serializable]
    public class CategoryItems
    {
        public Categories[] Categories;
    }
    [Serializable]
    public class Categories
    {
        public int id, relatedProduct;
        public string name, urlvideo;
        public bool augmented;
    }

    //PRODUCTS 
}

[Serializable]
    public class ProductItems
    {
        public Products[] Products;
    }
    [Serializable]
    public class Products
    {
        public int id, discount, category;
        public string company, title, description, thumbnail;
        public float[] prices;
    }

    public class VideosARItem
    {
        public int id, relatedProduct;
        public string video;
        public string url;

        public VideosARItem(int _id, string _video, string _url, int _relatedProduct)
        {
            this.id = _id;
            this.video = _video;
            this.url = _url;
            this.relatedProduct = _relatedProduct;
        }
}
