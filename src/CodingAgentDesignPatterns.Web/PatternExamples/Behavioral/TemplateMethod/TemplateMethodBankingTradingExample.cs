namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.TemplateMethod;

public static class TemplateMethodBankingTradingExample
{
    // The base class fixes the order of steps.
    // The subclass fills in the details that are allowed to vary.
    public static IReadOnlyList<string> Run()
    {
        EndOfDayReport recipe = new RiskEndOfDayReport();
        return recipe.Execute();
    }

    private abstract class EndOfDayReport
    {
        public IReadOnlyList<string> Execute()
        {
            return new[]
            {
                "End-of-day report workflow",
                PrepareBase(),
                PrepareVariant(),
                Finish()
            };
        }

        private string PrepareBase() => "Base step completed.";
        protected abstract string PrepareVariant();
        private string Finish() => "Risk report workflow completed";
    }

    private sealed class RiskEndOfDayReport : EndOfDayReport
    {
        protected override string PrepareVariant() => "Variant-specific step completed.";
    }
}
