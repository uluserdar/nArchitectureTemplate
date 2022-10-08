namespace nArchitectureExtension.Services.GenerationServices.CommandCodeGenerators
{
    public interface ICommandCodeGeneratorService
    {
        public void GenerateCreateCommandCode();
        public void GenerateUpdateCommandCode();
        public void GenerateDeleteCommandCode();
    }
}
