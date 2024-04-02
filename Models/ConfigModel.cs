namespace DownloadConfiguationWeb.Models
{
    public class FhirServerSetting
    {
        public string Endpoint { get; set; }
        public AuthSetting Auth { get; set; }
        public SearchSetting Search { get; set; }
    }

    public class AuthSetting
    {
        public bool NeedToken { get; set; }
    }

    public class SearchSetting
    {
        public string SearchResource { get; set; }
        public string SearchParameter { get; set; }
        public string OrderBy { get; set; }
        public int Count { get; set; }
    }

    public class OutputSetting
    {
        public string OutputPlace { get; set; }
        public string FileName { get; set; }
        public int OutputNumber { get; set; }
        public string WorkSheetName { get; set; }
        public string OutputForm { get; set; }
    }

    public class ColumnsCustomized
    {
        public string Resource { get; set; }
        public string Name { get; set; }
        public string ColName { get; set; }
        public bool Show { get; set; }
        public bool Decrypt { get; set; }
        public bool Customized { get; set; }
        public string ResourcePath { get; set; }
        public string UpperPath { get; set; }
        public string DefaultValue { get; set; }
    }


    public class Configuration
    {
        public FhirServerSetting FhirServerSetting { get; set; }
        public OutputSetting Output { get; set; }
        public List<ColumnsCustomized> ColumnsCustomized { get; set; }
    }
}
