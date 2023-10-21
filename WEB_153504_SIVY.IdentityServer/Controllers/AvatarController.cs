using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WEB_153504_SIVY.IdentityServer.Models;

namespace WEB_153504_SIVY.IdentityServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        public AvatarController(IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _environment = environment;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var id = _userManager.GetUserId(User);
            var path = Directory.GetFiles(Path.Combine(_environment.ContentRootPath, "Images"), $"{id}.*").FirstOrDefault();

            var memory = new MemoryStream();

            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(_environment.ContentRootPath, "Images", "user-icon.png");
            }

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            var fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
            fileExtensionContentTypeProvider.TryGetContentType(path, out string mime);

            memory.Position = 0;

            return File(memory, mime);
        } 
    }
}
