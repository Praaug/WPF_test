using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common;
using ServerCommunication.Interfaces;

namespace ServerCommunication.Classes
{


    public class ServerConnector : IServerConnector
    {
        private List<BookDto> _books;
        private List<BookDetailsDto> _bookDetails;

        public ServerConnector()
        {
            CreateBooks();
            CreateDetails();
        }

        /// <inheritdoc />
        public List<BookDto> GetBooks()
        {
            // some calls to the backend later we get the following results:
            return _books;
        }

        /// <inheritdoc />
        public BookDetailsDto? GetBookDetails(int id)
        {
            return _bookDetails.FirstOrDefault(details => details.BookId == id);
        }

        /// <inheritdoc />
        public bool AddNewBook(BookDetailsDto bookDetails)
        {
            BookDetailsDto addedBook = bookDetails;
            addedBook.BookId = GetHighestBookId();
            addedBook.Id = GetHighestBookDetailsId();
            _books.Add(new BookDto()
            {
                Author = addedBook.Author,
                Title = addedBook.Title,
                Id = addedBook.BookId,
                IsCheckedOut = false,
                ImagePath = addedBook.ImagePath,
            });
            _bookDetails.Add(addedBook);

            return true;
        }

        /// <inheritdoc />
        public bool RemoveBook(int id)
        {
            if (id < 0 || id > GetHighestBookId())
            {
                return false;
            }

            _books.Remove(_books.First(b => b.Id == id));

            return true;
        }

        /// <inheritdoc />
        public bool CheckoutBook(int id)
        {
            if (id < 0 || id > GetHighestBookId())
            {
                return false;
            }

            BookDto? book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null || book.IsCheckedOut)
            {
                return false;
            }

            book.IsCheckedOut = true;
            return true;

        }

        /// <inheritdoc />
        public bool ReturnBook(int id)
        {
            if (id < 0 || id > GetHighestBookId())
            {
                return false;
            }

            BookDto? book = _books.FirstOrDefault(b => b.Id == id);
            if (book is not { IsCheckedOut: true })
            {
                return false;
            }

            book.IsCheckedOut = false;
            return true;
        }

        private void CreateBooks()
        {
            _books = new List<BookDto>();
            _books.Add(new BookDto()
            {
                Id = 0,
                Author = "Andy Weir",
                Title = "Der Marsianer",
                ImagePath = "DerMarsianer.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 1,
                Author = "Kendra Halloway",
                Title = "The Shadows of Elaria",
                ImagePath = "TheShadowsOfElaria.png",
            });
            _books.Add(new BookDto()
            {
                Id = 2,
                Author = "Dr. Nathaniel Bryce",
                Title = "The Quantum Enigma",
                ImagePath = "TheQuantumEnigma.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 3,
                Author = "Emily Rivers",
                Title = "Whispering Pines",
                ImagePath = "WhisperingPines.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 4,
                Author = "Oliver Grant",
                Title = "The Painted Veil",
                ImagePath = "ThePaintedVeil.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 5,
                Author = "George Orwell",
                Title = "1984",
                ImagePath = "1984.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 7,
                Author = "Frank Schätzing",
                Title = "Der Schwarm",
                ImagePath = "DerSchwarm.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 8,
                Author = "Hermann Hesse",
                Title = "Der Steppenwolf",
                ImagePath = "DerSteppenwolf.jpg",
            });
            _books.Add(new BookDto()
            {
                Id = 6,
                Author = "J.K. Rowling",
                Title = "Harry Potter und der Stein der Weisen",
                ImagePath = "HarryPotterUndDerSteinDerWeisen.jpg",
            });

        }

