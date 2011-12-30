namespace SoundBoard
{
	public interface IFileDialogController
	{
        string OpenFile(string xiFilter);
        string SaveFile(string xiFilter);
	}
}
