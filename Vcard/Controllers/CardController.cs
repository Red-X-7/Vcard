using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Collections.Generic;

namespace Vcard.Controllers
{
    [ApiController]
    [Route("card")]
    public class CardController : ControllerBase
    {
        // 1️⃣ BusinessCard sınıfı
        public class BusinessCard
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public string Company { get; set; }
            public string Title { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Website { get; set; }
        }

        // 2️⃣ Basit Dictionary ile veri depolama
        private static Dictionary<string, BusinessCard> cards = new()
        {
            {
                "redx13",
                new BusinessCard
                {
                    Id = "red",
                    FullName = "Red ",
                    Company = "Game Studio",
                    Title = "Unity Developer",
                    Phone = "+905xxxxxxxxx",
                    Email = "mail@site.com",
                    Website = "https://portfolio.com"
                }
            }
        };

        // 3️⃣ GET endpoint → vCard üretir
        [HttpGet("{id}")]
        public IActionResult GetCard(string id)
        {
            if (!cards.ContainsKey(id)) return NotFound();

            var c = cards[id];
            string vcard = $@"BEGIN:VCARD
VERSION:3.0
FN:{c.FullName}
ORG:{c.Company}
TITLE:{c.Title}
TEL:{c.Phone}
EMAIL:{c.Email}
URL:{c.Website}
END:VCARD";

            var bytes = Encoding.UTF8.GetBytes(vcard);
            return File(bytes, "text/vcard", "contact.vcf");
        }

        // 4️⃣ PUT endpoint → bilgiyi günceller
        [HttpPut("{id}")]
        public IActionResult UpdateCard(string id, [FromBody] BusinessCard updated)
        {
            if (!cards.ContainsKey(id)) return NotFound();

            cards[id] = updated;
            return Ok(cards[id]);
        }
    }
}