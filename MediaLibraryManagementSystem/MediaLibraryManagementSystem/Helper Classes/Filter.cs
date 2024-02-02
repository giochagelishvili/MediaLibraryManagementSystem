using MediaLibraryManagementSystem.Media_Classes;

namespace MediaLibraryManagementSystem.Helper_Classes
{
    public static class Filter
    {
        public readonly static List<string> AvailableFilters = new()
        {
            "Release Year",
            "Rating",
            "Genre",
            "Director",
            "Language",
            "Country",
            "Actors"
        };

        public static List<Media> FilterByReleaseYear(List<Media> mediaList)
        {
            Console.Write("Released after: ");
            int releaseYear = Interaction.GetIntInput(2024);
            Console.WriteLine("-----------------------------");
            return mediaList.Where(media => media.ReleaseYear > releaseYear).ToList();
        }

        public static List<Media> FilterByRating(List<Media> mediaList)
        {
            Console.Write("Rating higher than: ");
            decimal rating = Interaction.GetDecimalInput(0, 10);
            Console.WriteLine("-----------------------------");
            return mediaList.Where(media => media.Rating > rating).ToList();
        }        
        
        public static List<Media> FilterByGenre(List<Media> mediaList)
        {
            Console.WriteLine("Choose genre: ");
            
            int genreCount = Enum.GetValues(typeof(Genre)).Length;

            for(int i = 1; i <= genreCount; i++)
                Console.WriteLine($"{i}) {(Genre)i}");

            Genre chosenGenre = (Genre)Interaction.GetIntInput(genreCount);

            Console.WriteLine("-----------------------------");
            
            return mediaList.Where(media => media.Genre.Equals(chosenGenre)).ToList();
        }

        public static List<Media> FilterByDirector(List<Media> mediaList)
        {
            Console.Write("Director: ");
            string director = Interaction.GetStringInput().ToLower();
            Console.WriteLine("-----------------------------");
            return mediaList.Where(media => media.Director.ToLower().Contains(director)).ToList();
        }        
        
        public static List<Media> FilterByCountry(List<Media> mediaList)
        {
            Console.Write("Country (e.g. UK, USA): ");
            string country = Interaction.GetStringInput().ToLower();
            Console.WriteLine("-----------------------------");
            return mediaList.Where(media => media.Country.ToLower().Contains(country)).ToList();
        }        
        
        public static List<Media> FilterByActor(List<Media> mediaList)
        {
            Console.Write("Actor name: ");
            string actorName = Interaction.GetStringInput().ToLower();
            Console.WriteLine("-----------------------------");
            return mediaList
                .Where(media => media.Actors.Any(actor => actor.ToLower().Contains(actorName)))
                .ToList();
        }

        public static List<Media> FilterByLanguage(List<Media> mediaList)
        {
            Console.WriteLine("Choose language: ");

            int languageCount = Enum.GetValues(typeof(Language)).Length;

            for (int i = 1; i <= languageCount; i++)
                Console.WriteLine($"{i}) {(Language)i}");

            Language chosenLanguage = (Language)Interaction.GetIntInput(languageCount);

            Console.WriteLine("-----------------------------");

            return mediaList.Where(media => media.Language.Equals(chosenLanguage)).ToList();
        }
    }
}
