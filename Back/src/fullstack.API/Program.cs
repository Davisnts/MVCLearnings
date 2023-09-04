namespace Fullstack.API


{
    public class Program
    {

        public static void Main(string[] args)
        {
            if (!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}