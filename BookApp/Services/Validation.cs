namespace BookApp.Services
{
    public static class Validation
    {
        public static Task<string> GetValiString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim() ?? string.Empty;
                
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return Task.FromResult(input);
                }

                Console.WriteLine("Input cannot be empty. Please try again.");
            } while (true);
        }
    }
}
