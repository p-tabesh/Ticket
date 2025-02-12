using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using Ticket.Application.Models;

namespace Ticket.Presentation.Controllers
{
    [Route("OtpManager")]
    public class OtpManagerController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;
        public OtpManagerController(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }

        [HttpPost]
        [Route("get-otp")]
        public async Task<IActionResult> GetOtp(string phoneNumber)
        {
            var otpInfo = new OtpGeneratorModel() { phoneNumber = phoneNumber };
            var SerializedOtpInfo = JsonSerializer.Serialize(otpInfo);
            var encodedOtpInfo = Encoding.UTF8.GetBytes(SerializedOtpInfo);


            await _cache.SetAsync(phoneNumber, encodedOtpInfo, _options);
            return Ok();
        }

        [HttpPost]
        [Route("validate-otp")]
        public async Task<IActionResult> ValidateOtp(string phone, int Otp)
        {
            var content = await _cache.GetStringAsync(phone);
            if (content is null)
            {
                return BadRequest("this phone must request otp first");
            }
            var otpInfo = JsonSerializer.Deserialize<OtpGeneratorModel>(content);

            if (Otp == otpInfo.Otp && DateTime.Now < otpInfo.ExpireDate && phone == otpInfo.phoneNumber)
            {
                _cache.Remove(phone);
                return Ok($"Otp is Correct");
            }
            else
            {
                return BadRequest("otp invalid");
            }
        }
    }
}