        private void CreateDetails()
        {
            _bookDetails = new List<BookDetailsDto>();
            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 0,
                Author = "Andy Weir",
                Title = "Der Marsianer",
                ImagePath = "DerMarsianer.jpg",
                BookId = 0,
                CompletionPercentage = 0,
                Edition = "2. Ausgabe",
                Language = "Deutsch",
                PageCount = 512,
                Price = 14.99,
                Publisher = "Heyne",
                ReleaseDate = new DateTime(2014, 10, 13),
                ISBN = "978-3-453-31583-9",
                Genre = "Science Fiction",
            });
            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 1,
                Author = "Kendra Halloway",
                Title = "The Shadows of Elaria",
                ImagePath = "TheShadowsOfElaria.jpg",
                BookId = 1,
                CompletionPercentage = 0,
                Edition = "2nd Edition",
                Language = "English",
                PageCount = 432,
                Price = 15.99,
                Publisher = "Mythic Press",
                ReleaseDate = new DateTime(2021, 6, 15),
                ISBN = "978-1-234-56789-0",
                Genre = "Fantasy",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 2,
                Author = "Dr. Nathaniel Bryce",
                Title = "The Quantum Enigma",
                ImagePath = "TheQuantumEnigma.jpg",
                BookId = 2,
                CompletionPercentage = 0,
                Edition = "1st Edition",
                Language = "English",
                PageCount = 298,
                Price = 18.99,
                Publisher = "Science Sphere",
                ReleaseDate = new DateTime(2020, 11, 22),
                ISBN = "978-1-345-67890-1",
                Genre = "Science Fiction",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 3,
                Author = "Emily Rivers",
                Title = "Whispering Pines",
                ImagePath = "WhisperingPines.jpg",
                BookId = 3,
                CompletionPercentage = 0,
                Edition = "4th Edition",
                Language = "English",
                PageCount = 384,
                Price = 14.99,
                Publisher = "Mystery House",
                ReleaseDate = new DateTime(2019, 3, 10),
                ISBN = "978-1-456-78901-2",
                Genre = "Mystery/Thriller",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 4,
                Author = "Oliver Grant",
                Title = "The Painted Veil",
                ImagePath = "ThePaintedVeil.jpg",
                BookId = 4,
                CompletionPercentage = 30,
                Edition = "1st Edition",
                Language = "English",
                PageCount = 375,
                Price = 18.50,
                Publisher = "Historical Fiction Press",
                ReleaseDate = new DateTime(2019, 7, 10),
                ISBN = "978-1-23456-789-3",
                Genre = "Historical Fiction",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 5,
                Author = "George Orwell",
                Title = "1984",
                ImagePath = "1984.jpg",
                BookId = 5,
                CompletionPercentage = 56,
                Edition = "1. Ausgabe",
                Language = "Deutsch",
                PageCount = 336,
                Price = 12.99,
                Publisher = "Ullstein",
                ReleaseDate = new DateTime(2013, 7, 1),
                ISBN = "978-3-548-26540-5",
                Genre = "Dystopie",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 6,
                Author = "J.K. Rowling",
                Title = "Harry Potter und der Stein der Weisen",
                ImagePath = "HarryPotterUndDerSteinDerWeisen.jpg",
                BookId = 6,
                CompletionPercentage = 100,
                Edition = "1. Ausgabe",
                Language = "Deutsch",
                PageCount = 336,
                Price = 19.99,
                Publisher = "Carlsen",
                ReleaseDate = new DateTime(1998, 7, 27),
                ISBN = "978-3-551-55451-7",
                Genre = "Fantasy",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 7,
                Author = "Frank Schätzing",
                Title = "Der Schwarm",
                ImagePath = "DerSchwarm.jpg",
                BookId = 7,
                CompletionPercentage = 0,
                Edition = "1. Ausgabe",
                Language = "Deutsch",
                PageCount = 1000,
                Price = 24.99,
                Publisher = "Kiepenheuer & Witsch",
                ReleaseDate = new DateTime(2004, 8, 26),
                ISBN = "978-3-462-03419-3",
                Genre = "Thriller",
            });

            _bookDetails.Add(new BookDetailsDto()
            {
                Id = 8,
                Author = "Hermann Hesse",
                Title = "Der Steppenwolf",
                ImagePath = "DerSteppenwolf.jpg",
                BookId = 8,
                CompletionPercentage = 0,
                Edition = "3. Ausgabe",
                PageCount = 320,
                Publisher = "Suhrkamp",
                ReleaseDate = new DateTime(2012, 5, 1),
                ISBN = "978-3-518-37111-6",
                Genre = "Roman",
            });

        }

        private int GetHighestBookId()
        {
            int highest = 0;
            foreach (var book in _books)
            {
                highest = Math.Max(highest, book.Id);
            }

            return highest;
        }

        private int GetHighestBookDetailsId()
        {

            int highest = 0;
            foreach (var details in _bookDetails)
            {
                highest = Math.Max(highest, details.Id);
            }

            return highest;
        }
    }
}