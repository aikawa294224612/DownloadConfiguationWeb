using DownloadConfiguationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;

namespace DownloadConfiguationWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            string jsonFilePath = "template.json"; 

            string jsonText = System.IO.File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<Configuration>(jsonText);

            return View(config);
        }

        [HttpPost] 
        public IActionResult CreateConfig(IFormCollection form)
        {
            string endpoint = form["endpoint"];
            bool needToken = form["needToken"] == "true" ? true : false;
            string searchResource = form["searchResource"];
            string searchParameter = form["searchParameter"];
            string orderBy = form["orderBy"];
            string outputForm = form["outputForm"];
            string outputPlace = form["outputPlace"];
            string fileName = form["fileName"];
            int outputNumber = int.Parse(form["outputNumber"]);
            string workSheetName = form["workSheetName"];

            int index = int.Parse(form["tableIndex"])+1;
            Console.WriteLine(index);

            AuthSetting auth = new AuthSetting();
            auth.NeedToken = needToken;

            SearchSetting search = new SearchSetting();
            search.SearchResource = searchResource;
            search.SearchParameter = searchParameter;
            search.OrderBy = orderBy;
            search.Count = 10;

            FhirServerSetting fhir = new FhirServerSetting();
            fhir.Endpoint = endpoint;
            fhir.Auth = auth;
            fhir.Search = search;            

            OutputSetting output = new OutputSetting();
            output.OutputForm = outputForm;
            output.OutputPlace = outputPlace;
            output.FileName = fileName;
            output.OutputNumber = outputNumber;
            output.WorkSheetName = workSheetName;

            List<ColumnsCustomized> columnsCustomized = new List<ColumnsCustomized>();
            for (int i = 0; i < index; i++)
            {
                string resource = form["resource-" + i];
                string name = form["name-" + i];
                string upper = form["upper-" + i];
                string resourcePath = form["resourcepath-" + i];
                string colName = form["colname-" + i];
                bool show = form["show-" + i] == "on"; 
                bool decrypt = form["decrypt-" + i] == "on";
                string defaultValue = form["default-" + i];

                if(resource != "")
                {
                    ColumnsCustomized column = new ColumnsCustomized
                    {
                        Resource = resource,
                        Name = name,
                        UpperPath = upper,
                        ResourcePath = resourcePath,
                        ColName = colName,
                        Show = show,
                        Decrypt = decrypt,
                        DefaultValue = defaultValue
                    };

                    columnsCustomized.Add(column);
                }
                
            }
           

            Configuration configuration = new Configuration();
            configuration.FhirServerSetting = fhir;
            configuration.Output = output;
            configuration.ColumnsCustomized = columnsCustomized;

            string jsonData = JsonConvert.SerializeObject(configuration);

            string filePath = "C:\\Users\\polly.peng\\Downloads\\appsettings.json";
            using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                streamWriter.Write(jsonData);
            }

            return Json(new { success = true, message = "" });
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
