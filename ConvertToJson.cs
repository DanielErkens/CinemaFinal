using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

//Dit is de class voor het schrijven naar- en het lezen van de Json files.
public class ConvertToJson
{

    public User user;
    public string JsonFile;

    //Deze code is voor het uitlezen van het eerste json bestand, het user bestand.
    public List<User> loadJson1()
    {
        //Deze code pakt de lokale path name van het eerste json file.
        string path = Assembly.GetCallingAssembly().CodeBase;
        string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        string projectPath = new Uri(actualPath).LocalPath;
        string fileToRead = projectPath + @"save_file1.json";

        //Deze code leest alle info van het bestand uit, en zet het om naar json string.
        string JsonFile = File.ReadAllText(fileToRead);

        //Deze code zet de json string om naar een c# list en returnt de lijst met alle users.
        List<User> userList = JsonConvert.DeserializeObject<List<User>>(JsonFile);
        return userList;
    }

    //Deze code is voor het uitlezen van het tweede json bestand, het zalen bestand.
    public Zaal[] loadJson2()
    {
        //Deze code pakt de lokale path name van het tweede json file.
        string path = Assembly.GetCallingAssembly().CodeBase;
        string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        string projectPath = new Uri(actualPath).LocalPath;
        string fileToRead = projectPath + @"save_file2.json";

        //Deze code leest alle info van het bestand uit, en zet het om naar json string.
        string JsonFile = File.ReadAllText(fileToRead);

        //Deze code zet de json string om naar een c# list en returnt de lijst met alle users. Vervolgens wordt de lijst omgezet naar een array.
        List<Zaal> ZalenTemp = JsonConvert.DeserializeObject<List<Zaal>>(JsonFile);
        Zaal[] Zalen = ZalenTemp.ToArray();
        return Zalen;
    }


    //Deze code is voor het uitlezen van het derde json bestand, het movies bestand.
    public Movie[] loadJson3()
    {
        //Deze code pakt de lokale path name van het derde json file.
        string path = Assembly.GetCallingAssembly().CodeBase;
        string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        string projectPath = new Uri(actualPath).LocalPath;
        string fileToRead = projectPath + @"save_file3.json";

        //Deze code leest alle info van het bestand uit, en zet het om naar json string.
        string JsonFile = File.ReadAllText(fileToRead);

        //Deze code zet de json string om naar een c# list en returnt de lijst met alle users. Vervolgens wordt de lijst omgezet naar een array.
        List<Movie> MovieTemp = JsonConvert.DeserializeObject<List<Movie>>(JsonFile);
        Movie[] MovieList = MovieTemp.ToArray();
        return MovieList;
    }

    //Deze code is voor het schrijven naar alle json files.
    public void WriteToJson(List<User> userlist, Zaal[] Zalen, Movie[] Movies)
    {
        //Deze code pakt de locale opslagplaats van de json files.
        string path = Assembly.GetCallingAssembly().CodeBase;
        string actualPath = path.Substring(0, path.LastIndexOf("bin"));
        string projectPath = new Uri(actualPath).LocalPath;

        //Deze code is voor het compleet maken van alle 3 de paths van de json files.
        string fileToWrite1 = projectPath + @"save_file1.json";
        string fileToWrite2 = projectPath + @"save_file2.json";
        string fileToWrite3 = projectPath + @"save_file3.json";

        //Deze line zorgt ervoor dat de json string een leesbaar format krijgt.
        var options = new JsonSerializerOptions() { WriteIndented = true };

        //Deze line zet de list van users correct om.
        List<User> ListOfUsers = userlist.ToList<User>();

        //Deze lines zetten alle info die naar de json files geschreven moet worden om naar json strings, met het juiste format.
        string json1 = System.Text.Json.JsonSerializer.Serialize(ListOfUsers, options);
        string json2 = System.Text.Json.JsonSerializer.Serialize(Zalen, options);
        string json3 = System.Text.Json.JsonSerializer.Serialize(Movies, options);

        //Deze lines schrijven alle info naar de json files.
        File.WriteAllText(fileToWrite1, json1);
        File.WriteAllText(fileToWrite2, json2);
        File.WriteAllText(fileToWrite3, json3);
    }

}
