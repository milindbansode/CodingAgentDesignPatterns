namespace CodingAgentDesignPatterns.Web.PatternExamples.Behavioral.TemplateMethod;

public static class TemplateMethodRealWorldExample
{
    // The base class fixes the order of steps.
    // The subclass fills in the details that are allowed to vary.
    public static IReadOnlyList<string> Run()
    {
        DrinkRecipe recipe = new TeaRecipe();
        return recipe.Execute();
    }

    private abstract class DrinkRecipe
    {
        public IReadOnlyList<string> Execute()
        {
            return new[]
            {
                "Hot drink recipe",
                PrepareBase(),
                PrepareVariant(),
                Finish()
            };
        }

        private string PrepareBase() => "Base step completed.";
        protected abstract string PrepareVariant();
        private string Finish() => "Tea recipe completed";
    }

    private sealed class TeaRecipe : DrinkRecipe
    {
        protected override string PrepareVariant() => "Variant-specific step completed.";
    }
}
