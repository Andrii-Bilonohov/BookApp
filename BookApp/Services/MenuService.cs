using BookApp.Api.Models;
using BookApp.Core.Services;
using BookApp.Api.Services;
using BookApp.Storage.Data;
using BookApp.Core.Models;
using BookApp.Storage.Repositories;


namespace BookApp.Services
{
    public class MenuService
    {
        private readonly BookServiceApi _bookServiceApi;
        private readonly AuthorServiceApi _authorServiceApi;
        private readonly BookService _bookService;
        private readonly BookContext _context;
        private readonly AuthorService _authorService;
        private readonly AuthorBookService _authorBookService;


        public MenuService()
        {
            _bookServiceApi = new BookServiceApi(new HttpClient(), new LoggerService());
            _authorServiceApi = new AuthorServiceApi(new HttpClient(), new LoggerService());
            _context = new BookContext();
            _bookService = new BookService(new BookRepository(_context), new LoggerService());
            _authorService = new AuthorService(new AuthorRepository(_context), new LoggerService());
            _authorBookService = new AuthorBookService(new AuthorBookRepository(_context), new LoggerService());
        }


        private static readonly List<string> _menuItems = new List<string>
            {
                "Search Book By Name",
                "Sort Book",
                "Search Book By Author",
                "Search Author By Name",
                "Exit"
            };


        private static readonly List<string> _sortOptions = new List<string>
            {
                "Sort by Title Ascending",
                "Sort by Title Descending",
                "Sort by Publish Year Ascending",
                "Sort by Publish Year Descending"
            };


