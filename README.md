# CodingAgentDesignPatterns

ASP.NET Core MVC learning app for all 23 Gang of Four design patterns.

## What is included

- All 23 GOF patterns
- Two examples for every pattern
  - Real-world scenario
  - Banking / trading scenario
- Browser-based UI to explore patterns and open the commented C# source code
- Sample output for every example so learners can quickly connect the code to the result

## Project structure

- `/src/CodingAgentDesignPatterns.Web` - ASP.NET Core MVC application
- `/src/CodingAgentDesignPatterns.Web/PatternExamples` - commented example code for every pattern
- `/src/CodingAgentDesignPatterns.Web/Views/Patterns` - pattern explorer pages

## Run locally

```bash
dotnet build /tmp/workspace/milindbansode/CodingAgentDesignPatterns/CodingAgentDesignPatterns.slnx
dotnet run --project /tmp/workspace/milindbansode/CodingAgentDesignPatterns/src/CodingAgentDesignPatterns.Web/CodingAgentDesignPatterns.Web.csproj
```

Then open the app and browse to the **Pattern Explorer** page.
