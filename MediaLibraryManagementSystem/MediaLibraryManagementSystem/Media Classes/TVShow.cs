

namespace MediaLibraryManagementSystem.Media_Classes
{
    public class TVShow : Media
    {
        public int Episodes { get; set; }
        private int WatchedEpisodes { get; set; } = 0;

        private const int EpisodeDuration = 10;

        public TVShow(string title, int releaseYear, decimal rating, Genre genre, string director, Language language, string country, List<string> actors, int episodes) : base(title, releaseYear, rating, genre, director, language, country, actors)
        {
            Episodes = episodes;
        }

        public TVShow(TVShow tvShow) : base(tvShow) 
        {
            Episodes = tvShow.Episodes;
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
                    if(Stopwatch.Elapsed.TotalSeconds > EpisodeDuration)
                    {
                        WatchedEpisodes++;

                        if(WatchedEpisodes >= Episodes)
                        {
                            OnFinishedPlaying();
                            break;
                        }

                        Stopwatch.Restart();
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
