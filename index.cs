using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

class Engineer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime LastSupportDate { get; set; }

    public Engineer(string name, string email, DateTime lastSupportDate)
    {
        Name = name;
        Email = email;
        LastSupportDate = lastSupportDate;
    }
}

[ApiController]
[Route("[controller]")]
public class SupportWheelOfFateController : ControllerBase
{
    private static List<Engineer> engineers = new List<Engineer>
    {
        new Engineer("John Smith", "johnsmith@email.com", new DateTime(2021, 1, 1)),
        new Engineer("Jane Doe", "janedoe@email.com", new DateTime(2021, 1, 2)),
        // Add more engineers as necessary
    };

    [HttpGet]
    public ActionResult<IEnumerable<Engineer>> Get(DateTime date)
    {
        var availableEngineers = engineers
            .Where(e => e.LastSupportDate.Date != date.Date && e.LastSupportDate.Date != date.AddDays(-1).Date)
            .ToList();

        var rng = new Random();
        var selectedEngineers = Enumerable.Range(0, 10).Select(i => availableEngineers[rng.Next(availableEngineers.Count)]).ToList();

        return Ok(selectedEngineers);
    }
}