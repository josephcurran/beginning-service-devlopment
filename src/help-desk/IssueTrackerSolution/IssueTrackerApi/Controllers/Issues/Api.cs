using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerApi.Controllers.Issues;

public class Api : ControllerBase
{

    // GET /issues
    [HttpGet("/issues")]
    public async Task<ActionResult> GetTheIssuesAsync()
    {
        var issues = new List<object>();
        return Ok(issues);
    }

    [HttpPost("/issues")]
    public async Task<ActionResult> AddIssueAsync(
       [FromBody] CreateIssueRequestModel request,
       [FromServices] IValidator<CreateIssueRequestModel> validator)
    {
        var results = await validator.ValidateAsync(request);
        if (results.IsValid)
        {
            var response = new Issue
            {
                CreatedAt = DateTimeOffset.UtcNow,
                Description = request.Description,
                Software = request.Software,
                Id = Guid.NewGuid(),
                Status = IssueStatus.Created
            };
            // do our thing.
            return Ok(response);
        }
        else
        {
            return BadRequest(results.ToDictionary()); // 400
        }

    }
}

/*
 * {
  "software": "Excel",
  "description": "I want clippy back"
} */

public record CreateIssueRequestModel
{

    public string Software { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}


public record Issue
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Software { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public IssueStatus Status { get; set; }

}
public enum IssueStatus { Created }

public class CreateIssueRequestModelValidator : AbstractValidator<CreateIssueRequestModel>
{
    private readonly IReadOnlyList<string> _supportedSoftware = ["excel", "powerpoint", "word"];
    public CreateIssueRequestModelValidator()
    {
        RuleFor(i => i.Description)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(i => i.Software)
            .NotEmpty()
            .Must(i =>
            {
                var sw = i.ToLowerInvariant().Trim();
                return _supportedSoftware.Any(s => s == sw);

            }).WithMessage("Unsupported Software. Good Luck");

    }
}