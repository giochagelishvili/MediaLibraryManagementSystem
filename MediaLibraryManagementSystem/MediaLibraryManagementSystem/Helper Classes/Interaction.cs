namespace MediaLibraryManagementSystem.Helper_Classes
{
    public static class Interaction
    {
        public static int GetIntInput(int maxValue, int minValue = 1)
        {
            int num = minValue - 1;

            do
            {
                try
                {
                    bool userInput = int.TryParse(Console.ReadLine(), out num);

                    if (!userInput)
                        throw new InvalidCastException("User input is not a number.");
                    else if (num < minValue || num > maxValue)
                        throw new InvalidCastException("User input is out of bounds.");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    Console.WriteLine(ex.Message);
                }
            } while (num < minValue || num > maxValue);

            return num;
        }

        public static decimal GetDecimalInput(decimal minValue, decimal maxValue)
        {
            decimal num = minValue - 0.1M;

            do
            {
                try
                {
                    bool userInput = decimal.TryParse(Console.ReadLine(), out num);

                    if (!userInput)
                        throw new InvalidCastException("Invalid format.");
                    else if (num < minValue || num > maxValue)
                        throw new InvalidCastException("Out of range.");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    Console.WriteLine(ex.Message);
                }
            } while (num < minValue || num > maxValue);

            return num;
        }

        public static string GetStringInput()
        {
            string? input = "";

            do
            {
                try
                {
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                        throw new Exception("Input was an empty string or null.");
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    Console.WriteLine(ex.Message);
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }
    }
}
