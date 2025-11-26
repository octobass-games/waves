namespace Octobass.Waves.Item
{
    public class LoreItemInstance : ItemInstance
    {
        public LoreItemDefinition Definition;

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
