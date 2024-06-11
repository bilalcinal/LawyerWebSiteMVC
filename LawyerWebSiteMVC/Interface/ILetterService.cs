using LawyerWebSiteMVC.Data;

namespace LawyerWebSiteMVC.Interface
{
    public interface ILetterService
    {
        Task<(bool, string)> CreateLetterAsync(Letter letter);
        Task<IEnumerable<Letter>> GetAllLettersAsync();
        Task<Letter> GetLetterByIdAsync(int id);

    }
}
