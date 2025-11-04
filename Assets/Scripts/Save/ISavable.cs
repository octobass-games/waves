namespace Octobass.Waves.Save
{
    public interface ISavable
    {
        void Save(SaveData saveData);

        void Load(SaveData saveData);
    }
}
