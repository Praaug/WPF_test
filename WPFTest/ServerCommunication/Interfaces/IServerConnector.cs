using Common;

namespace ServerCommunication.Interfaces;

public interface IServerConnector
{
    /// <summary> Gets all books in the Library </summary>
    /// <returns>A list of BookDtos containing all books in the librabry</returns>
    List<BookDto> GetBooks();
    
    /// <summary> Gets the details of the book with the provided id </summary>
    /// <param name="id">The id of the book</param>
    /// <returns>The Book details corresponding to the id</returns>
    BookDetailsDto? GetBookDetails(int id);

    /// <summary> Adds a new book to the Library </summary>
    /// <param name="bookDetails">The book with all details to add</param>
    /// <returns>[true] if the adding was sucessfull</returns>
    bool AddNewBook(BookDetailsDto bookDetails);
    
    /// <summary> Removes a book from the Library </summary>
    /// <param name="id">The id of the book to remove</param>
    /// <returns>[true] if the deletion was successfull</returns>
    bool RemoveBook(int id);

    /// <summary> Checks out a book from the Library </summary>
    /// <param name="id">The id of the book to checkout</param>
    /// <returns>[true] if the checkout process was successfull</returns>
    bool CheckoutBook(int id);
    
    /// <summary> Returns a checked out book </summary>
    /// <param name="id">the id of the book</param>
    /// <returns>[true] if the return process was successfull</returns>
    bool ReturnBook(int id);
}