namespace MediaLibraryManagementSystem.Helper_Classes
{
    public static class Logger
    {
        private const string LogPath = "Logs.txt";

        public static void Log(Exception ex)
        {
            using(var sw = new StreamWriter(LogPath, true)) 
            {
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("--------------------");
            }
        }
    }
}
