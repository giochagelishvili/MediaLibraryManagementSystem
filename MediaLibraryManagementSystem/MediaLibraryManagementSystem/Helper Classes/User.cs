using MediaLibraryManagementSystem.Media_Classes;

namespace MediaLibraryManagementSystem.Helper_Classes
{
    public class User
    {
        public string Username { get; set; }
        public List<Media> WatchList { get; set; }
        public List<Media> AlreadyWatched { get; set; }
        public List<Media> SubscribedMovies { get; set; }
        public List<Media> CurrentlyWatching { get; set; }

        public User(string username)
        {
            Username = username;
            WatchList = new();
            AlreadyWatched = new();
            SubscribedMovies = new();
            CurrentlyWatching = new();
        }

        public async Task Watch(Media media)
        {
            if (!SubscribedMovies.Any(subscribedMovie => subscribedMovie.Title == media.Title))
            {
                Console.WriteLine($"{media.Title} is not subscribed.");
                Console.WriteLine("-----------------------------");
                return;
            }

            CurrentlyWatching.Add(media);

            media.FinishedPlaying += OnFinishedPlaying;

            await media.Play();
        }

        public void OnFinishedPlaying(object? sender, MediaEventArgs args)
        {
            var movie = args.Media;

            Console.WriteLine($"\n{movie.Title} has finished playing.");

            CurrentlyWatching.Remove(movie);

            AlreadyWatched.Add(movie);
        }
    }
}
