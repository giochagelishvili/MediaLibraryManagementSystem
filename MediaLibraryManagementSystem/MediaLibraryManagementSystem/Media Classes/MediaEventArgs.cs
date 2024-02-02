namespace MediaLibraryManagementSystem.Media_Classes
{
    public class MediaEventArgs : EventArgs
    {
        public MediaEventArgs(Media media)
        {
            Media = media;
        }

        public Media Media { get; set; }
    }
}