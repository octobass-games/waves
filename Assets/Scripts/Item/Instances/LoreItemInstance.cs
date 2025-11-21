namespace Octobass.Waves.Item
{
    public class LoreItemInstance : ItemInstance
    {
        private LoreItemDefinition Definition;

        public LoreItemInstance(string name, LoreItemDefinition definition) : base(name)
        {
            Definition = definition;
        }

        public string GetText()
        {
            return Definition.Text;
        }
    }
}
