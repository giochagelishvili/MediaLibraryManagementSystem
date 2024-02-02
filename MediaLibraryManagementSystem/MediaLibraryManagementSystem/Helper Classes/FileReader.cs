using MediaLibraryManagementSystem.Media_Classes;

namespace MediaLibraryManagementSystem.Helper_Classes
{
    public static class FileReader
    {
        public static List<Media> ReadMedia(string filePath)
        {
            List<Media> mediaList = new();

            using(var streamReader = new StreamReader(filePath))
            {
                var line = streamReader.ReadLine();

                while (line != null)
                {
                    var splittedLine = line.Split('|');

                    string title = splittedLine[0];
                    int releaseYear = int.Parse(splittedLine[1]);
                    decimal rating = decimal.Parse(splittedLine[2]);
                    Genre genre = (Genre)Enum.Parse(typeof(Genre), splittedLine[3]);
                    string director = splittedLine[4];
                    Language language = (Language)Enum.Parse(typeof(Language), splittedLine[5]);
                    string country = splittedLine[6];
                    List<string> actors = splittedLine[7].Split(",").ToList();
                    int duration = int.Parse(splittedLine[8]);

                    if (title.StartsWith('*'))
                    {
                        title = title.TrimStart('*');
                        mediaList.Add(new Movie(title, releaseYear, rating, genre, director, language, country, actors, duration));
                    }
                    else
                    {
                        mediaList.Add(new TVShow(title, releaseYear, rating, genre, director, language, country, actors, duration));
                    }

                    line = streamReader.ReadLine();
                }
            }

            return mediaList;
        }
    }
}