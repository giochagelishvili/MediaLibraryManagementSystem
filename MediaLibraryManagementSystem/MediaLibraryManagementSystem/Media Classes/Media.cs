using System.Diagnostics;

namespace MediaLibraryManagementSystem.Media_Classes
{
    public abstract class Media
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rating { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public Language Language { get; set; }
        public string Country { get; set; }
        public List<string> Actors { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public CancellationTokenSource Cts { get; set; }

        public event EventHandler<MediaEventArgs> FinishedPlaying;

        public Media(string title, int releaseYear, decimal rating, Genre genre, string director, Language language, string country, List<string> actors)
        {
            Title = title;
            ReleaseYear = releaseYear;
            Rating = rating;
            Genre = genre;
            Director = director;
            Language = language;
            Country = country;
            Actors = actors;
            Stopwatch = new();
            Cts = new();
        }

        public Media(Media media)
        {
            Title = media.Title;
            ReleaseYear = media.ReleaseYear;
            Rating = media.Rating;
            Genre = media.Genre;
            Director = media.Director;
            Language = media.Language;
            Country = media.Country;
            Actors = media.Actors;
            Stopwatch = new();
            Cts = new();
        }

        public override string ToString() => $"{Title}({ReleaseYear}) directed by {Director}";

        protected void OnFinishedPlaying()
        {
            if (FinishedPlaying != null)
                FinishedPlaying.Invoke(this, new MediaEventArgs(this));
        }

        public void Pause()
        {
            Cts.Cancel();
            Console.WriteLine($"{Title} has been paused.");
            Console.WriteLine($"Elapsed time: {Stopwatch.Elapsed.TotalSeconds}");
        }

        public abstract Task Play();
    }

    public enum Genre
    {
        Comedy = 1,
        Detective,
        Romantic,
        Thriller,
        Horror,
        Family,
        Animation,
        Drama,
        Action,
        Crime,
    }

    public enum Language
    {
        English = 1,
        Japanese,
        Georgian
    }
}
