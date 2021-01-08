using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LibShare.Api.Helpers
{
    public static class InitStaticFiles
    {
        public static string CreateFolderServer(IWebHostEnvironment env,
            IConfiguration configuration, string[] settings)
        {
            string fileDestDir = env.ContentRootPath;
            foreach (var pathConfig in settings)
            {
                fileDestDir = Path.Combine(fileDestDir, configuration.GetValue<string>(pathConfig));
                if (!Directory.Exists(fileDestDir))
                {
                    Directory.CreateDirectory(fileDestDir);
                }
            }
            return fileDestDir;
        }
    }
}
