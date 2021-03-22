using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Security
{
    public class AppSettings
    {
        public IDictionary<string, string> ConnectionStrings { get; set; }
        public string this[string key] { get => ConnectionStrings[key]; }

        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }

        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
        public string MicrosoftClientId { get; set; }
        public string MicrosoftClientSecret { get; set; }

        public AppSettings() { }
    }
}
