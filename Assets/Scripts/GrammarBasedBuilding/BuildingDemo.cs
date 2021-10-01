using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class BuildingDemo : MonoBehaviour
{
    public BuildingSettings settings;
    void Start()
    {
        //await GetWebpageAsync();

        Building b = BuildingGenerator.Generate(settings);

        GetComponent<BuildingRenderer>().Render(b);
        Debug.Log(b);
    }

    async System.Threading.Tasks.Task GetWebpageAsync()
    {
        HttpClient httpClient = new HttpClient();

        var response = await httpClient.GetAsync("https://www.bbc.co.uk/news");
        var pageContents = await response.Content.ReadAsStringAsync();

        Debug.Log(pageContents);
    }

}
