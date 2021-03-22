namespace BusinessLogic
{
    public class NameGenerator
    {
        public static string Generate(string newName, IResourceManager resourceManager)
        {
            bool containt = resourceManager.ContainString(newName);
            string generatedName = newName;
            uint iteration = 1;
            while(containt)
            {
                iteration++;
                generatedName = $"{newName}{iteration}";
                containt = resourceManager.ContainString(generatedName);
            }
            return generatedName;
        }
    }
}
