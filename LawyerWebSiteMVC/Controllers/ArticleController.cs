using LawyerWebSiteMVC.Data;
using LawyerWebSiteMVC.Interface;
using LawyerWebSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LawyerWebSiteMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleDto articleDto)
        {
            var result = await _articleService.CreateArticleAsync(articleDto);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle([FromBody] ArticleDto articleDto)
        {
            var result = await _articleService.UpdateArticleAsync(articleDto);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);
            if (result)
                return Ok("Article deleted successfully");
            return NotFound("Article not found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article != null)
                return Ok(article);
            return NotFound("Article not found");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return Ok(articles);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            var result = await _articleService.CreateCommentAsync(comment);
            if (result.Item1)
                return Ok(result.Item2);
            return BadRequest(result.Item2);
        }

        [HttpDelete("comment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _articleService.DeleteCommentAsync(id);
            if (result)
                return Ok("Comment deleted successfully");
            return NotFound("Comment not found");
        }

        [HttpGet("{articleId}/comments")]
        public async Task<IActionResult> GetCommentsByArticleId(int articleId)
        {
            var comments = await _articleService.GetCommentsByArticleIdAsync(articleId);
            return Ok(comments);
        }
    }
}
