using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WPFProject.Models;

namespace WPFProject.ViewModels
{
    class DataAdabterViewModel
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        Manager manager = new Manager();
        public async Task Load()
        {
            using (FileStream fs = File.OpenRead("path to file"))
            {
                manager = await JsonSerializer.DeserializeAsync<Manager>(fs);
            }
        }
        public async Task Save()
        {
            using (FileStream fs = File.OpenWrite("Path to File"))
            {
                fs.Seek(0, SeekOrigin.Begin);
                await JsonSerializer.SerializeAsync(fs, manager, options);
            }
        }
    }
}
