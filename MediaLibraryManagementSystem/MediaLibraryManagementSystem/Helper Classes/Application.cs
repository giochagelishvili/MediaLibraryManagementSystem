using MediaLibraryManagementSystem.Media_Classes;

namespace MediaLibraryManagementSystem.Helper_Classes
{
    public static class Application
    {
        private const string FilePath = "../../../../Media.txt";

        public static void Start()
        {
            List<Media> media = FileReader.ReadMedia(FilePath);
            User user = new("RoronoaZoro");

            MediaManager mediaManager = new(media, user);

            while (true)
            {
                mediaManager.ShowOperations();
                mediaManager.ExecuteOperation();
            }
        }
    }
}
