using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace MvcTaskManager.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UploadController : ControllerBase
  {

    [HttpPost, DisableRequestSizeLimit]
    public IActionResult Upload()
    {
      try
      {
        var file = Request.Form.Files[0];
        //var folderName = Path.Combine("Resources", "Imagesx");
        var folderName = Path.Combine("wwwroot", "assets");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

  
        if (file.Length > 0)
        {
          var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          var fullPath = Path.Combine(pathToSave, fileName);
          var dbPath = Path.Combine(folderName, fileName);

          using (var stream = new FileStream(fullPath, FileMode.Create))
          {
            file.CopyTo(stream);
          }
          return Ok(new { dbPath }); 
        }
        else
        {
          return BadRequest();
        }
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex}");
      }
    
   }
  }
}
