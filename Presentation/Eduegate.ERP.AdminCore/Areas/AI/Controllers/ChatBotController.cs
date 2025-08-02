using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Domain.AI;
using Org.BouncyCastle.Asn1.Crmf;
using Eduegate.Services.Contracts.School.AI;


namespace Eduegate.ERP.Admin.Areas.AI.Controllers
{
    [Area("AI")]
    public class ChatBotController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> rasaChat()
        {
            try
            {
                ChatBotDTO request = new ChatBotDTO();

                if (Request.HasFormContentType)
                {
                    // Handle form-data request (for audio)
                    var formCollection = await Request.ReadFormAsync();
                    request.VoiceData = formCollection["VoiceData"]; // Text data from form (if any)

                    var file = formCollection.Files.GetFile("VoiceData"); // Ensure key matches frontend
                    if (file != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();
                            request.VoiceData = Convert.ToBase64String(fileBytes); // Convert audio to Base64
                        }
                    }
                }
                else
                {
                    // Handle JSON request (for text messages)
                    request = await Request.ReadFromJsonAsync<ChatBotDTO>();
                }

                if (string.IsNullOrWhiteSpace(request.Message) && string.IsNullOrWhiteSpace(request.VoiceData))
                {
                    return BadRequest(new { success = false, message = "Message or voice data cannot be empty." });
                }

                var result = await new ChatBotBL(CallContext).RasaChatAsync(request);

                return Ok(new { success = result != null, message = result ?? "No response from bot." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}