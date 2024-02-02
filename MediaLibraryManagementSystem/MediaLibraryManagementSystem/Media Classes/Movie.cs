

namespace MediaLibraryManagementSystem.Media_Classes
{
    public class Movie : Media
    {
        public int Duration { get; set; }

        public Movie(string title, int releaseYear, decimal rating, Genre genre, string director, Language language, string country, List<string> actors, int duration) : base(title, releaseYear, rating, genre, director, language, country, actors)
        {
            Duration = duration;
        }

        public Movie(Movie chosenMovie) : base(chosenMovie)
        {
            Duration = chosenMovie.Duration;
        }

        public override async Task Play()
        {
            if (Cts.IsCancellationRequested)
            {
                Cts.Dispose();
                Cts = new();
            }

            Stopwatch.Start();

            Console.WriteLine($"{Title} has started playing.");

            Console.WriteLine("-----------------------------");

            await Task.Run(() =>
            {
                while (true)
                {
                    if (Stopwatch.Elapsed.TotalSeconds > Duration)
                    {
                        Stopwatch.Stop();
                        OnFinishedPlaying();
                        break;
                    }
                    else if (Cts.IsCancellationRequested)
                    {
                        Stopwatch.Stop();
                        break;
                    }
                }
            });
        }
    }
}