        public static void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            for (int i = 0; i < _menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuItems[i]}");
            }
        }


        public async Task RunMenuAsync()
        {
            while (true)
            {
                DisplayMenu();
                await MenuChoiceAsync();
            }
        }


        public static void DisplaySortOptions()
        {
            Console.WriteLine("Sort Options:");
            for (int i = 0; i < _sortOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_sortOptions[i]}");
            }
        }


        public async Task MenuChoiceAsync()
        {
            var choice = await Validation.GetValiString("Please select an option from the menu: ");

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    await GetBooksByTitleAsync();
                    break;
                case "2":
                    Console.Clear();
                    await SortBooksAsync();
                    break;
                case "3":
                    Console.Clear();
                    await GetBooksByAuthorAsync();
                    break;
                case "4":
                    Console.Clear();
                    await GetAuthorByNameAsync();
                    break;
                case "5":
                    Console.WriteLine("Exiting the application. Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    await ReturnToMenu();
                    break;
            }
        }


        private async Task GetBooksByTitleAsync()
        {
            var title = await Validation.GetValiString("Please enter the book title: ");
            
            Console.WriteLine("Fetching books by title from API...");
            await GetBooksByTitleApiAsync(title);

            Console.WriteLine("\n\n");

            Console.WriteLine("Fetching books by title from local database...");
            await GetBooksByTitleWithLocalDataBaseAsync(title);

            await ReturnToMenu();
        }


        private async Task GetBooksByTitleApiAsync(string title)
        {
            var bookWrapper = await _bookServiceApi.GetByTitleAsync(title);

            if (bookWrapper?.Docs == null)
            {
                Console.WriteLine("No books found in Api.");
                return;
            }

            Console.WriteLine(bookWrapper);
        }


        private async Task GetBooksByTitleWithLocalDataBaseAsync(string title)
        {
            var books = await _bookService.GetByTitleAsync(title);

            if (books == null || !books.Any())
            {
                Console.WriteLine("No books found in the local database.");
                await ReturnToMenu();
                return;
            }

            foreach (var book in books)
                Console.WriteLine(book);
        }


        private async Task SortBooksAsync()
        {
            Console.Clear();

            DisplaySortOptions();
            var sortOption = await Validation.GetValiString("Please select a sort option: ");
            await GetBooksByTitleSortedAsync(sortOption);
        }


        private async Task GetBooksByTitleSortedAsync(string sortOption)
        {
            var title = await Validation.GetValiString("Please enter the book title: ");

            switch (sortOption)
            {
                case "1":
                    Console.WriteLine("Fetching books by title from API...");
                    await GetBooksByTitleSortedApiAsync(b => b.Title, false, title);

                    Console.WriteLine("\n\n");

                    Console.WriteLine("Fetching books by title from local database...");
                    await GetBooksByTitleSortedWithLocalDataBaseAsync(b => b.Title, false, title);

                    await ReturnToMenu();
                    break;
                case "2":
                    Console.WriteLine("Fetching books by title from API...");
                    await GetBooksByTitleSortedApiAsync(b => b.Title, true, title);

                    Console.WriteLine("\n\n");

                    Console.WriteLine("Fetching books by title from local database...");
                    await GetBooksByTitleSortedWithLocalDataBaseAsync(b => b.Title, true, title);

                    await ReturnToMenu();
                    break;
                case "3":
                    Console.WriteLine("Fetching books by title from API...");
                    await GetBooksByTitleSortedApiAsync(b => b.FirstPublishYear, false, title);

                    Console.WriteLine("\n\n");

                    Console.WriteLine("Fetching books by title from local database...");
                    await GetBooksByTitleSortedWithLocalDataBaseAsync(b => b.FirstPublishYear, false, title);

                    await ReturnToMenu();
                    break;
                case "4":
                    Console.WriteLine("Fetching books by title from API...");
                    await GetBooksByTitleSortedApiAsync(b => b.FirstPublishYear, true, title);

                    Console.WriteLine("\n\n");

                    Console.WriteLine("Fetching books by title from local database...");
                    await GetBooksByTitleSortedWithLocalDataBaseAsync(b => b.FirstPublishYear, true, title);

                    await ReturnToMenu();
                    break;
                default:
                    Console.WriteLine("Invalid sort option. Please try again.");
                    await ReturnToMenu();
                    break;
            }
        }


        private async Task GetBooksByTitleSortedApiAsync(Func<BookApi, object> keySelector, bool descending, string title)
        {
            var bookWrapper = await _bookServiceApi.GetByTitleAsync(title);

            if (bookWrapper?.Docs == null || bookWrapper.Docs.Count == 0)
            {
                Console.WriteLine("No books found in the API.");
                return;
            }

            var sortedBooks = descending ? bookWrapper.Docs.OrderByDescending(keySelector) : bookWrapper.Docs.OrderBy(keySelector);

            foreach (var book in sortedBooks)
                Console.WriteLine(book);
        }


        private async Task GetBooksByTitleSortedWithLocalDataBaseAsync(Func<Book, object> keySelector, bool descending, string title)
        {
            var books = await _bookService.GetByTitleAsync(title);
            
            if (books == null || !books.Any())
            {
                Console.WriteLine("No books found in the local database.");
                return;
            }

            var sortedBooks = descending ? books.OrderByDescending(keySelector) : books.OrderBy(keySelector);
            
            foreach (var book in sortedBooks)
                Console.WriteLine(book);
        }


        private async Task GetBooksByAuthorAsync()
        {
            var authorName = await Validation.GetValiString("Please enter the author's name: ");
            
            Console.WriteLine("Fetching books by author from API...");
            await GetBooksByAuthorApiAsync(authorName);

            Console.WriteLine("\n\n");

            Console.WriteLine("Fetching books by author from local database...");
            await GetBooksByAuthorWithLocalDataBaseAsync(authorName);
            await ReturnToMenu();
        }


        private async Task GetBooksByAuthorApiAsync(string authorName)
        {
            var bookWrapper = await _bookServiceApi.GetByAuthorAsync(authorName);
            
            if (bookWrapper == null)
            { 
                Console.WriteLine("No books found in the API.");
                await ReturnToMenu();
                return;
            }

            Console.WriteLine(bookWrapper);
        }


        private async Task GetBooksByAuthorWithLocalDataBaseAsync(string authorName)
        {
            var books = await _authorBookService.GetBooksByAuthorNameAsync(authorName);

            if (books == null || !books.Any())
            {
                Console.WriteLine("No books found for the specified author in the local database.");
                return;
            }

            foreach (var book in books)
                Console.WriteLine(book);
        }


        private async Task GetAuthorByNameAsync()
        {
            var authorName = await Validation.GetValiString("Please enter the author's name: ");
            
            Console.WriteLine("Fetching author by name from API...");
            await GetAuthorByNameApiAsync(authorName);

            Console.WriteLine("\n\n");

            Console.WriteLine("Fetching author by name from local database...");
            await GetAuthorByNameWithLocalDataBaseAsync(authorName);

            await ReturnToMenu();
        }


        private async Task GetAuthorByNameApiAsync(string authorName)
        {
            var authorWrapper = await _authorServiceApi.GetByNameAsync(authorName);
            
            if (authorWrapper?.Docs == null)
            {
                Console.WriteLine("No authors found in the API.");
                return;
            }

            Console.WriteLine(authorWrapper);
        }


        private async Task GetAuthorByNameWithLocalDataBaseAsync(string authorName)
        {
            var authors = await _authorService.GetByNameAsync(authorName);

            if (authors == null || !authors.Any())
            {
                Console.WriteLine("No authors found in the local database.");
                return;
            }

            foreach (var author in authors)
                Console.WriteLine(author);
        }


        private async Task ReturnToMenu()
        {
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
            Console.Clear();
            await RunMenuAsync();
        }
    }
}
