using MediaLibraryManagementSystem.Media_Classes;

namespace MediaLibraryManagementSystem.Helper_Classes
{
    public class MediaManager
    {
        private List<Media> MediaList { get; set; }
        private List<Media> FilteredMedia { get; set; }
        private User User { get; set; }
        private Dictionary<int, string> Operations { get; set; }
        public MediaManager(List<Media> mediaList, User user)
        {
            MediaList = mediaList;
            FilteredMedia = MediaList;
            User = user;
            Operations = new()
            {
                { 1, "Show Available Media" },
                { 2, "Search" },
                { 3, "Add Filter" },
                { 4, "See Watchlist" },
                { 5, "Add To Watchlist" },
                { 6, "Remove From Watchlist" },
                { 7, "See Subscribtions" },
                { 8, "Subscribe Movie" },
                { 9, "Unsubscribe Movie" },
                { 10, "Watch Movie" },
                { 11, "Pause Movie" },
                { 12, "Continue Watching" }
            };
        }

        public void ExecuteOperation()
        {
            Console.Write("Please choose operation: ");
            int operationIndex = Interaction.GetIntInput(Operations.Count);

            Console.WriteLine("-----------------------------");

            switch (operationIndex)
            {
                case 1:
                    PrintMedia();
                    break;
                case 2:
                    SearchMedia();
                    break;
                case 3:
                    FilterMedia();
                    break;
                case 4:
                    ShowWatchList();
                    break;
                case 5:
                    AddToWatchList();
                    break;
                case 6:
                    DeleteFromWatchList();
                    break;
                case 7:
                    ShowSubscribedMovies();
                    break;
                case 8:
                    SubscribeMovie();
                    break;
                case 9:
                    UnsubscribeMovie();
                    break;
                case 10:
                    WatchMovie();
                    break;
                case 11:
                    PauseMovie();
                    break;
                case 12:
                    ContinueWatching();
                    break;
            }
        }

        private void ContinueWatching()
        {
            ShowCurrentlyWatchingMovies();

            Console.WriteLine("-----------------------------");

            Console.Write("Choose movie: ");

            var chosenMovie = User.CurrentlyWatching[Interaction.GetIntInput(User.CurrentlyWatching.Count) - 1];

            Console.WriteLine("-----------------------------");

            chosenMovie.Play();

        }

        private void PauseMovie()
        {
            ShowCurrentlyWatchingMovies();

            Console.WriteLine("-----------------------------");

            Console.Write("Choose movie: ");

            var chosenMovie = User.CurrentlyWatching[Interaction.GetIntInput(User.CurrentlyWatching.Count) - 1];

            Console.WriteLine("-----------------------------");

            chosenMovie.Pause();

            Console.WriteLine("-----------------------------");
        }

        private void ShowCurrentlyWatchingMovies()
        {
            if(User.CurrentlyWatching.Count == 0)
            {
                Console.WriteLine("Currently watching list is empty.");
                return;
            }

            for (int i = 1; i <= User.CurrentlyWatching.Count; i++)
                Console.WriteLine($"{i}) {User.CurrentlyWatching[i - 1]}");
        }

        private void WatchMovie()
        {
            PrintMedia();

            Console.Write("Choose movie: ");

            var chosenMovie = MediaList[Interaction.GetIntInput(MediaList.Count) - 1];

            if (chosenMovie.GetType() == typeof(Movie))
            {
                var castToMovie = (Movie)chosenMovie;
                chosenMovie = new Movie(castToMovie);
            }
            else if(chosenMovie.GetType() == typeof(TVShow))
            {
                var castToTVShow = (TVShow)chosenMovie;
                chosenMovie = new TVShow(castToTVShow);
            }

            Console.WriteLine("-----------------------------");

            Task task = User.Watch(chosenMovie);
        }

        private void PrintMedia()
        {
            int indexer = 1;

            foreach (var media in MediaList)
            {
                Console.WriteLine($"{indexer}) {media.Title}");
                indexer++;
            }

            Console.WriteLine("-----------------------------");
        }

        private void SearchMedia()
        {
            Console.Write("Enter keyword: ");

            string keyword = Interaction.GetStringInput();

            Console.WriteLine("-----------------------------");

            List<Media> result = MediaList.Where(media => media.Title.ToLower().Contains(keyword.ToLower())).ToList();

            if (result.Count > 0)
                result.ForEach(media => Console.WriteLine(media));
            else
                Console.WriteLine("No results.");

            Console.WriteLine("-----------------------------");
        }

        private void FilterMedia()
        {
            int filterIndex = -1;

            var filters = new List<string>(Filter.AvailableFilters);

            do
            {
                filters.ForEach(filter => Console.WriteLine(filter));

                Console.WriteLine("Type 0 to exit filters.");

                filterIndex = Interaction.GetIntInput(filters.Count, 0);

                if (filterIndex != 0)
                {
                    string chosenFilter = filters[filterIndex - 1];
                    filters.Remove(chosenFilter);
                    ApplyFilter(chosenFilter);
                }

            } while (filterIndex != 0 && FilteredMedia.Count > 0);

            FilteredMedia = MediaList;
        }

