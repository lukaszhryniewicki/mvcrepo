using FORUM_DYSKUSYJNE.Database;

namespace FORUM_DYSKUSYJNE.Contracts
{
	public interface ITopicsService
	{
		Topic GetTopicById(int id);

	}
}