        private void ApplyFilter(string chosenFilter)
        {
            switch (chosenFilter)
            {
                case "Release Year":
                    FilteredMedia = Filter.FilterByReleaseYear(FilteredMedia);
                    break;
                case "Rating":
                    FilteredMedia = Filter.FilterByRating(FilteredMedia);
                    break;
                case "Genre":
                    FilteredMedia = Filter.FilterByGenre(FilteredMedia);
                    break;
                case "Director":
                    FilteredMedia = Filter.FilterByDirector(FilteredMedia);
                    break;
                case "Language":
                    FilteredMedia = Filter.FilterByLanguage(FilteredMedia);
                    break;
                case "Country":
                    FilteredMedia = Filter.FilterByCountry(FilteredMedia);
                    break;
                case "Actors":
                    FilteredMedia = Filter.FilterByActor(FilteredMedia);
                    break;
            }

            if (FilteredMedia != null && FilteredMedia.Count > 0)
                FilteredMedia.ForEach(media => Console.WriteLine(media));
            else
                Console.WriteLine("Could not find any media with applied filter.");

            Console.WriteLine("-----------------------------");
        }

        private void ShowSubscribedMovies()
        {
            if (User.SubscribedMovies.Count == 0)
            {
                Console.WriteLine("No subscribtions.");
                Console.WriteLine("-----------------------------");
                return;
            }

            for (int i = 1; i <= User.SubscribedMovies.Count; i++)
                Console.WriteLine($"{i}) {User.SubscribedMovies[i - 1]}");
        }

        private void SubscribeMovie()
        {
            PrintMedia();

            Console.Write("Choose movie: ");

            var chosenMovie = MediaList[Interaction.GetIntInput(MediaList.Count) - 1];

            Console.WriteLine("-----------------------------");

            if (User.SubscribedMovies.Contains(chosenMovie))
            {
                Console.WriteLine($"{chosenMovie.Title} is already subscribed.");
                Console.WriteLine("-----------------------------");
                return;
            }

            User.SubscribedMovies.Add(chosenMovie);
            ShowSubscribedMovies();
            Console.WriteLine("-----------------------------");
        }

        private void UnsubscribeMovie()
        {
            if (User.SubscribedMovies.Count == 0)
            {
                Console.WriteLine("No subscribtions.");
                Console.WriteLine("-----------------------------");
                return;
            }

            ShowSubscribedMovies();

            Console.WriteLine("-----------------------------");

            Console.Write("Choose movie: ");

            var chosenMovie = User.SubscribedMovies[Interaction.GetIntInput(User.SubscribedMovies.Count) - 1];

            Console.WriteLine("-----------------------------");

            User.SubscribedMovies.Remove(chosenMovie);

            Console.WriteLine($"{chosenMovie.Title} has been unsubscribed.");

            Console.WriteLine("-----------------------------");
        }

        private void DeleteFromWatchList()
        {
            if (User.WatchList.Count == 0)
            {
                Console.WriteLine("Watchlist is empty.");
                Console.WriteLine("-----------------------------");
                return;
            }

            ShowWatchList();

            Console.WriteLine("-----------------------------");

            Console.Write("Choose movie: ");

            var chosenMovie = User.WatchList[Interaction.GetIntInput(User.WatchList.Count) - 1];

            Console.WriteLine("-----------------------------");

            User.WatchList.Remove(chosenMovie);

            Console.WriteLine($"{chosenMovie.Title} has been removed from watchlist.");

            Console.WriteLine("-----------------------------");
        }

        private void AddToWatchList()
        {
            PrintMedia();

            Console.Write("Choose movie: ");

            var chosenMovie = MediaList[Interaction.GetIntInput(MediaList.Count) - 1];

            Console.WriteLine("-----------------------------");

            if (User.WatchList.Contains(chosenMovie))
            {
                Console.WriteLine($"{chosenMovie.Title} is already in watchlist.");
            }
            else
            {
                User.WatchList.Add(chosenMovie);
                ShowWatchList();
            }

            Console.WriteLine("-----------------------------");
        }

        private void ShowWatchList()
        {
            if (User.WatchList.Count == 0)
            {
                Console.WriteLine("Watchlist is empty.");
                Console.WriteLine("-----------------------------");
                return;
            }

            for (int i = 1; i <= User.WatchList.Count; i++)
                Console.WriteLine($"{i}) {User.WatchList[i - 1]}");
        }

        public void ShowOperations()
        {
            foreach (var operation in Operations)
                Console.WriteLine($"{operation.Key}) {operation.Value}");
        }
    }
}
